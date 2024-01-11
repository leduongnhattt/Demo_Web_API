using Lesson1API.Models.Domain;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Lesson1API.Repositories
{
    public interface IRegionResponsitory
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid id);
        Task<Region> CreateAsync(Region region);
        Task<Region?> UpdateAsync(Guid id, Region update);
        Task<Region?> DeleteAsync(Guid id);
    }
}
