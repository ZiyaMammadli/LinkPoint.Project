using LinkPoint.Business.DTOs.ContactMessageDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactMessagesController : ControllerBase
    {
        private readonly IContactMessageService _contactMessageService;

        public ContactMessagesController(IContactMessageService contactMessageService)
        {
            _contactMessageService = contactMessageService;
        }
        [HttpGet("[action]")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> GetAllContactMessage()
        {
            try
            {
                return Ok(await _contactMessageService.GetAllContactMessageAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetAllContactMessagesForUser(string userId)
        {
            try
            {
                return Ok(await _contactMessageService.GetAllContactMessagesForUserAsync(userId));
            }
            catch(UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetContactMessageById(int id)
        {
            try
            {
                return Ok(await _contactMessageService.GetContactMessageByIdAsync(id));
            }
            catch(ContactMessageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateContactMessage(ContactMessagePostDto contactMessagePostDto)
        {
            try
            {
                await _contactMessageService.CreateContactMessageAsync(contactMessagePostDto);
                return Ok();
            }
            catch(UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode,ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("[action]/{ContactMessageId}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> AcceptContactMessage(int ContactMessageId)
        {
            try
            {
                await _contactMessageService.AcceptContactMessageAsync(ContactMessageId);
                return Ok();
            }
            catch (ContactMessageNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("[action]/{ContactMessageId}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> RejectContactMessage(int ContactMessageId)
        {
            try
            {
                await _contactMessageService.RejectContactMessageAsync(ContactMessageId);
                return Ok();
            }
            catch (ContactMessageNotFoundException ex)
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
