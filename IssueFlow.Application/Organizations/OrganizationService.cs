using IssueFlow.Application.Organizations.Dtos;
using IssueFlow.Application.Repositories;

namespace IssueFlow.Application.Organizations;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository _organizationRepository;

    public OrganizationService(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<ReadOrganizationDto> CreateOrganizationAsync(CreateOrganizationDto createOrganizationDto)
    {
        return await _organizationRepository.CreateOrganizationAsync(createOrganizationDto);
    }

    public async Task<ReadOrganizationDto?> DeleteOrganizationAsync(Guid id)
    {
        return await _organizationRepository.DeleteOrganizationAsync(id);
    }

    public async Task<IReadOnlyList<ReadOrganizationDto>> GetAllOrganizationsAsync(int page = 1, int pageSize = 15)
    {
        return await _organizationRepository.ReadAllOrganizationsAsync(page, pageSize);
    }

    public async Task<ReadOrganizationDto?> GetOrganizationAsync(Guid id)
    {
        return await _organizationRepository.ReadOrganizationAsync(id);
    }

    public async Task<IReadOnlyList<ReadOrganizationDto>> GetOrganizationsByProfileOwner(Guid profileOwnerId, int page = 1, int pageSize = 15)
    {
        return await _organizationRepository.ReadOrganizationsByProfileOwner(profileOwnerId, page: page, pageSize: pageSize);
    }

    public async Task<ReadOrganizationDto?> UpdateOrganizationAsync(Guid id, UpdateOrganizationDto updateOrganizationDto)
    {
        return await _organizationRepository.UpdateOrganizationAsync(id, updateOrganizationDto);
    }
}
