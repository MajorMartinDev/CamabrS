using CamabrS.IntegrationTests.Inspection.Fixtures;

namespace CamabrS.IntegrationTests.Inspection;
public sealed class RequiredAndPreconditionTests(AppFixture fixture) : IntegrationContext(fixture)
{
    private readonly Lorem lorem = new();
    private readonly Internet internet = new();

    [Fact]
    public async Task OpenEndpoint_Before()
    {
        //when
        var result = await Host.OpenInspection(
            Guid.NewGuid(), DateTimeOffset.Now);

        //then
        result.Context.Response.StatusCode.ShouldBe(StatusCodes.Status412PreconditionFailed);        
    }

    [Fact]
    public async Task AssignEndpoint_Required()
    {
        //when
        var result = await Host.AssignSpecialist(
            Guid.NewGuid(), 0, BaselineData.LockHoldingSpecialist, DateTimeOffset.Now);

        //then
        result.Context.Response.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task AssignEndpoint_Before()
    {
        //when
        var result = await Host.AssignSpecialist(
            Guid.NewGuid(), 0, Guid.NewGuid(), DateTimeOffset.Now);

        //then
        result.Context.Response.StatusCode.ShouldBe(StatusCodes.Status412PreconditionFailed);
    }

    [Fact]
    public async Task UnassignEndpoint()
    {
        //when
        var result = await Host.UnassignSpecialist(
            Guid.NewGuid(), 0, Guid.NewGuid(), DateTimeOffset.Now);

        //then
        result.Context.Response.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task LockEndpoint()
    {
        //when
        var result = await Host.LockInspection(
            Guid.NewGuid(), Guid.NewGuid(), 0, DateTimeOffset.Now);

        //then
        result.Context.Response.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task UnlockEndpoint()
    {
        //when
        var result = await Host.UnlockInspection(
            Guid.NewGuid(), 0, DateTimeOffset.Now);

        //then
        result.Context.Response.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task SubmitEndpoint()
    {
        //when
        var result = await Host.SubmitInspection(
            Guid.NewGuid(), 0, Guid.NewGuid(), DateTimeOffset.Now);

        //then
        result.Context.Response.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task SignEndpoint()
    {
        //when
        var result = await Host.SignInspection(
            Guid.NewGuid(), 0, internet.Url(), DateTimeOffset.Now);

        //then
        result.Context.Response.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task CloseEndpoint()
    {
        //when
        var result = await Host.CloseInspection(
            Guid.NewGuid(), 0, DateTimeOffset.Now);

        //then
        result.Context.Response.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task ReviewEndpoint()
    {
        //when
        var result = await Host.ReviewInspection(
            Guid.NewGuid(), 0, API.Inspection.ReviewVerdict.Approved, lorem.Paragraph(), DateTimeOffset.Now);

        //then
        result.Context.Response.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task ReopenEndpoint()
    {
        //when
        var result = await Host.ReopenInspection(
            Guid.NewGuid(), 0, DateTimeOffset.Now);

        //then
        result.Context.Response.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task CompleteEndpoint()
    {
        //when
        var result = await Host.CompleteInspection(
            Guid.NewGuid(), 0, DateTimeOffset.Now);

        //then
        result.Context.Response.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
    }
}