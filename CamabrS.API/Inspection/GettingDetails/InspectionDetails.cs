using Marten.Events.Aggregation;

namespace CamabrS.API.Inspection.GettingDetails;

public sealed record InspectionDetails(Guid Id);

public sealed class InspectionDetailsProjection: SingleStreamProjection<InspectionDetails>
{
    public static InspectionDetails Create(InspectionOpened opened) =>
        new(opened.InspectionId);

    public static InspectionDetails Apply(SpecialistAssigned specialistAssigned) =>
        new(specialistAssigned.InspectionId);
}