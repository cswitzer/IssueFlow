using IssueFlow.Application.Auth;
using Microsoft.AspNetCore.Identity;

namespace IssueFlow.Infrastructure.Identity;

// implement these and inject into the service provider
public class IdentityAuthService : IAuthService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public IdentityAuthService(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IPasswordHasher<ApplicationUser> passwordHasher,
        IJwtTokenService jwtTokenService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthResult> LoginUser(LoginRequest request)
    {
        var result = await _signInManager.PasswordSignInAsync(
            request.Email,
            request.Password,
            isPersistent: false,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                throw new InvalidOperationException("User not found after successful login.");

            var roles = await _userManager.GetRolesAsync(user);
            var jwtUser = new JwtUserDto
            {
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Roles = roles.ToList()
            };
            var token = _jwtTokenService.GenerateAccessToken(jwtUser);

            return new AuthResult
            {
                Success = true,
                Errors = new List<string>(),
                AccessToken = token
            };
        }

        return new AuthResult
        {
            Success = false,
            Errors = new List<string>() { "Invalid login attempt." }
        };
    }

    public async Task<AuthResult> RegisterUser(RegisterRequest request)
    {
        var newUser = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.Email,
            SecurityQuestion = request.SecurityQuestion
        };

        newUser.SecurityAnswerHash = _passwordHasher.HashPassword(newUser, request.SecurityAnswer);

        var result = await _userManager.CreateAsync(newUser, request.Password);

        if (result.Succeeded)
        {
            var jwtUser = new JwtUserDto
            {
                UserId = newUser.Id,
                Email = newUser.Email,
                UserName = newUser.UserName,
                Roles = new List<string>()
            };
            var token = _jwtTokenService.GenerateAccessToken(jwtUser);

            return new AuthResult
            {
                Success = true,
                Errors = new List<string>(),
                AccessToken = token
            };
        }

        return new AuthResult
        {
            Success = false,
            Errors = result.Errors.Select(e => e.Description).ToList()
        };
    }
}
