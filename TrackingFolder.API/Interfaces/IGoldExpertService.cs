using TrackingFolder.API.Contracts;
using TrackingFolder.API.Contracts.Requests;

namespace TrackingFolder.API.Interfaces
{
    public interface IGoldExpertService
    {
        Task<GExMeasureResponse> AddExpertMeasureAsync(CreateGoldExpertMeasureResult createGExMeasureRequest);
        Task<GExMeasureResponse> GetExpertMeasureByIdAsync(Guid id);
        Task<IEnumerable<GExMeasureResponse>> GetExpertMeasuresAsync();
        Task<GExMeasureResponse> UpdateExpertMeasureAsync(Guid id, UpdateGoldExpertMeasureResult updateGExMeasureRequest);
        Task<bool> DeleteExpertMeasureAsync(Guid id);
    }
}
