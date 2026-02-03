using IssueFlow.Application.Profiles.Dtos;

namespace IssueFlow.Application.Repositories;

public interface IProfileRepository
{
    Task<ReadProfileDto> CreateProfileAsync(CreateProfileDto createProfileDto);
    Task<ReadProfileDto?> ReadProfileAsync(Guid id);
    Task<IReadOnlyList<ReadProfileDto>> ReadProfilesByOrganization(Guid organizationId, int page = 1, int pageSize = 15);
    Task<IReadOnlyList<ReadProfileDto>> ReadAllProfilesAsync(int page = 1, int pageSize = 15);
    Task<ReadProfileDto?> UpdateProfileAsync(Guid id, UpdateProfileDto updateProfileDto);
    Task<ReadProfileDto?> DeleteProfileAsync(Guid id);
}
