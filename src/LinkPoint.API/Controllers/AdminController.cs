using LinkPoint.Business.DTOs.AccountDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.ConfirmedExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotFoundException;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace LinkPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminAuthService _adminAuthService;
        private readonly IAdminUserService _adminUserService;
        private readonly IAdminPostService _adminPostService;

        public AdminController(IAdminAuthService adminAuthService,IAdminUserService adminUserService,IAdminPostService adminPostService)
        {
            _adminAuthService = adminAuthService;
            _adminUserService = adminUserService;
            _adminPostService = adminPostService;
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
        [Authorize(Roles = "Admin,SuperAdmin")]
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
        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> GetUserAbout(string UserId)
        {
            try
            {
                return Ok(await _adminUserService.GetUserAboutAsync(UserId));
            }
            catch (UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (UserAboutNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> GetUserWork(string UserId)
        {
            try
            {
                return Ok(await _adminUserService.GetUserWorkAsync(UserId));
            }
            catch (UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> GetUserEducation(string UserId)
        {
            try
            {
                return Ok(await _adminUserService.GetUserEducationAsync(UserId));
            }
            catch (UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> GetAllUserInterests(string UserId)
        {
            try
            {
                return Ok(await _adminUserService.GetAllUserInterestsAsync(UserId));
            }
            catch (UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]/{UserId}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> UserSoftDelete(string UserId)
        {
            try
            {
                await _adminUserService.UserSoftDeleteAsync(UserId);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (UserAboutNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (ImageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]/{UserId}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> UserActivate(string UserId)
        {
            try
            {
                await _adminUserService.UserActivateAsync(UserId);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (UserAboutNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (ImageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> GetAllPosts()
        {
            try
            {
                return Ok(await _adminPostService.GetAllPostsAsync());
            }
            catch (PostNotFoundException ex)
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
        [HttpGet("[action]/{PostId}")]
        public async Task<IActionResult> GetByIdPost(int PostId)
        {
            try
            {              
                return Ok(await _adminPostService.GetByIdPostAsync(PostId));
            }
            catch (PostNotFoundException ex)
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
        [HttpGet("[action]/{PostId}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> SoftDeletePost(int PostId)
        {
            try
            {
                await _adminPostService.SoftDeletePostAsync(PostId);
                return Ok();
            }
            catch (PostNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]/{PostId}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> ActivatePost(int PostId)
        {
            try
            {
                await _adminPostService.ActivatePostAsync(PostId);
                return Ok();
            }
            catch (PostNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]/{PostId}")]
        public async Task<IActionResult> Delete(int PostId)
        {
            try
            {
                await _adminPostService.DeleteAsync(PostId);
                return Ok();
            }
            catch (PostNotFoundException ex)
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
