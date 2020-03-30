using AutoMapper;

namespace FliGen.Services.Tours.Mappings
{
    public class ToursProfile : Profile
    {
        public ToursProfile()
        {
            CreateMap<Domain.Entities.Tour, Application.Dto.Tour>()
                .ForMember(x => x.Date, ls => ls.MapFrom(s => s.Date.ToString("yyyy-MM-dd")))
                .ForMember(x => x.HomeCount, ls => ls.MapFrom(s => s.HomeCount))
                .ForMember(x => x.GuestCount, ls => ls.MapFrom(s => s.GuestCount))
                .ForMember(x => x.SeasonId, ls => ls.MapFrom(s => s.SeasonId))
                .ForMember(x => x.TourStatus, ls => ls.MapFrom(s => s.TourStatus.Id));
        }
    }
}