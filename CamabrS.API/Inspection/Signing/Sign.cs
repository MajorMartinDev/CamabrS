﻿using Wolverine.Http;
using Wolverine.Marten;
using Wolverine;
using static Microsoft.AspNetCore.Http.TypedResults;
using CamabrS.API.Core.Http;
using FluentValidation;

namespace CamabrS.API.Inspection.Signing;

public sealed record SignInspection(Guid InspectionId, int Version, string SignatureLink, DateTimeOffset SignedAt)
{
    public sealed class SignInspectionValidator : AbstractValidator<SignInspection>
    {
        public SignInspectionValidator()
        {
            RuleFor(x => x.InspectionId).NotEmpty().NotNull();
            RuleFor(x => x.SignatureLink).NotEmpty().NotNull();
            //TODO add a regex rule for signature link 
        }
    }
};

public static class SignEndpoints 
{
    public const string SignEnpoint = "/api/inspections/sign";

    [WolverinePost(SignEnpoint), AggregateHandler]
    public static (IResult, Events, OutgoingMessages) Post(
        SignInspection command,
        Inspection inspection,        
        User user)
    {
        var (inspectionId, version, signatureLink, signedAt) = command;

        if (inspection.Status != InspectionStatus.Submitted)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Submitted, inspectionId));

        var events = new Events();
        var messages = new OutgoingMessages();

        events.Add(new Inspection.SignInspection(inspectionId, user.Id, signatureLink, signedAt));

        events.Add(new InspectionSigned(inspectionId, user.Id, signatureLink, signedAt));

        return (Ok(version + events.Count), events, messages);
    }    
}