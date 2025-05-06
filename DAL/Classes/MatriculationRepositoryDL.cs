using WebApplication1.DAL.Interfaces;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dto.Classes;

namespace WebApplication1.DAL.Classes
{
    public class MatriculationRepositoryDL : IMatriculationRepositoryDL
    {
        private readonly RegisteretionContextDL _context;

        public MatriculationRepositoryDL(RegisteretionContextDL context)
        {
            _context = context;
        }


        public async Task<TInstitutionUser?> GetUserByCredentialsAsync(string username, string password)
        {
            return await _context.TInstitutionUser.FirstOrDefaultAsync(u => u.NvUserName == username && u.NvPassword == password);
        }

        public async Task<TModerator?> GetModeratorByIdAsync(int moderatorId)
        {
            return await _context.TModerator.FirstOrDefaultAsync(m => m.IModeratorId == moderatorId);
        }

        public async Task<bool> UpdateInstitutionAsync(int institutionId, UpdateMatriculationDataRequest request, int userId)
        {
            TInstitution? institution = await _context.TInstitution.FirstOrDefaultAsync(i => i.IInstitutionId == institutionId);
            if (institution == null)
                return false;

            institution.NvBiologyCoordinatorName = request.CoordinatorName;
            institution.NvCoordinatorMail = request.CoordinatorEmail;
            institution.NvCoordinatorPhone = request.CoordinatorPhone;
            institution.ILastModifyUserId = userId;
            institution.DtLastModifyDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMatriculationInstitutionAsync(int institutionId, int laboratoryRooms, int moderatorId, int userId)
        {
            int matriculationId = await _context.TMatriculation.MaxAsync(m => (int?)m.IMatriculationId) ?? 0;
            if (matriculationId == 0)
            {
                return false;
            }

            TModerator? moderator = await GetModeratorByIdAsync(moderatorId);
            if (moderator == null)
            {
                throw new InvalidOperationException($"Moderator with ID {moderatorId} not found.");
            }

            TMatriculationInstitution? matriculationInstitution = await _context.TMatriculationInstitution
               .FirstOrDefaultAsync(m => m.IInstitutionId == institutionId && m.IMatriculationId == matriculationId);

            if (matriculationInstitution == null)
            {
                matriculationInstitution = new TMatriculationInstitution
                {
                    IInstitutionId = institutionId,
                    IMatriculationInstitutionId = matriculationInstitution!.IMatriculationInstitutionId,
                    IMatriculationId = matriculationId,
                    IRegistrationType = 632,
                    IDeliveryType = 13,
                    IDeliveryModeratorId = moderatorId,
                    ILaboratoryRooms = laboratoryRooms,
                    ICreateByUserId = userId,
                    DtCreateDate = DateTime.Now,
                    ILastModifyUserId = userId,
                    DtLastModifyDate = DateTime.Now,
                    ISysRowStatus = 1
                };
                _context.TMatriculationInstitution.Add(matriculationInstitution);
            }
            else
            {
                matriculationInstitution.IDeliveryModeratorId = moderatorId;
                matriculationInstitution.ILaboratoryRooms = laboratoryRooms;
                matriculationInstitution.DtLastModifyDate = DateTime.Now;
                matriculationInstitution.ILastModifyUserId = userId;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMatriculationInstitutionTestersAsync(int institutionId, IEnumerable<string> testers, int userId)
        {
            TMatriculationInstitution? matriculationInstitution = await _context.TMatriculationInstitution
                .FirstOrDefaultAsync(m => m.IInstitutionId == institutionId);

            if (matriculationInstitution == null)
                return false;

            int matriculationInstitutionId = matriculationInstitution.IMatriculationInstitutionId;

            IQueryable<TMatriculationInstitutionTester> existingTesters = _context.TMatriculationInstitutionTester
                .Where(t => t.IMatriculationInstitutionId == matriculationInstitutionId);
            _context.TMatriculationInstitutionTester.RemoveRange(existingTesters);

            await _context.SaveChangesAsync();

            foreach (string teacherName in testers)
            {
                TMatriculationInstitutionTester tester = new TMatriculationInstitutionTester
                {
                    IMatriculationInstitutionId = matriculationInstitutionId,
                    NvTesterName = teacherName,
                    ICreateByUserId = userId,
                    DtCreateDate = DateTime.Now,
                    ILastModifyUserId = userId,
                    DtLastModifyDate = DateTime.Now,
                    ISysRowStatus = 1
                };
                _context.TMatriculationInstitutionTester.Add(tester);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MatriculationParamDto>> GetLast3ParamsByMaxMatriculationIdAsync()
        {
            int maxMatriculationId = await _context.TMatriculationParams
                .MaxAsync(p => (int?)p.IMatriculationId) ?? 0;

            List<MatriculationParamDto> result = await _context.TMatriculationParams
                .Where(p => p.IMatriculationId == maxMatriculationId)
                .OrderByDescending(p => p.IMatriculationParamId)
                .Take(3)
                .Select(p => new MatriculationParamDto
                {
                    iMatriculationParamId = p.IMatriculationParamId,
                    iMatriculationId = p.IMatriculationId,
                    nvParamName = p.NvParamName
                })
                .ToListAsync();

            return result;
        }

        public async Task<bool> AddInstitutionParamsAsync(int classParamId, int morningTimeId,
            int afternoonTimeId, int valueMorning, int valueAfternoon, int userId,int institutionId)
        {
            TMatriculationInstitution? matriculationInstitution = await _context.TMatriculationInstitution.FirstOrDefaultAsync(m => m.IInstitutionId == institutionId);
            if (matriculationInstitution==null)
            {
                return false;
            }

            DateTime now = DateTime.Now;
            List<TMatriculationInstitutionParams> newRows =
                new List<TMatriculationInstitutionParams>
            {
                new TMatriculationInstitutionParams
                {
                    IMatriculationInstitutionId = matriculationInstitution.IMatriculationInstitutionId,
                    IMatriculationParamClassId = classParamId,
                    IMatriculationParamTimeId = morningTimeId,
                    IValue = valueMorning,
                    ICreateByUserId = userId,
                    DtCreateDate = now,
                    ILastModifyUserId = userId,
                    DtLastModifyDate = now,
                    ISysRowStatus = 1
                },
                new TMatriculationInstitutionParams
                {
                    IMatriculationInstitutionId = matriculationInstitution.IMatriculationInstitutionId,
                    IMatriculationParamClassId = classParamId,
                    IMatriculationParamTimeId = afternoonTimeId,
                    IValue = valueAfternoon,
                    ICreateByUserId = userId,
                    DtCreateDate = now,
                    ILastModifyUserId = userId,
                    DtLastModifyDate = now,
                    ISysRowStatus = 1
                }
            };

            _context.TMatriculationInstitutionParams.AddRange(newRows);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}