using Marten.Events;
using Marten.Events.Aggregation;

namespace CamabrS.API.Inspection.GettingDetails;

public sealed record InspectionDetails(Guid Id, Guid[] AssignedSpecialists);

public sealed class InspectionDetailsProjection: SingleStreamProjection<InspectionDetails>
{
    public static InspectionDetails Create(IEvent<InspectionOpened> opened) =>
        new(opened.Id, []);

    public static InspectionDetails Apply(SpecialistAssigned specialistAssigned, InspectionDetails current) =>
        current with 
        { 
            AssignedSpecialists = current.AssignedSpecialists.Union
            (
                new[] { specialistAssigned.InspectionId }
            ).ToArray()
        };
}