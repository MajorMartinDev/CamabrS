namespace CamabrS.Contracts.Inspection;

public sealed record OpenInspection(Guid ObjectId);
public sealed record AssignSpecialist(Guid InspectionId, int Version, Guid SpecialistId);
public sealed record UnassignSpecialist(Guid InspectionId, int Version, Guid SpecialistId);
public sealed record LockInspection(Guid InspectionId, int Version);
public sealed record UnlockInspection(Guid InspectionId, int Version);
public sealed record SubmitInspectionResult(Guid InspectionId, int Version, Guid FormId);
public sealed record SignInspection(Guid InspectionId, int Version, string SignatureLink);
public sealed record CloseInspection(Guid InspectionId, int Version);
public sealed record ReviewInspection(Guid InspectionId, int Version, bool Verdict, string Summary);
public sealed record ReopenInspection(Guid InspectionId, int Version);
public sealed record CompleteInspection(Guid InspectionId, int Version);