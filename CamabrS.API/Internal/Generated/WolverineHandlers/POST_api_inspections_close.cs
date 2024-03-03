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
    // START: POST_api_inspections_close
    public class POST_api_inspections_close : Wolverine.Http.HttpHandler
    {
        private readonly Wolverine.Http.WolverineHttpOptions _wolverineHttpOptions;
        private readonly FluentValidation.IValidator<CamabrS.API.Inspection.Closeing.CloseInspection> _validator;
        private readonly Wolverine.Marten.Publishing.OutboxedSessionFactory _outboxedSessionFactory;
        private readonly Wolverine.Http.FluentValidation.IProblemDetailSource<CamabrS.API.Inspection.Closeing.CloseInspection> _problemDetailSource;
        private readonly Wolverine.Runtime.IWolverineRuntime _wolverineRuntime;

        public POST_api_inspections_close(Wolverine.Http.WolverineHttpOptions wolverineHttpOptions, FluentValidation.IValidator<CamabrS.API.Inspection.Closeing.CloseInspection> validator, Wolverine.Marten.Publishing.OutboxedSessionFactory outboxedSessionFactory, Wolverine.Http.FluentValidation.IProblemDetailSource<CamabrS.API.Inspection.Closeing.CloseInspection> problemDetailSource, Wolverine.Runtime.IWolverineRuntime wolverineRuntime) : base(wolverineHttpOptions)
        {
            _wolverineHttpOptions = wolverineHttpOptions;
            _validator = validator;
            _outboxedSessionFactory = outboxedSessionFactory;
            _problemDetailSource = problemDetailSource;
            _wolverineRuntime = wolverineRuntime;
        }



        public override async System.Threading.Tasks.Task Handle(Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            var messageContext = new Wolverine.Runtime.MessageContext(_wolverineRuntime);
            // Reading the request body via JSON deserialization
            var (command, jsonContinue) = await ReadJsonAsync<CamabrS.API.Inspection.Closeing.CloseInspection>(httpContext);
            if (jsonContinue == Wolverine.HandlerContinuation.Stop) return;
            
            // Execute FluentValidation validators
            var result1 = await Wolverine.Http.FluentValidation.Internals.FluentValidationHttpExecutor.ExecuteOne<CamabrS.API.Inspection.Closeing.CloseInspection>(_validator, _problemDetailSource, command).ConfigureAwait(false);

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


            await using var documentSession = _outboxedSessionFactory.OpenSession(messageContext);
            var eventStore = documentSession.Events;
            
            // Loading Marten aggregate
            var eventStream = await eventStore.FetchForWriting<CamabrS.API.Inspection.Inspection>(command.InspectionId, command.Version, httpContext.RequestAborted).ConfigureAwait(false);

            
            // The actual HTTP request handler execution
            (var apiResponse_response, var events, var outgoingMessages) = CamabrS.API.Inspection.Closeing.CloseEndpoints.Post(command, eventStream.Aggregate, user);

            if (events != null)
            {
                
                // Capturing any possible events returned from the command handlers
                eventStream.AppendMany(events);

            }

            
            // Outgoing, cascaded message
            await messageContext.EnqueueCascadingAsync(outgoingMessages).ConfigureAwait(false);

            await documentSession.SaveChangesAsync(httpContext.RequestAborted).ConfigureAwait(false);
            // Writing the response body to JSON because this was the first 'return variable' in the method signature
            await WriteJsonAsync(httpContext, apiResponse_response);
            
            // Making sure there is at least one call to flush outgoing, cascading messages
            await messageContext.FlushOutgoingMessagesAsync().ConfigureAwait(false);

        }

    }

    // END: POST_api_inspections_close
    
    
}

