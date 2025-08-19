using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrackingFolder.API.AppContext;
using TrackingFolder.API.Contracts;
using TrackingFolder.API.Contracts.Requests;
using TrackingFolder.API.Interfaces;
using TrackingFolder.API.Models;

namespace TrackingFolder.API.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    public class GoldExpertMeasureService(ApplicationContext context, ILogger<GoldExpertMeasureService> logger, IMapper mapper) : IGoldExpertService
    {
        private readonly ApplicationContext _context = context ?? throw new ArgumentNullException(nameof(context)); // Database context
        private readonly ILogger<GoldExpertMeasureService> _logger = logger ?? throw new ArgumentNullException(nameof(logger)); // Logger for logging information and errors
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<Response<string>> AddGExpertMeasureAsync(CreateGoldExpertMeasureResult createGExMeasureRequest)
        {
            try
            {
                var measure = _mapper.Map<GExMeasureResult>(createGExMeasureRequest);

                // Check if the gold expert machine is existing
                var gExMachine = _context.GoldExpertMachines.FirstOrDefault(m => m.Id == measure.GExMachineId);

                if (gExMachine != null)
                {
                    _context.GExMeasureResults.Add(measure);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"GEx {gExMachine.SerialNumber} added measure result successfully.");

                    return new Response<string>
                    {
                        Message = "Gold expert measure result added successfully.",
                        Data = measure.Id.ToString(),
                        Success = true
                    };
                }
                else
                {
                    return new Response<string>
                    {
                        Message = $"Not found target gold expert machine with id {measure.GExMachineId}",
                        Data = null,
                        Success = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding gold expert measure result: {ex.Message}");
                return new Response<string>
                {
                    Message = "Error adding gold expert measure result.",
                    Data = null,
                    Success = false
                };
            }
        }

        public async Task<Response<bool>> DeleteGExpertMeasureAsync(Guid id)
        {
            try
            {
                var measure = await _context.GExMeasureResults.FirstOrDefaultAsync(m => m.Id == id);
                if (measure == null)
                {
                    _logger.LogWarning($"Attempted to delete non-existent measure with ID {id}.");
                    return new Response<bool>
                    {
                        Message = $"Measure with ID {id} not found.",
                        Data = false,
                        Success = false
                    };
                }

                _context.GExMeasureResults.Remove(measure);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Measure with ID {id} deleted successfully.");
                return new Response<bool>
                {
                    Message = "Measure deleted successfully.",
                    Data = true,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting measure with ID {id}: {ex.Message}");
                return new Response<bool>
                {
                    Message = "Error deleting measure.",
                    Data = false,
                    Success = false
                };
            }
        }

        public async Task<Response<GExMeasureResponse>> GetGExpertMeasureByIdAsync(Guid id)
        {
            try
            {
                var measure = await _context.GExMeasureResults.FirstOrDefaultAsync(m => m.Id == id);
                if (measure == null)
                {
                    _logger.LogWarning($"Measure with ID {id} not found.");
                    return new Response<GExMeasureResponse>
                    {
                        Message = $"Measure with ID {id} not found.",
                        Data = null,
                        Success = false
                    };
                }

                var response = _mapper.Map<GExMeasureResponse>(measure);
                _logger.LogInformation($"Retrieved measure with ID {id} successfully.");
                return new Response<GExMeasureResponse>
                {
                    Message = "Measure retrieved successfully.",
                    Data = response,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving measure with ID {id}: {ex.Message}");
                return new Response<GExMeasureResponse>
                {
                    Message = "Error retrieving measure.",
                    Data = null,
                    Success = false
                };
            }
        }

        public async Task<Response<IEnumerable<GExMeasureResponse>>> GetGExpertMeasuresAsync()
        {
            try
            {
                var measures = await _context.GExMeasureResults.ToListAsync();
                var response = _mapper.Map<IEnumerable<GExMeasureResponse>>(measures);
                _logger.LogInformation($"Retrieved {measures.Count} measures successfully.");
                return new Response<IEnumerable<GExMeasureResponse>>
                {
                    Message = "Measures retrieved successfully.",
                    Data = response,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving measures: {ex.Message}");
                return new Response<IEnumerable<GExMeasureResponse>>
                {
                    Message = "Error retrieving measures.",
                    Data = null,
                    Success = false
                };
            }
        }

        public async Task<Response<string>> UpdateGExpertMeasureAsync(Guid id, UpdateGoldExpertMeasureResult updateGExMeasureRequest)
        {
            try
            {
                var measure = await _context.GExMeasureResults.FirstOrDefaultAsync(m => m.Id == id);
                if (measure == null)
                {
                    _logger.LogWarning($"Measure with ID {id} not found for update.");
                    return new Response<string>
                    {
                        Message = $"Measure with ID {id} not found.",
                        Data = null,
                        Success = false
                    };
                }

                // Check if the gold expert machine exists
                var gExMachine = await _context.GoldExpertMachines.FirstOrDefaultAsync(m => m.Id == measure.GExMachineId);
                if (gExMachine == null)
                {
                    _logger.LogWarning($"Gold expert machine with ID {measure.GExMachineId} not found.");
                    return new Response<string>
                    {
                        Message = $"Not found target gold expert machine with ID {measure.GExMachineId}.",
                        Data = null,
                        Success = false
                    };
                }

                _mapper.Map(updateGExMeasureRequest, measure);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Measure with ID {id} updated successfully.");
                return new Response<string>
                {
                    Message = "Measure updated successfully.",
                    Data = measure.Id.ToString(),
                    Success = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating measure with ID {id}: {ex.Message}");
                return new Response<string>
                {
                    Message = "Error updating measure.",
                    Data = null,
                    Success = false
                };
            }
        }
    }
}
