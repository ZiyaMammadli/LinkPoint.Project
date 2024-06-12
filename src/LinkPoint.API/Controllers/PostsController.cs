using LinkPoint.Business.DTOs.PostDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotValidExceptions;
using LinkPoint.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllPosts() 
        {
            try
            {
                return Ok(await _postService.GetAllPostsAsync());
            }
            catch(PostNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);    
            } 
            catch(ProfileImageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllPostsForImage() 
        {
            try
            {
                return Ok(await _postService.GetAllPostsForImageAsync());
            }
            catch(PostNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);    
            } 
            catch(ProfileImageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllPostsForVideo() 
        {
            try
            {
                return Ok(await _postService.GetAllPostsForVideoAsync());
            }
            catch(PostNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);    
            } 
            catch(ProfileImageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        
        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> GetAllOneUserPosts(string UserId)
        {
            try
            {
                return Ok(await _postService.GetAllOneUserPostsAsync(UserId));
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
        [HttpGet("[action]/{PostId}")]
        public async Task<IActionResult> GetByIdPost(int PostId)
        {
            try
            {
                return Ok(await _postService.GetByIdPostAsync(PostId));
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
        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePostWithImage(PostCreateWithImageDto postCreateWithImageDto)
        {
            try
            {
                await _postService.CreatePostWithImageAsync(postCreateWithImageDto);
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
        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePostWithVideo(PostCreateWithVideoDto postCreateWithVideoDto)
        {
            try
            {
                await _postService.CreatePostWithVideoAsync(postCreateWithVideoDto);
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
        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePostWithText(PostCreateWithTextDto postCreateWithTextDto)
        {
            try
            {
                await _postService.CreatePostWithTextAsync(postCreateWithTextDto);
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
        [HttpPut("[action]/{PostId}")]
        public async Task<IActionResult> UpdatePostWithText(int PostId, PostUpdateWithTextDto postUpdateWithTextDto)
        {
            try
            {
                await _postService.UpdatePostWithTextAsync(PostId, postUpdateWithTextDto);
                return Ok();
            }
            catch (IdNotValidException ex)
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
        [HttpDelete("[action]/{PostId}")]
        public async Task<IActionResult> SoftDeletePost(int PostId, PostDeleteDto postDeleteDto)
        {
            try
            {
                await _postService.SoftDeletePostAsync(PostId, postDeleteDto);
                return Ok();
            }
            catch (IdNotValidException ex)
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
        [HttpDelete("[action]/{PostId}")]
        public async Task<IActionResult> DeletePost(int PostId)
        {
            try
            {
                await _postService.DeleteAsync(PostId);
                return Ok();
            }
            catch (IdNotValidException ex)
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
