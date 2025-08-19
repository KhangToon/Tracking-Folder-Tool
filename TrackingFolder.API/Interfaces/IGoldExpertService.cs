using TrackingFolder.API.Contracts;
using TrackingFolder.API.Contracts.Requests;

namespace TrackingFolder.API.Interfaces
{
    public interface IGoldExpertService
    {
        Task<Response<string>> AddGExpertMeasureAsync(CreateGoldExpertMeasureResult createGExMeasureRequest);
        Task<Response<GExMeasureResponse>> GetGExpertMeasureByIdAsync(Guid id);
        Task<Response<IEnumerable<GExMeasureResponse>>> GetGExpertMeasuresAsync();
        Task<Response<string>> UpdateGExpertMeasureAsync(Guid id, UpdateGoldExpertMeasureResult updateGExMeasureRequest);
        Task<Response<bool>> DeleteGExpertMeasureAsync(Guid id);
    }
}
