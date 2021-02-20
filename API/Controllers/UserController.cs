using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Interfaces;
using API.models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo repo, IMapper mapper)
        {
            _mapper = mapper;
            _userRepo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers() 
            => Ok(await _userRepo.GetMembersAsync());     
        
        
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUserByIdAsync(string username)
            => await _userRepo.GetMemberAsync(username);

    }
}