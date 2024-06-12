using LinkPoint.Business.DTOs.AccountDTOs;
using LinkPoint.Business.Services.Implementations;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.ConfirmedExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotFoundException;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LinkPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminAuthService _adminAuthService;
        private readonly IAdminUserService _adminUserService;

        public AdminController(IAdminAuthService adminAuthService,IAdminUserService adminUserService)
        {
            _adminAuthService = adminAuthService;
            _adminUserService = adminUserService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                return Ok(await _adminAuthService.LoginAsync(loginDto));
            }
            catch (InvalidCredentialException ex)
            {
                return BadRequest(new ErrorDto { message = ex.Message });
            }
            catch (EmailConfirmedException ex)
            {
                return BadRequest(new ErrorDto { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDto { message = ex.Message });
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginRefreshToken(string refreshToken)
        {
            try
            {
                return Ok(await _adminAuthService.RefreshTokenLoginAsync(refreshToken));
            }
            catch (RefreshTokenNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> LogOut()
        {
            await _adminAuthService.LogOutAsync();
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            try
            {
                await _adminAuthService.ForgotPasswordAsync(forgotPasswordDto);
                return Ok();
            }
            catch (EmailNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                await _adminAuthService.ResetPasswordAsync(resetPasswordDto);
                return Ok();
            }
            catch (EmailNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]/{query}")]
        public async Task<IActionResult> GetAllUsersForAdmin(string query)
        {
            try
            {
                return Ok(await _adminUserService.GetAllUsersAsync(query));
            }
            catch (ProfileImageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAllUsersWithPages(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                return Ok(await _adminUserService.GetAllUsersWithPagesAsync(pageNumber,pageSize));
            }
            catch (ProfileImageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> GetUserById(string UserId)
        {
            try
            {
                return Ok(await _adminUserService.GetUserByIdAsync(UserId));
            }
            catch (UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (ProfileImageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
