﻿using CamabrS.API.Core.Http;
using CamabrS.API.Inspection.Closeing;

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

    public static string GetInvalidSigningAttemptErrorMessage()
        => "Inspection can only be signed by the lock holding specialist.";

    [WolverinePost(SignEnpoint), AggregateHandler]
    public static (ApiResponse, Events, OutgoingMessages) Post(
        SignInspection command,
        Inspection inspection,        
        User user)
    {
        var events = new Events();
        var messages = new OutgoingMessages();

        var (inspectionId, version, signatureLink, signedAt) = command;        

        if (inspection.Status != InspectionStatus.Submitted)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Submitted, inspectionId));

        if (user.Id != inspection.LockHoldingSpecialist)
            throw new InvalidOperationException(
                GetInvalidSigningAttemptErrorMessage());

        events.Add(new Inspection.SignInspection(inspectionId, user.Id, signatureLink, signedAt));

        events.Add(new InspectionSigned(inspectionId, user.Id, signatureLink, signedAt));

        return (
            new ApiResponse(
                (version + events.Count), 
                [CloseEndpoints.CloseEnpoint]), 
                events, messages);
    }    
}