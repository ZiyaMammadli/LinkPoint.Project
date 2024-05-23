using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.CommonExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikesController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpGet("[action]/{PostId}")]
        public async Task<IActionResult> GetAllUsersLikedPost(int PostId)
        {
            try
            {               
                return Ok(await _likeService.GetAllUsersLikedPostAsync(PostId));
            }
            catch (LikeNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
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
        [HttpPost("[action]/{UserId}/{PostId}")]
        public async Task<IActionResult> AddLikeToPost(string UserId,int PostId)
        {
            try
            {
                await _likeService.AddLikeToPostAsync(UserId,PostId);
                return Ok();
            }
            catch(AlreadyExistException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }
            catch(UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }
            catch(PostNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("[action]/{UserId}/{PostId}")]
        public async Task<IActionResult> RemoveLikeFromPost(string UserId,int PostId)
        {
            try
            {
                await _likeService.RemoveLikeFromPostAsync(UserId, PostId);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
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
