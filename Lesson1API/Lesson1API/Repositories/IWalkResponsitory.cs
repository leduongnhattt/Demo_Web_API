using Lesson1API.Models.Domain;

namespace Lesson1API.Repositories
{
    public interface IWalkResponsitory
    {
        Task<List<Walk>> getAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAccennding = true,
            int pageNumber = 1, int pageSize = 10000);
        Task<Walk> createWalkAsync(Walk walk);
        Task<Walk?> getByIdAync(Guid id);
        Task<Walk?> updateWalkAsync(Guid id, Walk walk);
        Task<Walk?> deleteWalkAsync(Guid id);
    }
}
