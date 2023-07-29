using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeManagment.DTOs;
using OfficeManagment.Model;

namespace OfficeManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OMController : ControllerBase
    {
        private readonly DataContext _context;
        public OMController(DataContext context)
        {
            _context = context;
        }

        //Method to add another User to the project 
        //Method created to be used for Authorization
        [HttpPost("AddUserToProject")]
        public async Task<ActionResult> AddUserToProject(UserProjectsDTO userProjectDto)
        {
            var positions = await _context.Positions.Where(p => userProjectDto.PositionIds.Contains(p.Id)).ToListAsync();

            if (positions.Count != userProjectDto.PositionIds.Count)
            {
                return BadRequest("GUID dosent match with Positions ID");
            }

            var userProject = new UserProjects
            {
                ProjectId = userProjectDto.ProjectId,
                UserId = userProjectDto.UserId,
                PositionIds = userProjectDto.PositionIds,
                WorkingHours = userProjectDto.WorkingHours,
                ProgrammingLanguage = userProjectDto.ProgrammingLanguage,

            };

            _context.UserProjects.Add(userProject);
            await _context.SaveChangesAsync();

            return Ok(userProject);

        }

        //Add another position to a UserProject.
        [HttpPut("AddAnotherPositionToUserProject/{id}")]
        public async Task<IActionResult> AddPositionToUserProject(Guid id, Guid positionId)
        {
            var userProject = await _context.UserProjects.FindAsync(id);

            if (userProject == null)
            {
                return NotFound();
            }

            var positionExists = await _context.Positions.AnyAsync(x => x.Id == positionId);

            if (!positionExists)
            {
                return BadRequest("Position does not exist");
            }

            var positionIds = userProject.PositionIds;

            if (positionIds.Contains(positionId))
            {
                return BadRequest("Position already exists for this UserProject");
            }

            positionIds.Add(positionId);

            userProject.PositionIds = positionIds;

            await _context.SaveChangesAsync();

            return NoContent();
        }




    }
}
