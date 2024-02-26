// <auto-generated/>
#pragma warning disable
using FluentValidation;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using Wolverine.Http;
using Wolverine.Http.FluentValidation;
using Wolverine.Marten.Publishing;
using Wolverine.Runtime;

namespace Internal.Generated.WolverineHandlers
{
    // START: POST_api_inspections_assign
    public class POST_api_inspections_assign : Wolverine.Http.HttpHandler
    {
        private readonly Wolverine.Http.WolverineHttpOptions _wolverineHttpOptions;
        private readonly Wolverine.Runtime.IWolverineRuntime _wolverineRuntime;
        private readonly Wolverine.Http.FluentValidation.IProblemDetailSource<CamabrS.API.Inspection.Assigning.AssignSpecialist> _problemDetailSource;
        private readonly Wolverine.Marten.Publishing.OutboxedSessionFactory _outboxedSessionFactory;
        private readonly FluentValidation.IValidator<CamabrS.API.Inspection.Assigning.AssignSpecialist> _validator;

        public POST_api_inspections_assign(Wolverine.Http.WolverineHttpOptions wolverineHttpOptions, Wolverine.Runtime.IWolverineRuntime wolverineRuntime, Wolverine.Http.FluentValidation.IProblemDetailSource<CamabrS.API.Inspection.Assigning.AssignSpecialist> problemDetailSource, Wolverine.Marten.Publishing.OutboxedSessionFactory outboxedSessionFactory, FluentValidation.IValidator<CamabrS.API.Inspection.Assigning.AssignSpecialist> validator) : base(wolverineHttpOptions)
        {
            _wolverineHttpOptions = wolverineHttpOptions;
            _wolverineRuntime = wolverineRuntime;
            _problemDetailSource = problemDetailSource;
            _outboxedSessionFactory = outboxedSessionFactory;
            _validator = validator;
        }



        public override async System.Threading.Tasks.Task Handle(Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            var messageContext = new Wolverine.Runtime.MessageContext(_wolverineRuntime);
            // Reading the request body via JSON deserialization
            var (command, jsonContinue) = await ReadJsonAsync<CamabrS.API.Inspection.Assigning.AssignSpecialist>(httpContext);
            if (jsonContinue == Wolverine.HandlerContinuation.Stop) return;
            
            // Execute FluentValidation validators
            var result1 = await Wolverine.Http.FluentValidation.Internals.FluentValidationHttpExecutor.ExecuteOne<CamabrS.API.Inspection.Assigning.AssignSpecialist>(_validator, _problemDetailSource, command).ConfigureAwait(false);

            // Evaluate whether or not the execution should be stopped based on the IResult value
            if (!(result1 is Wolverine.Http.WolverineContinue))
            {
                await result1.ExecuteAsync(httpContext).ConfigureAwait(false);
                return;
            }


            (var user, var problemDetails2) = CamabrS.API.Core.Http.UserDetectionMiddleware.Load(httpContext.User);
            // Evaluate whether the processing should stop if there are any problems
            if (!(ReferenceEquals(problemDetails2, Wolverine.Http.WolverineContinue.NoProblems)))
            {
                await WriteProblems(problemDetails2, httpContext).ConfigureAwait(false);
                return;
            }


            System.DateTimeOffset now = default;
            System.DateTimeOffset.TryParse(httpContext.Request.Query["now"], System.Globalization.CultureInfo.InvariantCulture, out now);
            await using var documentSession = _outboxedSessionFactory.OpenSession(messageContext);
            var eventStore = documentSession.Events;
            
            // Loading Marten aggregate
            var eventStream = await eventStore.FetchForWriting<CamabrS.API.Inspection.Inspection>(command.InspectionId, command.Version, httpContext.RequestAborted).ConfigureAwait(false);

            var problemDetails3 = await CamabrS.API.Inspection.Assigning.AssignEndpoints.ValidateInspectionState(command, documentSession).ConfigureAwait(false);
            // Evaluate whether the processing should stop if there are any problems
            if (!(ReferenceEquals(problemDetails3, Wolverine.Http.WolverineContinue.NoProblems)))
            {
                await WriteProblems(problemDetails3, httpContext).ConfigureAwait(false);
                return;
            }


            
            // The actual HTTP request handler execution
            (var result, var events, var outgoingMessages) = CamabrS.API.Inspection.Assigning.AssignEndpoints.Post(command, eventStream.Aggregate, now, user);

            if (events != null)
            {
                
                // Capturing any possible events returned from the command handlers
                eventStream.AppendMany(events);

            }

            
            // Outgoing, cascaded message
            await messageContext.EnqueueCascadingAsync(outgoingMessages).ConfigureAwait(false);

            await documentSession.SaveChangesAsync(httpContext.RequestAborted).ConfigureAwait(false);
            
            // Commit any outstanding Marten changes
            await documentSession.SaveChangesAsync(httpContext.RequestAborted).ConfigureAwait(false);

            
            // Have to flush outgoing messages just in case Marten did nothing because of https://github.com/JasperFx/wolverine/issues/536
            await messageContext.FlushOutgoingMessagesAsync().ConfigureAwait(false);

            await result.ExecuteAsync(httpContext).ConfigureAwait(false);
        }

    }

    // END: POST_api_inspections_assign
    
    
}

