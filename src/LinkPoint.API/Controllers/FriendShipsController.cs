using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAcceptedFollowingUsers()
        {
            try
            {
                return Ok(await _friendShipService.GetAllAcceptedFollowingUsersAsync());
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
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAcceptedFollowerUsers()
        {
            try
            {
                return Ok(await _friendShipService.GetAllAcceptedFollowerUsersAsync());
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
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllPendingFollowerUsers()
        {
            try
            {
                return Ok(await _friendShipService.GetAllPendingFollowerUsersAsync());
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
        [HttpPost("[action]/{followingUserId}")]
        public async Task<IActionResult> AddToFriendShip(string followingUserId)
        {
            try
            {
                await _friendShipService.AddToFriendShipAsync(followingUserId);
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
    }
}
