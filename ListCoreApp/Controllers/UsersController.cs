using ListCoreApp.Data;
using ListCoreApp.Helpers;
using ListCoreApp.Models;
using ListCoreApp.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _config;


        public UsersController(DatabaseContext context, IConfiguration config) 
        {
            _context = context;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User user) {
            if (_context.Users.Where(u => u.Email == user.Email).Any()) return BadRequest("already exists");
            if (user.Password == null) return BadRequest(user); //return BadRequest("password cannot be null");
            var (hashedPassword, salt) = PasswordHasher.Hash(user.Password);
            user.Password = hashedPassword;
            user.SecurityHash = salt;
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
            var token = TokenGenerator.GetToken(_config);
            return Ok(new SuccessfulRegisterResponse
            {
                Id = user.Id,
                Token = token
            }); ;
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(User user)
        {
            try
            {
                var foundUser = await _context.Users.Where(u => u.Email == user.Email).FirstAsync();
                if (!PasswordValidator.Validate(user.Password, foundUser.SecurityHash, foundUser.Password))
                    return BadRequest("Wrong Password");
                var token = TokenGenerator.GetToken(_config);
                return Ok(new SuccesfulLoginResponse
                {
                    Token = token,
                    Id = foundUser.Id
                });
            }
            catch
            {
                return BadRequest("such account does not exist");
            }
        }
    }
}
