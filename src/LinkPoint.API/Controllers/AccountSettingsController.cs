using LinkPoint.Business.DTOs.AccountSettingsDTOs.BackgroundImageDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.PasswordDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.ProfileImageDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserAboutDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserEducationDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserInterestDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserWorkDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.CommonExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotValidExceptions;
using Microsoft.AspNetCore.Mvc;

namespace LinkPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountSettingsController : ControllerBase
    {
        private readonly IAccountSettingsService _accountSettingsService;

        public AccountSettingsController(IAccountSettingsService accountSettingsService)
        {
            _accountSettingsService = accountSettingsService;
        }
        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> GetAuthUserInfo(string UserId)
        {
            try
            {
                return Ok(await _accountSettingsService.GetAuthUserInfoAsync(UserId));
            }
            catch (ProfileImageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
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
        public async Task<IActionResult> GetUserAbout(string UserId)
        {
            try
            {
                return Ok(await _accountSettingsService.GetUserAboutAsync(UserId));
            }
            catch (UserAboutNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
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
        [HttpPut("[action]/{Id}")]
        public async Task<IActionResult> UpdateUserAbout(int Id,UserAboutPutDto userAboutPutDto)
        {
            try
            {
                await _accountSettingsService.UpdateUserAboutAsync(Id, userAboutPutDto);
                return Ok();
            }
            catch(IdNotValidException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);   
            } 
            catch(UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);   
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]/{UserId}")]
        public async Task <IActionResult> GetUserEducation(string UserId)
        {
            try
            {
                return Ok(await _accountSettingsService.GetUserEducationAsync(UserId));
            }
            catch(UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }
            catch(UserEducationNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUserEducation(UserEducationPostDto userEducationPostDto)
        {
            try
            {
                await _accountSettingsService.CreateUserEducationAsync(userEducationPostDto);
                return Ok();
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
        [HttpPut("[action]/{Id}")]
        public async Task<IActionResult> UpdateUserEducation(int Id, UserEducationPutDto userEducationPutDto)
        {
            try
            {
                await _accountSettingsService.UpdateUserEducationAsync(Id, userEducationPutDto);
                return Ok();
            }
            catch (IdNotValidException ex)
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
                return Ok(await _accountSettingsService.GetUserWorkAsync(UserId));
            }
            catch (UserWorkNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
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
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUserWork(UserWorkPostDto userWorkPostDto)
        {
            try
            {
                await _accountSettingsService.CreateUserWorkAsync(userWorkPostDto);
                return Ok();
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
        [HttpPut("[action]/{Id}")]
        public async Task<IActionResult> UpdateUserWork(int Id, UserWorkPutDto userWorkPutDto)
        {
            try
            {
                await _accountSettingsService.UpdateUserWorkAsync(Id, userWorkPutDto);
                return Ok();
            }
            catch (IdNotValidException ex)
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
                return Ok(await _accountSettingsService.GetAllUserInterestsAsync(UserId));
            }
            catch (UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (UserInterestNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUserInterest(UserInterestPostDto userInterestPostDto)
        {
            try
            {
                await _accountSettingsService.CreateUserInterestAsync(userInterestPostDto);
                return Ok();
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
        [HttpDelete("[action]/{Id}")]
        public async Task <IActionResult> DeleteUserInterest(int Id, UserInterestDeleteDto userInterestDeleteDto)
        {
            try
            {
                await _accountSettingsService.DeleteUserInterestAsync(Id, userInterestDeleteDto);
                return Ok();
            }
            catch (IdNotValidException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("[action]/{UserId}")]
        public async Task<IActionResult> ChangePassword(string UserId, ChangePasswordDto changePasswordDto)
        {
            try
            {
                await _accountSettingsService.ChangePasswordAsync(UserId, changePasswordDto);
                return Ok();
            }
            catch (IdNotValidException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            } 
            catch (UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            } 
            catch (InvalidCredentialsException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("[action]/{ImageId}")]
        public async Task<IActionResult> UpdateUserProfileImage(int ImageId, ProfileImagePutDto profileImagePostDto)
        {
            try
            {
                await _accountSettingsService.UpdateUserProfileImageAsync(ImageId, profileImagePostDto);
                return Ok();
            }
            catch (IdNotValidException ex)
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
        [HttpDelete("[action]/{ImageId}")]
        public async Task<IActionResult> DeleteUserProfileImage(int ImageId, ProfileImageDeleteDto profileImageDeleteDto)
        {
            try
            {
                await _accountSettingsService.DeleteUserProfileImageAsync(ImageId, profileImageDeleteDto);
                return Ok();
            }
            catch (IdNotValidException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (ImageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
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
        [HttpPut("[action]/{ImageId}")]
        public async Task<IActionResult> UpdateUserBacgroundImage(int ImageId, BackgroundImagePutDto backgroundImagePutDto)
        {
            try
            {
                await _accountSettingsService.UpdateUserBacgroundImageAsync(ImageId, backgroundImagePutDto);
                return Ok();
            }
            catch (IdNotValidException ex)
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
        [HttpDelete("[action]/{ImageId}")]
        public async Task<IActionResult> DeleteUserBackgroundImage(int ImageId, BackgroundImageDeleteDto backgroundImageDeleteDto)
        {
            try
            {
                await _accountSettingsService.DeleteUserBackgroundImageAsync(ImageId, backgroundImageDeleteDto);
                return Ok();
            }
            catch (IdNotValidException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (ImageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
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
    }
}
