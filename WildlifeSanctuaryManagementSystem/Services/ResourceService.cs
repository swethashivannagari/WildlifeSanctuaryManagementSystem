using System.ComponentModel.Design;
using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Repositories;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public class ResourceService:IResourceService
    {
       
            private readonly IResourceRepository _resourceRepository;

            public ResourceService(IResourceRepository resourceRepository)
            {
                _resourceRepository = resourceRepository;
            }

            public async Task<IEnumerable<Resource>> GetAllResourcesById(int sanctuaryId)
            {
            return await _resourceRepository.GetAllResourcesById(sanctuaryId);
            }

            public async Task<Resource> GetResourceById(int id)
            {
                return await _resourceRepository.GetResourceById(id);
            }
            public async Task<IEnumerable<Resource>> GetResourcesBySanctuaryId(int sanctuaryId)
            {
                return await _resourceRepository.GetResourcesBySanctuaryId(sanctuaryId);
            }

            public async Task AddResource(Resource resource)
            {
                await _resourceRepository.AddResource(resource);
            }

            public async Task UpdateResource(Resource resource)
            {
                await _resourceRepository.UpdateResource(resource);
            }

            public async Task DeleteResource(int id)
            {
                await _resourceRepository.DeleteResource(id);
            }

        }
    }
