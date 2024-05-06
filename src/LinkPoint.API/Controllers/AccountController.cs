using LinkPoint.Business.DTOs.AccountDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.CommonExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotFoundException;
using LinkPoint.Core.Entities;
using LinkPoint.Data.Contexts;
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
        //[HttpGet("[action]")]
        //public async Task<IActionResult> CreateUser()
        //{
        //    AppUser user = new AppUser()
        //    {
        //        FirstName = "Ziya",
        //        LastName ="Memmedli",
        //        UserName = "ziya_memmedli",
        //        Email = "ziyarma@code.edu.az",
        //        RefreshToken ="token",
        //        CreatedDate= DateTime.UtcNow,
        //    };
        //    string password = "Salam123@";
        //    await _userManager.CreateAsync(user, password);
        //    await _userManager.AddToRoleAsync(user, "SuperAdmin");
        //    UserAbout userAbout = new UserAbout()
        //    {
        //        UserId = user.Id,
        //        Male=true,
        //        Female=false,
        //        CreatedDate = DateTime.UtcNow,
        //        UpdatedDate = DateTime.UtcNow,
        //        User =user,
        //    };
        //    await _context.AddAsync(userAbout);
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}
        //[HttpGet("[action]")]
        //public async Task<IActionResult> CreateRole()
        //{

        //    IdentityRole role = new IdentityRole("Admin");
        //    IdentityRole role2 = new IdentityRole("SuperAdmin");
        //    IdentityRole role3 = new IdentityRole("Member");


        //    await _roleManager.CreateAsync(role);
        //    await _roleManager.CreateAsync(role2);
        //    await _roleManager.CreateAsync(role3);
        //    return Ok();
        //}
    }
}
