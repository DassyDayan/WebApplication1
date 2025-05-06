using AutoMapper;
using WebApplication1.BL.Interfaces;
using WebApplication1.DAL.Interfaces;
using WebApplication1.Dto.Classes;
using WebApplication1.Models;

namespace WebApplication1.BL.Classes
{
    public class TModeratorBL : ITModeratorBL
    {
        ITModeratorDL _ITModeratorDL;
        IMapper _mapper;
        public TModeratorBL(ITModeratorDL moderatorDL, IMapper mapper)
        {
            _ITModeratorDL = moderatorDL;
            this._mapper = mapper;
        }
        public async Task<List<TModeratorDTO>> GetModeratorsAsync()
        {
            List<TModerator> listModerators = await _ITModeratorDL.GetAllModeratorsAsync();
            List<TModerator> filteredModerators = listModerators.Where(m => m.ISysRowStatus == 1).ToList();
            return _mapper.Map<List<TModeratorDTO>>(filteredModerators); 
        }

        public async Task<TModeratorDTO> GetModeratorDtoById(int moderatorId)
        {
            TModerator? moderator= await _ITModeratorDL.GetModeratorDtoById(moderatorId);
            return _mapper.Map<TModerator, TModeratorDTO>(moderator!);
        }
    }
}