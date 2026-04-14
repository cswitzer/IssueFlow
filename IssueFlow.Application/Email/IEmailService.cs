namespace IssueFlow.Application.Email;

public interface IEmailService
{
    // For now, I will group all email-related operations under this service. If the need arises, I will
    // consider splitting it into multiple services, such as IIssueEmailService, IUserEmailService, etc.
    Task SendEmailAsync(string to, string subject, string body);

    // Account-related emails
    Task SendWelcomeEmailAsync(string email, string username);

    // Issue-related emails (some ideas to expand on in the future)
    //Task SendIssueAssignmentEmailAsync(string assigneeEmail, string assigneeName, string issueTitle, string projectName);
    //Task SendIssueStatusChangeEmailAsync(string recipientEmail, string recipientName, string issueTitle, string oldStatus, string newStatus, string projectName);

}
