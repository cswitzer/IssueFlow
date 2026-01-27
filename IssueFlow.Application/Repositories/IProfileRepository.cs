using IssueFlow.Application.Profiles.Dtos;

namespace IssueFlow.Application.Repositories;

public interface IProfileRepository
{
    Task<ReadProfileDto> CreateProfileAsync(CreateProfileDto createProfileDto);
    Task<ReadProfileDto?> ReadProfileAsync(Guid id);
    Task<IReadOnlyList<ReadProfileDto>> ReadAllProfilesAsync();
    Task<ReadProfileDto?> UpdateProfileAsync(Guid id, UpdateProfileDto updateProfileDto);
    Task<ReadProfileDto?> DeleteProfileAsync(Guid id);
}
