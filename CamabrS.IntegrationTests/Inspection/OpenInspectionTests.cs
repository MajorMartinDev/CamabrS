using CamabrS.API.Core.Http;
using CamabrS.API.Inspection;
using CamabrS.API.Inspection.GettingDetails;
using CamabrS.API.Inspection.Opening;
using static CamabrS.API.Inspection.Opening.OpenEndpoints;

namespace CamabrS.IntegrationTests.Inspection;
public sealed class OpenInspectionTests(AppFixture fixture) : IntegrationContext(fixture)
{  
    [Fact]
    public async Task Open_a_new_Inspection_should_succeed()
    {
        //given
        var user = new User(Guid.NewGuid());
        var openedAt = DateTimeOffset.UtcNow;
        
        //when
        var initial = await Host.Scenario(x =>
        {            
            x.Post
                .Json(new OpenInspection(BaselineData.DefaultTestAssetId, openedAt))
                .ToUrl(OpenEnpoint);

            x.StatusCodeShouldBeOk();

            x.WithClaim(new Claim("user-id", user.Id.ToString()));            
        });

        //then
        var inspectionId = initial.ReadAsJson<ApiCreationResponse>()!.Id;

        using var session = Store.LightweightSession();
        var events = await session.Events.FetchStreamAsync(inspectionId);
        var opened = events[0].Data.ShouldBeOfType<InspectionOpened>();
       
        opened.AssetId.ShouldBe(BaselineData.DefaultTestAssetId);
        opened.OpenedBy.ShouldBe(user.Id);
        opened.OpenedAt.ShouldBe(openedAt);
        
        var inspection = await session.Query<InspectionDetails>().SingleOrDefaultAsync(i => i.Id == inspectionId);
        inspection.ShouldNotBeNull();
        inspection.AssignedSpecialists.Length.ShouldBe(0);
        inspection.Status.ShouldBe(InspectionStatus.Opened);
    }

    [Fact]
    public async Task Open_a_new_Inspection_with_nonexistent_Asset_should_fail()
    {
        //given
        var assetId = Guid.NewGuid();
        var openedAt = DateTimeOffset.UtcNow;

        var user = new User(Guid.NewGuid());

        //when
        var initial = await Host.Scenario(x =>
        {
            x.Post
                .Json(new OpenInspection(assetId, openedAt))
                .ToUrl(OpenEnpoint);

            x.StatusCodeShouldBe(StatusCodes.Status412PreconditionFailed);            

            x.WithClaim(new Claim("user-id", user.Id.ToString()));
        });

        //then
        var result = initial.ReadAsJson<ProblemDetails>()!;
        result.Status.ShouldBe(StatusCodes.Status412PreconditionFailed);
        result.Detail.ShouldBe(GetAssetNotExistsErrorDetail(assetId));
    }
}