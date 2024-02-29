namespace CamabrS.API.Specialist.GettingDetails;

public sealed record SpecialistDetails(Guid Id);

public sealed class SpecialistDetailsProjection : SingleStreamProjection<SpecialistDetails>
{
    public static SpecialistDetails Create(SpecialistCreated created) =>
        new(created.Id);
}