﻿using CamabrS.API.Core.Http;
using CamabrS.API.Specialist.GettingDetails;
using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Mvc;
using Wolverine;
using Wolverine.Attributes;
using Wolverine.Http;
using Wolverine.Marten;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace CamabrS.API.Inspection.Assigning;

public sealed record AssignSpecialist(Guid InspectionId, int Version, Guid SpecialistId)
{
    public sealed class AssignSpecialistValidator : AbstractValidator<AssignSpecialist>
    {
        public AssignSpecialistValidator()
        {
            RuleFor(x => x.InspectionId).NotEmpty().NotNull();
            RuleFor(x => x.SpecialistId).NotEmpty().NotNull();
        }
    }
};

public static class AssignEndpoints
{
    public const string AssignEnpoint = "/api/inspections/assign";

    public static string GetAssetNotExistsErrorDetail(Guid specialistId)
        => $"Asset with id {specialistId} does not exist!";

    [WolverineBefore]
    public static async Task<ProblemDetails> ValidateInspectionState(
        AssignSpecialist command,
        IDocumentSession session)
    {
        var (inspectionId, _, specialistId) = command;

        var specialistExists = await session.Query<SpecialistDetails>().AnyAsync(x => x.Id == specialistId);
        
        return specialistExists
            ? WolverineContinue.NoProblems
            : new ProblemDetails { Detail = GetAssetNotExistsErrorDetail(specialistId) };
    }
    
    [WolverinePost(AssignEnpoint), AggregateHandler]
    public static (IResult, Events, OutgoingMessages) Post(
        AssignSpecialist command,
        Inspection inspection,
        DateTimeOffset now,
        User user)
    {
        var (inspectionId, version, specialistId) = command;

        if (inspection.Status != InspectionStatus.Opened)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Opened, inspectionId));

        var events = new Events();
        var messages = new OutgoingMessages();
        
        events.Add(new Inspection.AssignSpecialist(inspectionId, user.Id, specialistId, now));

        //TODO add missing business logic to check if the specialist can be assigned based on certifications
        //TODO consider saving or logging information if the specialist could not be assigned based in missing certification
        events.Add(new SpecialistAssigned(inspectionId, user.Id, specialistId, now));

        //TODO send off message to notify Specialist that they got assigned an inspection

        return (Ok(version + events.Count), events, messages);
    }    
}