using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public interface IResourceRepository
    {
        Task<IEnumerable<Resource>> GetAllResourcesById(int sanctuaryId);
        Task<Resource> GetResourceById(int id);
        Task<IEnumerable<Resource>> GetResourcesBySanctuaryId(int sanctuaryId);
        Task AddResource(Resource resource);
        Task UpdateResource(Resource resource);
        Task DeleteResource(int id);
    }
}
