using AutoMapper;
using TrackingFolder.API.Contracts;
using TrackingFolder.API.Contracts.Requests;
using TrackingFolder.API.Models;
namespace TrackingFolder.API.Mapper
{
    public class GExMeasureResultProfile : Profile
    {
        public GExMeasureResultProfile()
        {
            //CreateMap<CreateGoldExpertMeasureResult, GExMeasureResult>()
            //    .ForMember(dest => dest.Id, otp => otp.Ignore())
            //    .ForMember(dest => dest.GExMachineId, otp => otp.Ignore())
            //    .ForMember(dest => dest.IsDeleted, otp => otp.Ignore())
            //    .ForMember(dest => dest.DeletedBy, otp => otp.Ignore())
            //    .ForMember(dest => dest.DeletedOn, otp => otp.Ignore())
            //    .ForMember(dest => dest.ModifiedBy, otp => otp.Ignore())
            //    .ForMember(dest => dest.ModifiedOn, otp => otp.Ignore());

            ////CreateMap<GExMeasureResult, GExMeasureResponse>().ReverseMap();

            //CreateMap<UpdateGoldExpertMeasureResult, GExMeasureResult>()
            //    .ForMember(dest => dest.Id, opt => opt.Ignore()); // Id is not updated

            //CreateMap<GExMeasureResult, GExMeasureResponse>();

            // Map Create DTO to Entity
            CreateMap<CreateGoldExpertMeasureResult, GExMeasureResult>();

            // Map Update DTO to Entity
            CreateMap<UpdateGoldExpertMeasureResult, GExMeasureResult>();

            // Map Entity to Response DTO
            CreateMap<GExMeasureResult, GExMeasureResponse>();
        }
    }
}
