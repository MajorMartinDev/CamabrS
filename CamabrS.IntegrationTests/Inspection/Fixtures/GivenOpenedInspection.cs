﻿using CamabrS.API.Inspection.GettingDetails;

namespace CamabrS.IntegrationTests.Inspection.Fixtures;
public class GivenOpenedInspection(AppFixture fixture) : IntegrationContext(fixture), IAsyncLifetime
{
    public override async Task InitializeAsync() =>
        Inspection = await Host.OpenedInspection(TestUser.CreateSuperuser());

    public InspectionDetails Inspection { get; protected set; } = default!;
}