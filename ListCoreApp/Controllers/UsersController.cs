using ListCoreApp.Data;
using ListCoreApp.Helpers;
using ListCoreApp.Models;
using ListCoreApp.Requests.StarredList;
using ListCoreApp.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
            if (user.Password == null) return BadRequest(user);
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
            var token = TokenGenerator.GetToken(_config, user.Email);
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
                var token = TokenGenerator.GetToken(_config, user.Email);
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

        [HttpGet("starred")]
        public async Task<ActionResult> GetStarred()
        {
            try
            {
                /*var tokenBearerEmail = TokenBearerValueGetter.getValue(Request, "email");
                var userId = await _context.Users.Where(u => u.Email == tokenBearerEmail)
                                                 .Select(u => u.Id)
                                                 .FirstAsync();*/
                var userId = UserIdGetter.getIdFromToken(Request, _context);
                var starredListsIds = await _context.UserLists.Where(ul => ul.UserId == userId)
                                                              .Select(ul => ul.ListId)
                                                              .ToListAsync();
                var starredLists = await _context.ItemLists.Where(il => starredListsIds.Contains(il.Id))
                                                            .Select(il => new { il.Name, il.AccessCode })
                                                            .ToListAsync();
                return Ok(starredLists);
            }
            catch {
                return BadRequest("Nothing found");
            }
        }

        [HttpPost("starred")]
        public async Task<ActionResult> PostStarred(StarListRequest request)
        {
            var list = await _context.ItemLists.Where(il => il.AccessCode == request.AccessCode).FirstAsync();
            var listAccessCode = list.AccessCode;

            if (listAccessCode != request.AccessCode) return BadRequest("Invalid request: pass");
            try
            {
                var tokenBearerEmail = TokenBearerValueGetter.getValue(Request, "email");
                var userId = await _context.Users.Where(u => u.Email == tokenBearerEmail)
                                                 .Select(u => u.Id)
                                                 .FirstAsync();

                var userList = new UserList()
                {
                    UserId = userId,
                    ListId = list.Id
                };

                _context.UserLists.Add(userList);
                await _context.SaveChangesAsync();

                return Ok("Starred a list");
            }
            catch {
                return BadRequest("Invalid request");
            }
        }

        [HttpDelete("starred")]
        public async Task<ActionResult> DeleteStarred(StarListRequest request) {
            var list = await _context.ItemLists.Where(il => il.AccessCode == request.AccessCode).FirstAsync();
            var listAccessCode = list.AccessCode;
            if (listAccessCode != request.AccessCode) return BadRequest("Invalid request: pass");
            try
            {
                var userId = UserIdGetter.getIdFromToken(Request, _context);
                var userListToDelete = await _context.UserLists.Where(ul => ul.ListId == list.Id && ul.UserId == userId).FirstAsync();
                _context.UserLists.Remove(userListToDelete);
                await _context.SaveChangesAsync();
                return Ok("Unstarred a list");
            }
            catch {
                return BadRequest("Not logged in");
            }
        }
    }
}
