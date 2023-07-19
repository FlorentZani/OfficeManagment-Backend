using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeManagment.DTOs;
using OfficeManagment.Model;

namespace OfficeManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProjectsController : ControllerBase
    {
        private readonly DataContext _context;
        public UserProjectsController(DataContext context) 
        {
            _context = context;
        }
        //GetAllUserProjects
        [HttpGet]
        public async Task<ActionResult<List<object>>> Get()
        {
            var userProjects = await _context.UserProjects
                .Include(x => x.Projects)
                .Include(x => x.User)
                .ToListAsync();

            var userProjectsData = userProjects.Select(userProjects => new
            {
                UserProjectId = userProjects.Id,
                ProjectName = userProjects.Projects.Name,
                ProjectId = userProjects.ProjectId,
                UserId = userProjects.UserId,
                UserName = userProjects.User.Name,
                ProgrammingLanguage = userProjects.ProgrammingLanguage,
                WorkingHours = userProjects.WorkingHours

            }).ToList();

            return Ok(userProjectsData);
        }

        //GetUserProjectById
        [HttpGet("GetSingleUserProject/{id}")]
        public async Task<ActionResult<object>> Get(Guid id)
        {
            var userProject = await _context.UserProjects
                .Include(x => x.Projects)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            var userProjectData =  new
            {
                ProjectName = userProject.Projects.Name,
                ProjectId = userProject.ProjectId,
                UserId = userProject.UserId,
                UserName = userProject.User.Name,
                ProgrammingLanguage = userProject.ProgrammingLanguage,
                WorkingHours = userProject.WorkingHours

            };
            return Ok(userProjectData);
        }
        //Add a new UserProject
        [HttpPost("AddUserProject")]
        public async Task<ActionResult> Add(UserProjectsDTO request)
        {
            
            var newUserProject = new UserProjects();
            newUserProject.ProjectId = request.ProjectId;
            newUserProject.UserId = request.UserId;
            newUserProject.WorkingHours = request.WorkingHours;
            newUserProject.ProgrammingLanguage = request.ProgrammingLanguage;


            _context.UserProjects.Add(newUserProject);
            await _context.SaveChangesAsync(); 

            return Ok("UserProject Added");
            
        }

        //Update UserProjects
        [HttpPut("UpdateUserProject/{id}")]
        public async Task<ActionResult> Update(Guid id ,UserProjectsDTO request)
        {
            var UpdatedUserProject = await _context.UserProjects.FindAsync(id);

            if (UpdatedUserProject == null)
            {
                return BadRequest("UserProjects Not Found!");
            }

            UpdatedUserProject.ProjectId = request.ProjectId;
            UpdatedUserProject.UserId= request.UserId;
            UpdatedUserProject.WorkingHours= request.WorkingHours;
            UpdatedUserProject.ProgrammingLanguage= request.ProgrammingLanguage;
            

            await _context.SaveChangesAsync();

            return Ok(UpdatedUserProject);
        }

        //Delete UserProjects
        [HttpDelete("DeleteUserProject/{id}")]
        public async Task<ActionResult> Delete (Guid id)
        {
            var UserProject = await _context.UserProjects.FindAsync(id);
            if (UserProject == null)
            {
                return BadRequest("UserProjects Not Found!");
            }

            _context.UserProjects.Remove(UserProject);
            await _context.SaveChangesAsync();

            return Ok("UserProject Deleted");
        }

    }
}
