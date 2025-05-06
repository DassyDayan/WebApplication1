using WebApplication1.Dto.Classes;
using WebApplication1.Models;

namespace WebApplication1.BL.Interfaces
{
    public interface IMatriculationServiceBL
    {
        Task<UserCredentialsResult?> GetUserCredentialsByCredentials(string username, string password);
        Task<bool> UpdateMatriculationDataAsync(string username, string password, UpdateMatriculationDataRequest request);
    }
}