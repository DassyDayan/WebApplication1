using Microsoft.AspNetCore.Mvc;
using WebApplication1.BL.Interfaces;
using WebApplication1.Dto.Classes;

namespace WebApplication1.Controllers
{
    public class MatriculationDatesController : Controller
    {
        private readonly IMatriculationBL _matriculationService;

        public MatriculationDatesController(IMatriculationBL matriculationService)
        {
            _matriculationService = matriculationService;
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestMatriculationInfo()
        {
            try
            {
                TMatriculationDto? result = await _matriculationService.GetLatestMatriculationInfoAsync();

                if (result == null)
                    return NotFound("No matriculation data found.");

                return Ok(result);
            }
            catch (Exception)
            {
                throw new Exception("Error getting dates.");
            }
           
        }
    }
}
