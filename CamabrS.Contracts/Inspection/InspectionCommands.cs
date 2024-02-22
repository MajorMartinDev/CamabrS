namespace CamabrS.Contracts.Inspection;

public sealed record OpenInspection(Guid ObjectId);
public sealed record AssignSpecialist(Guid InspectionId, Guid SpecialistId);
public sealed record UnassignSpecialist();
public sealed record LockInspection();
public sealed record UnlockInspection();
public sealed record SubmitInspectionResult();
public sealed record SignInspection();
public sealed record CloseInspection();
public sealed record ReviewInspection();
public sealed record ReopenInspection();
public sealed record CompleteInspection();