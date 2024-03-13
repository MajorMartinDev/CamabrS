using CamabrS.API.Inspection.Assigning;
using CamabrS.API.Inspection.Closeing;
using CamabrS.API.Inspection.Completing;
using CamabrS.API.Inspection.Locking;
using CamabrS.API.Inspection.Reopening;
using CamabrS.API.Inspection.Reviewing;
using CamabrS.API.Inspection.Signing;
using CamabrS.API.Inspection.Submitting;

namespace CamabrS.API.Inspection;

public static class NextInspectionSteps
{
    public static List<string> GetNextSteps(InspectionStatus newStatus, ReviewVerdict verdict = default)
    {
        List<string> result = [];

        return (newStatus, verdict)
            switch
            {
                (InspectionStatus.Opened, _) => [AssignEndpoints.AssignEnpoint],
                (InspectionStatus.Assigned, _) => [UnassignEndpoints.UnassignEnpoint, AssignEndpoints.AssignEnpoint, LockEndpoints.LockEnpoint],
                (InspectionStatus.Locked, _) => [UnlockEndpoints.UnlockEnpoint, SubmitEndpoints.SubmitEnpoint],
                (InspectionStatus.Submitted, _) => [SubmitEndpoints.SubmitEnpoint, SignEndpoints.SignEnpoint],
                (InspectionStatus.Signed, _) => [CloseEndpoints.CloseEnpoint],
                (InspectionStatus.Closed, _) => [ReviewEndpoints.ReviewEnpoint],
                (InspectionStatus.Reviewed, ReviewVerdict.Disapproved) => [ReopenEndpoints.ReopenEnpoint, CompleteEndpoints.CompleteEnpoint],
                (InspectionStatus.Reviewed, _) => [CompleteEndpoints.CompleteEnpoint],
                (InspectionStatus.Completed, _) => [],
                (_ , _) => []
            };
    }
}