using IssueFlow.Application.Profiles.Dtos;

namespace IssueFlow.Application.Profiles;

public interface IProfileService
{
    Task<ProfileDto> CreateProfileAsync(CreateProfileDto createProfileDto);
    Task<ProfileDto?> DeactivateProfileAsync(string userId);
    Task<ProfileDto?> GetProfileAsync(string userId);
    Task<ProfileDto> UpdateProfileAsync(UpdateProfileDto updateProfileDto);
}
