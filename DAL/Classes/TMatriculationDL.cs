using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL.Interfaces;
using WebApplication1.Dto.Classes;
using WebApplication1.Models;

namespace WebApplication1.DAL.Classes
{
    public class TMatriculationDL: IMatriculationDL
    {
        private readonly RegisteretionContextDL _context;

        public TMatriculationDL(RegisteretionContextDL context)
        {
            _context = context;
        }

        public async Task<TMatriculationDto?> GetLatestMatriculationInfoAsync()
        {
            TMatriculationDto? matriculation = await _context.TMatriculation
                    .OrderByDescending(m => m.IMatriculationId)
                    .Select(m => new TMatriculationDto
                    {
                        NvMatriculationName = m.NvMatriculationName,
                        DtMatriculationDate = m.DtMatriculationDate,
                        DtStudentsLastUpdateDate = m.DtStudentsLastUpdateDate
                    })
                    .FirstOrDefaultAsync();

            return matriculation;
        }
    }
}