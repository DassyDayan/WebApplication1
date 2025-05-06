using WebApplication1.Models;

namespace WebApplication1.DAL.Interfaces
{
    public interface ITModeratorDL
    {
        Task<List<TModerator>> GetAllModeratorsAsync();
        Task<bool> IsModeratorExistsAsync(int moderatorId, CancellationToken cancellation);
        Task<TModerator?> GetModeratorDtoById(int moderatorId);
    }
}