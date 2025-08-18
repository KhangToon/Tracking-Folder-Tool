using TrackingFolder.API.AppContext;
using TrackingFolder.API.Contracts;
using TrackingFolder.API.Contracts.Requests;
using TrackingFolder.API.Interfaces;

namespace TrackingFolder.API.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    public class GoldExpertMeasureService(ApplicationContext context, ILogger<GoldExpertMeasureService> logger) : IGoldExpertService
    {
        private readonly ApplicationContext _context = context ?? throw new ArgumentNullException(nameof(context)); // Database context
        private readonly ILogger<GoldExpertMeasureService> _logger = logger ?? throw new ArgumentNullException(nameof(logger)); // Logger for logging information and errors

        public Task<GExMeasureResponse> AddExpertMeasureAsync(CreateGoldExpertMeasureResult createGExMeasureRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteExpertMeasureAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GExMeasureResponse> GetExpertMeasureByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GExMeasureResponse>> GetExpertMeasuresAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GExMeasureResponse> UpdateExpertMeasureAsync(Guid id, UpdateGoldExpertMeasureResult updateGExMeasureRequest)
        {
            throw new NotImplementedException();
        }
    }
}
