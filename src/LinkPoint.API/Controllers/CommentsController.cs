using LinkPoint.Business.DTOs.CommentDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotValidExceptions;
using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCommentForPost(int PostId) 
        {
            try
            {
                return Ok(await _commentService.GetAllCommentForPostAsync(PostId));
            }
            catch (CommentNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            } 
            catch (ProfileImageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateComment(CommentPostDto commentPostDto)
        {
            try
            {
                
                return Ok( await _commentService.CreateCommentAsync(commentPostDto));
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
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateComment(int CommentId, CommentPutDto commentPutDto)
        {
            try
            {
                await _commentService.UpdateCommentAsync(CommentId,commentPutDto);
                return Ok();
            }
            catch (IdNotValidException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (CommentNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> SoftDeleteComment(int CommentId, CommentDeleteDto commentDeleteDto)
        {
            try
            {
                await _commentService.SoftDeleteCommentAsync(CommentId, commentDeleteDto);
                return Ok();
            }
            catch (IdNotValidException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (CommentNotFoundException ex)
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
