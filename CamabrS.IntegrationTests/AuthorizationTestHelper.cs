namespace CamabrS.IntegrationTests;
internal static class AuthorizationScenarioExtensions
{
    private const string Issuer = "https://dev-g4eshbtmrjet51zm.eu.auth0.com/";   

    public static void WithClaims(this Scenario scenario, TestUser testUser)
    {
        scenario.WithClaim(new Claim("user-id", testUser.UserId.ToString()));

        scenario.WithClaim(new Claim("scope", testUser.Permissions, null, Issuer));
    }
}

public class TestUser(string Permissions, Guid UserId = default)
{    
    public Guid UserId { get; } = UserId;
    public string Permissions { get; } = Permissions;

    public static TestUser CreateSuperuser(Guid userId = default) =>
        new (UserRights.WithSuperuserRights(), userId);

    public static readonly TestUser SuperuserWithLockHoldingId = CreateSuperuser(BaselineData.LockHoldingSpecialistId);
    public static readonly TestUser Superuser = CreateSuperuser(Guid.NewGuid());
}

internal sealed class UserRights()
{
    public static string WithSuperuserRights() =>
          "can:open can:assign can:close can:review can:complete";
}