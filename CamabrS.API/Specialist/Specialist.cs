namespace CamabrS.API.Specialist;

public sealed record Specialist(Guid Id)
{
    public static Specialist Create(SpecialistCreated Created) =>
        new(Created.Id);
}

public sealed record SpecialistCreated(Guid Id);