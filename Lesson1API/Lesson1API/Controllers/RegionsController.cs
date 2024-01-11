using AutoMapper;
using Lesson1API.CustomActionFilters;
using Lesson1API.Data;
using Lesson1API.Models.Domain;
using Lesson1API.Models.DTOs;
using Lesson1API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Lesson1API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly WalksDbContext dbContext;
        private readonly IRegionResponsitory regionResponsitory;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(WalksDbContext dbContext, IRegionResponsitory regionResponsitory, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionResponsitory = regionResponsitory;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
      //  [Authorize(Roles = "Reader")]
       // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> getAll()
        {
            try
            {
                //throw new Exception("This is a custom exception");
                var regionsDomain = await regionResponsitory.GetAllAsync();
                // Map domain models to DTOs
                // var regionsDto = new List<RegionDto>();
                /* foreach (var regionDomain in regionsDomain)
                 {
                     regionsDto.Add(
                         new RegionDto()
                         {
                             Id = regionDomain.Id,
                             Code = regionDomain.Code,
                             Name = regionDomain.Name,
                             RegionImageUrl = regionDomain.RegionImageUrl,
                         });
                 }*/
                // Return DTOs
                logger.LogInformation($"Finshied GetAllRegions request with data: {JsonSerializer.Serialize(regionsDomain)}");
                return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
                
            }
            /*            var regions = new list<region>
                        {
                            new region
                            {
                                id = guid.newguid(),
                                name = "việt nam",
                                code = "vn",
                                regionimageurl = "https://th.bing.com/th/id/r.c094cb462553765280f4040559e6af5d?rik=5sohtgtokdzkxq&riu=http%3a%2f%2f4.bp.blogspot.com%2f-gg6phtutcpo%2fuxubqhyn8wi%2faaaaaaaabca%2fsx11ukgecjy%2fs1600%2fhinh-anh-la-co-viet-nam-dep%2b%2525285%252529.jpg&ehk=%2b1mat8sdqf3azqtivg%2fn2uo6sb%2bcuif063kjnzhlgr4%3d&risl=&pid=imgraw&r=0"
                            },

                            new region
                            {
                                id= guid.newguid(),
                                name = "thái lan",
                                code = "tla",
                                regionimageurl = "https://th.bing.com/th/id/r.20ccf8979f0990c6ad0d559a795c6d9b?rik=k6lskgzcuinz2w&riu=http%3a%2f%2fshopcosao.com%2fwp-content%2fuploads%2f2017%2f11%2f900px-flag_of_thailand.svg_.png&ehk=xkfndtnx0ss9rh3dkglfaochguz3h%2fonegoojd%2bng6c%3d&risl=&pid=imgraw&r=0"
                            }
                        };*/
       


            // get data from database - Domain models
            // var regionsDomain = await dbContext.Regions.ToListAsync();
          
        }

        // Get Single Region (Get Region By Id)
        [HttpGet]
        [Route("{id:Guid}")]
        [ValidateModel]
       // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> getById([FromRoute] Guid id)
        {
            //var  region = dbContext.Regions.Find(id);
            // Get region domain model from database
            var regionDomain = await regionResponsitory.GetByIdAsync(id);
            if(regionDomain == null)
            {
                return NotFound();
            }
            // Map/ Convert Region Domain Model to Region DTO
          /*  var regionsDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };*/
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        // POST to Create New Region
        // POST: https://localhost:portnumber/api/regions
        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
                // Map or Convert DTO to Domain Model
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
                // Use Domain Model to create Region
                /*dbContext.Regions.Add(regionDomainModel);
                dbContext.SaveChanges();*/

                regionDomainModel = await regionResponsitory.CreateAsync(regionDomainModel);
                // Map Domain Model back to DTO
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return CreatedAtAction(nameof(getById), new { id = regionDomainModel.Id }, regionDto);
          
           
        }

        // Update Region
        // PUT: https//localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
       // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
           
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
                regionDomainModel = await regionResponsitory.UpdateAsync(id, regionDomainModel);
                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                // Convert Domain Modek to DTO
                return Ok(mapper.Map<RegionDto>(regionDomainModel));
          

        }


        // Delete Region
        // DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
       // [Authorize(Roles = "Writer, Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var  regionDomainModel = await regionResponsitory.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            // Delete region
           /* dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();*/

            // Return deleted Region back
            // Map domain Model to DTO
           /* var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };*/
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }

    }
}


