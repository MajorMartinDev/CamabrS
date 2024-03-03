namespace CamabrS.IntegrationTests;

public sealed class AppFixture : IAsyncLifetime
{
    public IAlbaHost Host { get; private set; }

    public Task DisposeAsync()
    {
        if (Host != null)
        {
            return Host.DisposeAsync().AsTask();
        }

        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
        OaktonEnvironment.AutoStartHost = true;

        Host = await AlbaHost.For<Program>(x =>
        {
            x.ConfigureServices(services =>
            {
                // We'll be using Rabbit MQ messaging later...
                services.DisableAllExternalWolverineTransports();

                // We're going to establish some baseline data
                // for testing
                services.InitializeMartenWith<BaselineData>();
            });
        }, new AuthenticationStub());
    }
}