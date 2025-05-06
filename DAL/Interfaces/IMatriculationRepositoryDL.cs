using WebApplication1.Dto.Classes;
using WebApplication1.Models;

namespace WebApplication1.DAL.Interfaces
{
    public interface IMatriculationRepositoryDL
    {
        Task<TInstitutionUser?> GetUserByCredentialsAsync(string username, string password);
        Task<TModerator?> GetModeratorByIdAsync(int moderatorId);
        Task<bool> UpdateInstitutionAsync(int institutionId, UpdateMatriculationDataRequest request, int userId);
        Task<bool> UpdateMatriculationInstitutionAsync(int institutionId, int laboratoryRooms,int moderatorId, int userId);
        Task<bool> UpdateMatriculationInstitutionTestersAsync(int institutionId, IEnumerable<string> testers, int userId);
        Task<List<MatriculationParamDto>> GetLast3ParamsByMaxMatriculationIdAsync();
        Task<bool> AddInstitutionParamsAsync(int classParamId, int morningTimeId,
            int afternoonTimeId, int valueMorning, int valueAfternoon, int userId,int institutionId);
    }
}