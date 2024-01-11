using Lesson1API.Data;
using Lesson1API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lesson1API.Repositories
{
    public class SQLWalkRepository : IWalkResponsitory
    {
        private readonly WalksDbContext dbContext;
        public SQLWalkRepository(WalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> createWalkAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> deleteWalkAsync(Guid id)
        {
             var exsting = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (exsting == null)
            {
                return null;
            }
            this.dbContext.Walks.Remove(exsting);
            await dbContext.SaveChangesAsync();
            return exsting;
        }



        public async Task<List<Walk>> getAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAccending = true
            , int pageNumber = 1, int pageSize = 10000)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            //Filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
            }    

            

            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAccending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }    
                else if(sortBy.Equals("Lenghth", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAccending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }    
            }

            // Pagination
            var skipResult = (pageNumber - 1) * pageSize;
            return await walks.Skip(skipResult).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> getByIdAync(Guid id)
        {
            return await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> updateWalkAsync(Guid id, Walk walk)
        {
            var exsting = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (exsting == null)
            {
                return null;
            }
            exsting.Name = walk.Name;
            exsting.Description = walk.Description;
            exsting.LengthInKm = walk.LengthInKm;
            exsting.WalkImageUrl = walk.WalkImageUrl;
            exsting.DifficultyId = walk.DifficultyId;
            exsting.RegionId = walk.RegionId;

            await this.dbContext.SaveChangesAsync();
            return exsting;
        }
    }
}
