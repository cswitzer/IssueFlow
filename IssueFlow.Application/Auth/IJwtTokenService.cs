namespace IssueFlow.Application.Auth;

public interface IJwtTokenService
{
    public string GenerateAccessToken(JwtUserDto jwtUserDto);
}
