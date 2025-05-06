using WebApplication1.Dto.Classes;

namespace WebApplication1.BL.Interfaces
{
    public interface ITModeratorBL
    {
        Task<List<TModeratorDTO>> GetModeratorsAsync();
        Task<TModeratorDTO> GetModeratorDtoById(int moderatorId);
    }
}
