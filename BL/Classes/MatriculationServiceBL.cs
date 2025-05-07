using WebApplication1.Models;
using WebApplication1.Dto.Classes;
using WebApplication1.BL.Interfaces;
using WebApplication1.DAL.Interfaces;

namespace WebApplication1.BL.Classes
{
  public class MatriculationServiceBL : IMatriculationServiceBL
  {
    private readonly IMatriculationRepositoryDL _matriculationRepositoryDL;
    private readonly IEmailService _emailService;
    private readonly IMatriculationDL _matriculationRepository;
    private readonly ITModeratorBL _moderatorBL;
    public MatriculationServiceBL(IMatriculationRepositoryDL matriculationRepositoryDL,
        IEmailService emailService,
        IMatriculationDL matriculationDL,
        ITModeratorBL moderatorBL)
    {
      _matriculationRepositoryDL = matriculationRepositoryDL;
      _emailService = emailService;
      _matriculationRepository = matriculationDL;
      _moderatorBL = moderatorBL;
    }

    public async Task<UserCredentialsResult?> GetUserCredentialsByCredentials(string username, string password)
    {
      TInstitutionUser? user = await _matriculationRepositoryDL.GetUserByCredentialsAsync(username, password);
      return user == null ? null : new UserCredentialsResult
      {
        InstitutionId = user.IInstitutionId,
        UserId = user.IUserId
      };
    }

    public async Task<bool> UpdateMatriculationDataAsync(string username, string password, UpdateMatriculationDataRequest request)
    {
      UserCredentialsResult? credentials = await GetUserCredentialsByCredentials(username, password);
      if (credentials == null)
        return false;

      int institutionId = credentials.InstitutionId;
      int userId = credentials.UserId;

      using var transaction = await _matriculationRepositoryDL.BeginTransactionAsync();

      try
      {
        bool isInstitutionUpdated = await _matriculationRepositoryDL.UpdateInstitutionAsync(institutionId, request, userId);
        if (!isInstitutionUpdated)
          throw new Exception("Institution update failed");

        bool isMatriculationInstitutionUpdated = await _matriculationRepositoryDL
            .UpdateMatriculationInstitutionAsync(institutionId, request.LaboratoryRooms, request.ModeratorId, userId);
        if (!isMatriculationInstitutionUpdated)
          throw new Exception("MatriculationInstitution update failed");

        bool isTestersUpdated = await _matriculationRepositoryDL
            .UpdateMatriculationInstitutionTestersAsync(institutionId, request.AccompanyingTeachers, userId);
        if (!isTestersUpdated)
          throw new Exception("Testers update failed");

        List<MatriculationParamDto> matriculationParams = await _matriculationRepositoryDL.GetLast3ParamsByMaxMatriculationIdAsync();

        int classParamId = matriculationParams.FirstOrDefault(mp => mp.nvParamName == "כל הכיתות")?.iMatriculationParamId
            ?? throw new Exception("פרמטר 'כל הכיתות' לא נמצא");

        int morningParam = matriculationParams.FirstOrDefault(mp => mp.nvParamName == "בוקר")?.iMatriculationParamId
            ?? throw new Exception("פרמטר 'בוקר' לא נמצא");

        int afternoonParam = matriculationParams.FirstOrDefault(mp => mp.nvParamName == "צהריים")?.iMatriculationParamId
            ?? throw new Exception("פרמטר 'צהריים' לא נמצא");

        bool isExamineesAdded = await _matriculationRepositoryDL.AddInstitutionParamsAsync(
            classParamId, morningParam, afternoonParam,
            request.MorningTesters, request.EveningTesters,
            userId, institutionId);

        if (!isExamineesAdded)
          throw new Exception("Adding examinees failed");

        await transaction.CommitAsync();

        TMatriculationDto? datesAndMatriculationName = await _matriculationRepository.GetLatestMatriculationInfoAsync();
        TModeratorDTO moderator = await _moderatorBL.GetModeratorDtoById(request.ModeratorId);
        string region = $"{moderator.NvFirstName}, {moderator.NvLastName} {moderator.NvRegion}";

        bool success = await _emailService.SendNewEmailAsync(request, datesAndMatriculationName!, region);
        return success;
      }
      catch
      {
        await transaction.RollbackAsync();
        return false;
      }
    }
  }
}
