using IssueFlow.Application.Profiles.Dtos;

namespace IssueFlow.Application.Profiles;

public interface IProfileService
{
    Task<ReadProfileDto> CreateProfileAsync(CreateProfileDto createProfileDto);
    Task<ReadProfileDto?> GetProfileAsync(Guid id);
    Task<IReadOnlyList<ReadProfileDto>> GetAllProfilesAsync();
    Task<ReadProfileDto?> UpdateProfileAsync(Guid id, UpdateProfileDto updateProfileDto);
    Task<ReadProfileDto?> DeleteProfileAsync(Guid id);
}
