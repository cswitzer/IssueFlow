namespace IssueFlow.Application.Profiles;

public class ProfileService : IProfileService
{
    // TODO create an interface here and implement it in the Infrastructure layer
    // for db access

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
