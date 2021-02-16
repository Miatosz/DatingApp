using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() 
            => await _context.Users.ToListAsync();

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AppUser>> GetUserByIdAsync(int id)
            => await _context.Users.FindAsync(id);

    }
}