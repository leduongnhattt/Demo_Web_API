using Lesson1API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lesson1API.Data
{
    public class WalksDbContext : DbContext
    {
        public WalksDbContext(DbContextOptions<WalksDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Send data for difficulties
            var difficulties = new List<Difficulty>()
            {
                new Difficulty
                {
                    Id = Guid.Parse("ca76a67c-58b1-406d-b2a0-ee0db42bac77"),
                    Name = "Khó 1"

                },
                    new Difficulty
                {
                    Id = Guid.Parse("b0f4bf58-a690-4fc7-b6b9-2bc1be378752"),
                    Name = "Khó 2"

                },
                        new Difficulty
                {
                    Id = Guid.Parse("811ee708-fc79-401a-9d90-8ce97da3a807"),
                    Name = "Khó 3"

                }

            };
           modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //Send data to Regions
            var regions = new List<Region>()
            {
                new Region
                {
                    Id = Guid.Parse("0758eb25-1c29-47f9-8219-62b9de165eb6"),
                    Code = "TL",
                    Name = "Thái Lan",
                    RegionImageUrl = "some2_image-url.jpg"
                },
                   new Region
                {
                    Id = Guid.Parse("1554d0e1-aa56-4373-859f-e06d7970e6be"),
                    Code = "TQ",
                    Name = "Trung Quốc",
                    RegionImageUrl = "some3_image-url.jpg"
                },
                      new Region
                {
                    Id = Guid.Parse("c34de05e-a1e5-43e8-8664-96ffafb527dc"),
                    Code = "VN",
                    Name = "Việt Nam",
                    RegionImageUrl = "some1_image-url.jpg"
                },
            };
            modelBuilder.Entity<Region>().HasData(regions);

        }
    }
}
