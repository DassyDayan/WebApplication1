using Microsoft.AspNetCore.Mvc;
using WebApplication1.BL.Interfaces;
using WebApplication1.Dto.Classes;

namespace WebApplication1.Controllers
{
    public class ModeratorController : Controller
    {
        ITModeratorBL _ITModeratorBL;

        public ModeratorController(ITModeratorBL moderatorBL)
        {
            _ITModeratorBL = moderatorBL;
        }

        [HttpGet("GetModerators")]
        public async Task<IActionResult> GetModerators()
        {
            try
            {
                List<TModeratorDTO> moderators = await _ITModeratorBL.GetModeratorsAsync();
                if (moderators == null || moderators.Count == 0)
                {
                    return NotFound("No moderators found.");
                }
                return Ok(moderators);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving moderators.");
            }
        }
    }
}