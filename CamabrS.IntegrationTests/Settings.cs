using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestFramework("CamabrS.IntegrationTests.AssemblyFixture", "CamabrS.IntegrationTests")]

namespace CamabrS.IntegrationTests;

public sealed class AssemblyFixture : XunitTestFramework
{
    public AssemblyFixture(IMessageSink messageSink)
        : base(messageSink)
    {
        OaktonEnvironment.AutoStartHost = true;
    }
}