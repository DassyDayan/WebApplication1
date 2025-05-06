using WebApplication1.BL.Interfaces;
using WebApplication1.DAL.Interfaces;
using WebApplication1.Dto.Classes;

namespace WebApplication1.BL.Classes
{
    public class MatriculationBL:IMatriculationBL
    {
        private readonly IMatriculationDL _matriculationRepository;

        public MatriculationBL(IMatriculationDL matriculationRepository)
        {
            _matriculationRepository = matriculationRepository;
        }

        public async Task<TMatriculationDto?> GetLatestMatriculationInfoAsync()
        {
            return await _matriculationRepository.GetLatestMatriculationInfoAsync();
        }
    }
}