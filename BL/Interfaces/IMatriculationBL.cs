using WebApplication1.Dto.Classes;

namespace WebApplication1.BL.Interfaces
{
    public interface IMatriculationBL
    {
        Task<TMatriculationDto?> GetLatestMatriculationInfoAsync();
    }
}