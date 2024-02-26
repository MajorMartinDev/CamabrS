﻿using CamabrS.API.Asset.GettingDetails;
using Marten;
using Marten.Events.Projections;

namespace CamabrS.API.Asset;

public static class Configuration
{
    public static StoreOptions ConfigureAssets(this StoreOptions options)
    {
        options.Projections.LiveStreamAggregation<Asset>();
        options.Projections.Add<AssetDetailsProjection>(ProjectionLifecycle.Inline);        

        return options;
    }
}