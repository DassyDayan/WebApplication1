using WebApplication1.Dto.Classes;
using WebApplication1.Models;

namespace WebApplication1.BL
{
    public interface IEmailService
    {
        Task<bool> SendNewEmailAsync(UpdateMatriculationDataRequest matriculationDataRequest, TMatriculationDto matriculationDto,string region);
    }
}