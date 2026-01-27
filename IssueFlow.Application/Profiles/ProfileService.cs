using IssueFlow.Application.Profiles.Dtos;

namespace IssueFlow.Application.Profiles;

public class ProfileService : IProfileService
{
    public Task<ProfileDto> CreateProfileAsync(CreateProfileDto createProfileDto)
    {
        throw new NotImplementedException();
    }

    public Task<ProfileDto?> DeactivateProfileAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<ProfileDto?> GetProfileAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<ProfileDto> UpdateProfileAsync(UpdateProfileDto updateProfileDto)
    {
        throw new NotImplementedException();
    }
}
