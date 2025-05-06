using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.DAL.Classes
{
    public class TModeratorDL : ITModeratorDL
    {
        private readonly RegisteretionContextDL _context;
        public TModeratorDL(RegisteretionContextDL context)
        {
            _context = context;
        }
        public async Task<List<TModerator>> GetAllModeratorsAsync()
        {
            return await _context.TModerator.ToListAsync();
        }
        public async Task<bool> IsModeratorExistsAsync(int moderatorId, CancellationToken cancellation)
        {
            return await _context.TModerator.AnyAsync(m => m.IModeratorId == moderatorId, cancellationToken: cancellation);
        }

        public async Task<TModerator?> GetModeratorDtoById(int moderatorId)
        {
            return await _context.TModerator.FirstOrDefaultAsync(m => m.IModeratorId == moderatorId);
        }
    }
}