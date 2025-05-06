using WebApplication1.Dto.Classes;

namespace WebApplication1.DAL.Interfaces
{
    public interface IMatriculationDL
    {
        Task<TMatriculationDto?> GetLatestMatriculationInfoAsync();
    }
}