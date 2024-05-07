using LinkPoint.Business.DTOs.AccountDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.CommonExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotFoundException;
using LinkPoint.Core.Entities;
using LinkPoint.Data.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace LinkPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAccountService _accountService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly LinkPointDbContext _context;

        public AccountController(UserManager<AppUser> userManager,IAccountService accountService,RoleManager<IdentityRole> roleManager,LinkPointDbContext context)
        {
            _userManager = userManager;
            _accountService = accountService;
            _roleManager = roleManager;
            _context = context;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                return Ok(await _accountService.LoginAsync(loginDto));                
            }
            catch(InvalidCredentialException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginRefreshToken(string refreshToken)
        {
            try
            {
                return Ok(await _accountService.RefreshTokenLoginAsync(refreshToken));
            }
            catch(RefreshTokenNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }       
        [HttpPost("[action]")]       
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
               await _accountService.RegisterAsync(registerDto);
               return Ok();
            }
            catch(AlreadyExistException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]")]
        [Authorize(Roles = "Member")]
        public IActionResult Getgetirmene()
        {
            return Ok("salam bro");
        }

    }
}
