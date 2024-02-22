using CamabrS.API;
using CamabrS.API.Inspection.GetInspectionHistory;
using JasperFx.CodeGeneration;
using JasperFx.Core;
using Marten;
using Marten.Events.Projections;
using Marten.Exceptions;
using Npgsql;
using Oakton;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.FluentValidation;
using Wolverine.Http;
using Wolverine.Http.FluentValidation;
using Wolverine.Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ApplyOaktonExtensions();

builder.Services.AddMarten(opts =>
{
    var connectionString = builder.Configuration.GetConnectionString("camabrs_dev");
    opts.Connection(connectionString!);

    opts.Projections.Add<InspectionHistoryTransformation>(ProjectionLifecycle.Inline);
})
    .IntegrateWithWolverine()
    .EventForwardingToWolverine(opts =>
    {

    });

builder.Host.UseWolverine(opts =>
{
    opts.CodeGeneration.TypeLoadMode = TypeLoadMode.Static;

    //durability for transient errors
    opts.OnException<NpgsqlException>().Or<MartenCommandException>()
        .RetryWithCooldown(50.Milliseconds(), 100.Milliseconds(), 250.Milliseconds());

    // Apply the validation middleware *and* discover and register
    // Fluent Validation validators
    opts.UseFluentValidation();

    // Automatic transactional middleware
    opts.Policies.AutoApplyTransactions();

    // Opt into the transactional inbox for local 
    // queues
    opts.Policies.UseDurableLocalQueues();

    // Opt into the transactional inbox/outbox on all messaging
    // endpoints
    opts.Policies.UseDurableOutboxOnAllSendingEndpoints();

    //add local queue stuff here
});


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapWolverineEndpoints(opts =>
{
    // Direct Wolverine.HTTP to use Fluent Validation
    // middleware to validate any request bodies where
    // there's a known validator (or many validators)
    opts.UseFluentValidationProblemDetailMiddleware();

    // Creates a User object in HTTP requests based on
    // the "user-id" claim
    opts.AddMiddleware(typeof(UserDetectionMiddleware));
});

app.UseHttpsRedirection();

// This is important for Wolverine/Marten diagnostics 
// and environment management
return await app.RunOaktonCommands(args);