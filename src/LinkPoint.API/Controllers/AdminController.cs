using LinkPoint.Business.DTOs.AccountDTOs;
using LinkPoint.Business.Services.Implementations;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.ConfirmedExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotFoundException;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace LinkPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminAuthService _adminAuthService;

        public AdminController(IAdminAuthService adminAuthService)
        {
            _adminAuthService = adminAuthService;
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
    }
}
