using WebApplication1.Models;

namespace WebApplication1.Dto.Classes
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TModerator, TModeratorDTO>();
            CreateMap<TModeratorDTO, TModerator>();

            CreateMap<TMatriculation, TMatriculationDto>();
            CreateMap<TMatriculationDto, TMatriculation>();
        }
    }
}