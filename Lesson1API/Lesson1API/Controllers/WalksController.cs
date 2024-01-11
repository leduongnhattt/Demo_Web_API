using AutoMapper;
using Lesson1API.Models.Domain;
using Lesson1API.Models.DTOs;
using Lesson1API.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace Lesson1API.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class WalksController : Controller
    {
        private readonly IMapper mapper;
        private readonly IWalkResponsitory walkResponsitory;
        public WalksController(IMapper mapper, IWalkResponsitory walkResponsitory)
        {
            this.mapper = mapper;
            this.walkResponsitory = walkResponsitory;
        }

        //Create Walk ~ POST
        [HttpPost]
        public async Task<IActionResult> createData([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //Mappe DTO to Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
            
            walkDomainModel = await walkResponsitory.createWalkAsync(walkDomainModel);
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }    

        // Get All ~ GET
        [HttpGet]
        public async Task<IActionResult> getAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy,[FromQuery] bool? isAccending
            , [FromQuery] int pageNuber = 1, [FromQuery] int pageSize = 10000)
        {
            var walkDomainModel = await walkResponsitory.getAllAsync(filterOn, filterQuery, sortBy, isAccending ?? true, pageNuber, pageSize);

            // Create an Exception
            throw new Exception("This is a new Exception");

            return Ok(mapper.Map<List<WalkDTO>>(walkDomainModel));
        }

        // Get By Id ~ GET
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> getById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkResponsitory.getByIdAync(id);
            if(walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }

        // Update ~ PUT
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,  UpdateWalkRequestDto updateWalkRequestDto)
        {

            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            walkDomainModel = await walkResponsitory.updateWalkAsync(id, walkDomainModel);
            if(walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }
        
        // Delete ~ DELETE
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomainModel = await walkResponsitory.deleteWalkAsync(id);
            if(walkDomainModel == null)
            {
                return NotFound();
            }    
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }
        
    }
}
