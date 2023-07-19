﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeManagment.DTOs;
using OfficeManagment.Model;

namespace OfficeManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly DataContext _context;
        public UserRoleController(DataContext context)
        {
            _context = context;
        }

        //Get All User Roles
        [HttpGet]
        public async Task<ActionResult<List<UserRole>>> Get()
        {
            var userRoles = await _context.UserRoles
                .Include(x => x.Roles)
                .Include(x => x.User)
                .ToListAsync();

            var userRolesData = userRoles.Select(userRoles => new
            {
                userRoleId = userRoles.Id,
                UserId = userRoles.UserId,
                UserName = userRoles.User.Name,
                RoleId = userRoles.RoleId,
                RoleName = userRoles.Roles.FullName
            }).ToList();

            return Ok(userRolesData);
        }

        //Get a single User Role 
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRole>> Get(Guid id)
        {
            var userRole = await _context.UserRoles
                .Include(x => x.Roles)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (userRole == null)
            {
                return NotFound();
            }

            var userRoleData = new
            {
                UserId = userRole.UserId,
                UserName = userRole.User.Name,
                RoleId = userRole.RoleId,
                RoleName = userRole.Roles.FullName
            };

            return Ok(userRoleData);
        }

        //Add a new UserRole 
        [HttpPost("AddUserRole")]
        public async Task<ActionResult> Add(UserRoleDTO request)
        {
            var newUserRole = new UserRole();
            newUserRole.UserId = request.UserId;
            newUserRole.RoleId = request.RoleId;

            _context.UserRoles.Add(newUserRole);
            await _context.SaveChangesAsync();
            
            return Ok("UserRole Added");
        }

        //Edit a UserRole
        [HttpPut("EditUserRole/{id}")]
        public async Task<ActionResult> Update(Guid id,UserRoleDTO request)
        {
            var userRole = await _context.UserRoles.FindAsync(id);
            if (userRole == null)
            {
                return NotFound();
            }

            userRole.UserId = request.UserId;
            userRole.RoleId = request.RoleId;

            await _context.SaveChangesAsync();

            return Ok("UserRole Updated!");
        }



    }
}
