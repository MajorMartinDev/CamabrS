using CamabrS.API.Asset;
using CamabrS.API.Core.Http;
using CamabrS.API.Inspection;
using CamabrS.API.Specialist;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ApplyOaktonExtensions();

builder.Services
    .AddDefaultExceptionHandler(
        (exception, _) => exception switch
        {
            ConcurrencyException => exception.MapToProblemDetails(StatusCodes.Status412PreconditionFailed),
            _ => null
        })
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddMarten(opts =>
{
    var connectionString = builder.Configuration.GetConnectionString("camabrs_dev");
    opts.Connection(connectionString!);

    opts.AutoCreateSchemaObjects = AutoCreate.All;

    opts.ConfigureInspections();
    opts.ConfigureAssets();
    opts.ConfigureSpecialists();
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

namespace CamabrS.Api
{    
    public partial class Program
    {
    }
}