using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public interface IResourceService
    {
        Task<IEnumerable<Resource>> GetAllResourcesById(int sanctuaryId);
        Task<Resource> GetResourceById(int id);
        Task<IEnumerable<Resource>> GetResourcesBySanctuaryId(int sanctuaryId);
        Task AddResource(Resource resource);
        Task UpdateResource(Resource resource);
        Task DeleteResource(int id);
    }
}
