﻿using CamabrS.API.Specialist.GettingDetails;
using Marten.Events.Projections;

namespace CamabrS.API.Specialist;

public static class Configuration
{
    public static StoreOptions ConfigureSpecialists(this StoreOptions options)
    {
        options.Projections.LiveStreamAggregation<Specialist>();
        options.Projections.Add<SpecialistDetailsProjection>(ProjectionLifecycle.Inline);

        return options;
    }
}