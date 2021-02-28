using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
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
            // => Ok(await _userRepo.GetMembersAsync());     
        {
            var x = await _userRepo.GetUsersAsync();
                       
            return Ok(_mapper.Map<IEnumerable<MemberDto>>(x));
        }
        
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUserByIdAsync(string username)
            => await _userRepo.GetMemberAsync(username);

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepo.GetUserByUsernameAsync(username);

            _mapper.Map(memberUpdateDto, user);

            _userRepo.Update(user);

            if (await _userRepo.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }

    }
}