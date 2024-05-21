using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        private readonly IConversationService _conversationService;

        public ConversationsController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }
        [HttpGet("[action]")]
        public async Task <IActionResult> GetAllConversations()
        {
            try
            {
               return Ok(await _conversationService.GetAllConversationsAsync());
            }
            catch(ConversationNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }
            catch(UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }
            catch(ProfileImageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }
            catch(MessageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
        [HttpGet("[action]/{conversationId}")]
        public async Task <IActionResult> GetByIdConversation(int conversationId)
        {
            try
            {
               return Ok(await _conversationService.GetByIdConversationAsync(conversationId));
            }
            catch(ConversationNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }
            catch(UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }
            catch(ProfileImageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }
            catch(MessageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
    }
}
