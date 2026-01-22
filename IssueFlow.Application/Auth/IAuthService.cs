namespace IssueFlow.Application.Auth;

public interface IAuthService
{
    Task<AuthResult> RegisterUser(RegisterRequest request);
    Task<AuthResult> LoginUser(LoginRequest request);
}
