using CamabrS.API.Core.Http;
using CamabrS.API.Inspection;
using CamabrS.API.Inspection.GettingDetails;
using CamabrS.API.Inspection.Opening;
using Microsoft.AspNetCore.Mvc;
using static CamabrS.API.Inspection.Opening.OpenEndpoints;

namespace CamabrS.IntegrationTests.Inspection;
public sealed class OpenInspectionTests : IntegrationContext
{
    public OpenInspectionTests(AppFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task Open_a_new_Inspection_should_succeed()
    {
        var user = new User(Guid.NewGuid());
        var openedAt = DateTimeOffset.UtcNow;
        
        var initial = await Host.Scenario(x =>
        {            
            x.Post
                .Json(new OpenInspection(BaselineData.DefaultTestAssetId, openedAt))
                .ToUrl(OpenEnpoint);

            x.StatusCodeShouldBe(201);

            x.WithClaim(new Claim("user-id", user.Id.ToString()));            
        });

        var inspectionId = initial.ReadAsJson<NewInspectionOpenedResponse>()!.InspectionId;

        using var session = Store.LightweightSession();
        var events = await session.Events.FetchStreamAsync(inspectionId);
        var opened = events[0].Data.ShouldBeOfType<InspectionOpened>();
       
        opened.AssetId.ShouldBe(BaselineData.DefaultTestAssetId);
        opened.OpenedBy.ShouldBe(user.Id);
        opened.OpenedAt.ShouldBe(openedAt);
        
        var inspectionDetail = await session.Query<InspectionDetails>().SingleOrDefaultAsync(i => i.Id == inspectionId);
        inspectionDetail.ShouldNotBeNull();
        inspectionDetail.AssignedSpecialists.Length.ShouldBe(0);
    }

    [Fact]
    public async Task Open_a_new_Inspection_with_nonexistent_Asset_should_fail()
    {
        var assetId = Guid.NewGuid();
        var openedAt = DateTimeOffset.UtcNow;

        var user = new User(Guid.NewGuid());

        var initial = await Host.Scenario(x =>
        {
            x.Post
                .Json(new OpenInspection(assetId, openedAt))
                .ToUrl(OpenEnpoint);

            x.StatusCodeShouldBe(403);            

            x.WithClaim(new Claim("user-id", user.Id.ToString()));
        });

        var result = initial.ReadAsJson<ProblemDetails>()!;
        result.Status.ShouldBe(403);
        result.Detail.ShouldBe(GetAssetNotExistsErrorDetail(assetId));
    }
}