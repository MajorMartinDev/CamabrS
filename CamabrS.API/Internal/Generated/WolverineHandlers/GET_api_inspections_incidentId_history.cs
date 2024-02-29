// <auto-generated/>
#pragma warning disable
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using Wolverine.Http;
using Wolverine.Marten.Publishing;
using Wolverine.Runtime;

namespace Internal.Generated.WolverineHandlers
{
    // START: GET_api_inspections_incidentId_history
    public class GET_api_inspections_incidentId_history : Wolverine.Http.HttpHandler
    {
        private readonly Wolverine.Http.WolverineHttpOptions _wolverineHttpOptions;
        private readonly Wolverine.Marten.Publishing.OutboxedSessionFactory _outboxedSessionFactory;
        private readonly Wolverine.Runtime.IWolverineRuntime _wolverineRuntime;

        public GET_api_inspections_incidentId_history(Wolverine.Http.WolverineHttpOptions wolverineHttpOptions, Wolverine.Marten.Publishing.OutboxedSessionFactory outboxedSessionFactory, Wolverine.Runtime.IWolverineRuntime wolverineRuntime) : base(wolverineHttpOptions)
        {
            _wolverineHttpOptions = wolverineHttpOptions;
            _outboxedSessionFactory = outboxedSessionFactory;
            _wolverineRuntime = wolverineRuntime;
        }



        public override async System.Threading.Tasks.Task Handle(Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            var messageContext = new Wolverine.Runtime.MessageContext(_wolverineRuntime);
            // Building the Marten session
            await using var querySession = _outboxedSessionFactory.QuerySession(messageContext);
            (var user, var problemDetails1) = CamabrS.API.Core.Http.UserDetectionMiddleware.Load(httpContext.User);
            // Evaluate whether the processing should stop if there are any problems
            if (!(ReferenceEquals(problemDetails1, Wolverine.Http.WolverineContinue.NoProblems)))
            {
                await WriteProblems(problemDetails1, httpContext).ConfigureAwait(false);
                return;
            }


            if (!System.Guid.TryParse((string)httpContext.GetRouteValue("incidentId"), out var incidentId))
            {
                httpContext.Response.StatusCode = 404;
                return;
            }


            
            // The actual HTTP request handler execution
            var inspectionHistoryIReadOnlyList_response = await CamabrS.API.Inspection.GettingHistory.GetHistoryEndpoints.GetHistory(incidentId, querySession, httpContext.RequestAborted).ConfigureAwait(false);

            // Writing the response body to JSON because this was the first 'return variable' in the method signature
            await WriteJsonAsync(httpContext, inspectionHistoryIReadOnlyList_response);
        }

    }

    // END: GET_api_inspections_incidentId_history
    
    
}

