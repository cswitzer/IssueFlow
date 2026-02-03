using IssueFlow.Application.Organizations.Dtos;

namespace IssueFlow.Application.Repositories;

public interface IOrganizationRepository
{
    Task<ReadOrganizationDto> CreateOrganizationAsync(CreateOrganizationDto createOrganizationDto);
    Task<ReadOrganizationDto?> ReadOrganizationAsync(Guid id);
    Task<IReadOnlyList<ReadOrganizationDto>> ReadOrganizationsByProfileOwner(Guid profileOwnerId, int page = 1, int pageSize = 15);
    Task<IReadOnlyList<ReadOrganizationDto>> ReadAllOrganizationsAsync(int page = 1, int pageSize = 15);
    Task<ReadOrganizationDto?> UpdateOrganizationAsync(Guid id, UpdateOrganizationDto updateOrganizationDto);
    Task<ReadOrganizationDto?> DeleteOrganizationAsync(Guid id);
}
