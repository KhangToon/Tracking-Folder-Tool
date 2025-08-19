using AutoMapper;
using TrackingFolder.API.Contracts;
using TrackingFolder.API.Models;

namespace TrackingFolder.API.Mapper
{
    public class CsvColumnHeaderProfile : Profile
    {
        public CsvColumnHeaderProfile()
        {
            CreateMap<CsvColumnHeader, CsvColumnHeaderResponse>().ReverseMap();
        }
    }
}
