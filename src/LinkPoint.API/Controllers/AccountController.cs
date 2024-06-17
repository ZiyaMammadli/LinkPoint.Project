using LinkPoint.Business.DTOs.AccountDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.CommonExceptions;
using LinkPoint.Business.Utilities.Exceptions.ConfirmedExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotFoundException;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace LinkPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
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
                return BadRequest(new ErrorDto { message=ex.Message});
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDto { message = ex.Message });
            }
        }
        [HttpGet("[action]/{UserId}/{code}")]
        public async Task<IActionResult> Emailconfirm(string UserId,string code)
        {
            try
            {
                await _accountService.EmailConfirmAsync(UserId, code);
                return Ok();
            }
            catch(ValueNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> LogOut()
        {
            await _accountService.LogOutAsync();
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task <IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            try
            {
                await _accountService.ForgotPasswordAsync(forgotPasswordDto);
                return Ok();
            }
            catch(EmailNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
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
                await _accountService.ResetPasswordAsync(resetPasswordDto);
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
