namespace TajneedApi.Domain.Exceptions;

public enum ExceptionCodes
{
    MemberRequestIsNull = 1,
    AccessDeniedToApproveRequests,
    UnSupportedActionType,
    UnSupportedFileExportFormat,
    DocumentExportListIsNull,
    MemberAssociatedWithUpdateCaseNotFound,
    MemberMismatchWithPrimaryAccount
}