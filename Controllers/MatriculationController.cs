using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.BL.Classes;
using WebApplication1.Models;
using FluentValidation.Results;

namespace WebApplication1.Controllers
{
    public class MatriculationController : Controller
    {
        private readonly MatriculationServiceBL _service;

        public MatriculationController(MatriculationServiceBL service)
        {
            _service = service;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromQuery] string username, [FromQuery] string password, 
            [FromBody] UpdateMatriculationDataRequest request, 
            [FromServices] IValidator<UpdateMatriculationDataRequest> validator,
            CancellationToken cancellationToken)
        {
            try
            {
                ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    IEnumerable<object> errors = validationResult.Errors
                        .Select(e => new { e.PropertyName, e.ErrorMessage });
                    return BadRequest(errors);
                }
                bool success = await _service.UpdateMatriculationDataAsync(username, password, request);
                if (!success)
                    return Unauthorized("Invalid credentials or institution not found.");
                return Ok(new { success = true, message = "Data updated/saved successfully" });
            }
            catch (Exception)
            {
                throw new Exception("Error updated registeration");
            } 
        }
    }
}