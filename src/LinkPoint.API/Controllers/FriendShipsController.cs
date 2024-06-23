using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace LinkPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendShipsController : ControllerBase
    {
        private readonly IFriendShipService _friendShipService;

        public FriendShipsController(IFriendShipService friendShipService)
        {
            _friendShipService = friendShipService;
        }
        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> GetAllAcceptedFollowingUsers(string UserId)
        {
            try
            {
                return Ok(await _friendShipService.GetAllAcceptedFollowingUsersAsync(UserId));
            }
            catch(UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }    
            catch(ProfileImageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }    
            catch(FriendShipNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> GetAllAcceptedFollowerUsers(string UserId)
        {
            try
            {
                return Ok(await _friendShipService.GetAllAcceptedFollowerUsersAsync(UserId));
            }
            catch (UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (ProfileImageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (FriendShipNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> GetAllMyFriends(string UserId)
        {
            try
            {
                return Ok(await _friendShipService.GetAllMyFriendsAsync(UserId));
            }
            catch (UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (ProfileImageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (FriendShipNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> GetAllPendingFollowerUsers(string UserId)
        {
            try
            {
                return Ok(await _friendShipService.GetAllPendingFollowerUsersAsync(UserId));
            }
            catch (UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (ProfileImageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (FriendShipNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("[action]/{UserId}/{followingUserId}")]
        public async Task<IActionResult> AddToFriendShip(string UserId, string followingUserId)
        {
            try
            {
                await _friendShipService.AddToFriendShipAsync(UserId, followingUserId);
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

        [HttpGet("[action]/{UserId}/{followingUserId}")]
        public async Task<IActionResult> CheckFriendShipStatus(string UserId, string followingUserId)
        {
            try
            {              
                return Ok(await _friendShipService.CheckFriendShipStatusAsync(UserId, followingUserId));
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

        [HttpDelete("[action]/{UserId}/{followingUserId}")]
        public async Task<IActionResult> CancelFriendShip(string UserId, string followingUserId)
        {
            try
            {
                await _friendShipService.CancelFriendShipAsync(UserId, followingUserId);
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

        [HttpPut("[action]/{friendShipId}")]
        public async Task<IActionResult> AcceptFriendShipRequest(int friendShipId)
        {
            try
            {
                await _friendShipService.AcceptFriendShipRequestAsync(friendShipId);
                return Ok();
            }
            catch (FriendShipNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("[action]/{friendShipId}")]
        public async Task<IActionResult> RejectFriendShipRequest(int friendShipId)
        {
            try
            {
                await _friendShipService.RejectFriendShipRequestAsync(friendShipId);
                return Ok();
            }
            catch (FriendShipNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("[action]/{UserId}/{followingUserId}")]
        public async Task<IActionResult> Unfollow(string UserId, string followingUserId)
        {
            try
            {
                await _friendShipService.UnfollowAsync(UserId, followingUserId);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (FriendShipNotFoundException ex)
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
