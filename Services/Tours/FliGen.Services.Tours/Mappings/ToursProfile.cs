using AutoMapper;

namespace FliGen.Services.Tours.Mappings
{
    public class ToursProfile : Profile
    {
        public ToursProfile()
        {
            CreateMap<Domain.Entities.Tour, Application.Dto.TourDto>()
                .ForMember(x => x.Date, t => t.MapFrom(s => s.Date.ToString("yyyy-MM-dd")))
                .ForMember(x => x.HomeCount, t => t.MapFrom(s => s.HomeCount))
                .ForMember(x => x.GuestCount, t => t.MapFrom(s => s.GuestCount))
                .ForMember(x => x.SeasonId, t => t.MapFrom(s => s.SeasonId))
                .ForMember(x => x.TourStatus, t => t.MapFrom(s => s.TourStatus.Id));
        }
    }
}