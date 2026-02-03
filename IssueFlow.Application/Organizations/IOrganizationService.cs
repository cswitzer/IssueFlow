using IssueFlow.Application.Organizations.Dtos;

namespace IssueFlow.Application.Organizations;

public interface IOrganizationService
{
    Task<ReadOrganizationDto> CreateOrganizationAsync(CreateOrganizationDto createOrganizationDto);
    Task<ReadOrganizationDto?> GetOrganizationAsync(Guid id);
    Task<IReadOnlyList<ReadOrganizationDto>> GetOrganizationsByProfileOwner(Guid profileOwnerId, int page = 1, int pageSize = 15);
    Task<IReadOnlyList<ReadOrganizationDto>> GetAllOrganizationsAsync(int page = 1, int pageSize = 15);
    Task<ReadOrganizationDto?> UpdateOrganizationAsync(Guid id, UpdateOrganizationDto updateOrganizationDto);
    Task<ReadOrganizationDto?> DeleteOrganizationAsync(Guid id);
}
