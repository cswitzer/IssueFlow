using IssueFlow.Application.Profiles.Dtos;
using IssueFlow.Application.Repositories;

namespace IssueFlow.Application.Profiles;

public class ProfileService : IProfileService
{
    private readonly IProfileRepository _profileRepository;

    public ProfileService(IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    public async Task<ReadProfileDto> CreateProfileAsync(CreateProfileDto createProfileDto)
    {
        return await _profileRepository.CreateProfileAsync(createProfileDto);
    }

    public async Task<ReadProfileDto?> DeleteProfileAsync(Guid id)
    {
        return await _profileRepository.DeleteProfileAsync(id);
    }

    public async Task<IReadOnlyList<ReadProfileDto>> GetAllProfilesAsync(int page = 1, int pageSize = 15)
    {
        return await _profileRepository.ReadAllProfilesAsync(page, pageSize);
    }

    public async Task<ReadProfileDto?> GetProfileAsync(Guid id)
    {
        return await _profileRepository.ReadProfileAsync(id);
    }

    public async Task<ReadProfileDto?> UpdateProfileAsync(Guid id, UpdateProfileDto updateProfileDto)
    {
        return await _profileRepository.UpdateProfileAsync(id, updateProfileDto);
    }
}
