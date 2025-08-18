using TrackingFolder.API.Contracts;
using TrackingFolder.API.Contracts.Requests;
using TrackingFolder.API.Interfaces;

namespace TrackingFolder.API.Endpoints
{
    /// <summary>
    /// Provides endpoint mappings for Gold Expert operations.
    /// </summary>
    public static class GoldExpertEnpoint
    {
        /// <summary>
        /// Maps the Gold Expert endpoints to the specified <see cref="IEndpointRouteBuilder"/>.
        /// </summary>
        /// <param name="app">The endpoint route builder to map the endpoints to.</param>
        /// <returns>The updated <see cref="IEndpointRouteBuilder"/>.</returns>
        public static IEndpointRouteBuilder MapGoldExpertEndpoints(this IEndpointRouteBuilder app)
        {
            // Define the endpoints

            // GET: /api/v1/gold-expert-measures
            app.MapGet("/api/v1/gold-expert-measures", async (IGoldExpertService service) =>
            {
                return await service.GetExpertMeasuresAsync();
            })
            .WithName("GetAllGoldExpertMeasures")
            .Produces<IEnumerable<GExMeasureResponse>>(StatusCodes.Status200OK);

            // GET: /api/v1/gold-expert-measures/{id: guid}
            app.MapGet("/api/v1/gold-expert-measures/{id:guid}", async (Guid id, IGoldExpertService service) =>
            {
                return await service.GetExpertMeasureByIdAsync(id);
            })
            .WithName("GetGoldExpertMeasureById")
            .Produces<GExMeasureResponse>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status404NotFound);

            // POST: /api/v1/gold-expert-measures
            app.MapPost("/api/v1/gold-expert-measures", async (CreateGoldExpertMeasureResult request, IGoldExpertService service) =>
            {
                return await service.AddExpertMeasureAsync(request);
            })
            .WithName("AddGoldExpertMeasure")
            .Produces<GExMeasureResponse>(StatusCodes.Status201Created)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest);

            // PUT: /api/v1/gold-expert-measures/{id: guid}
            app.MapPut("/api/v1/gold-expert-measures/{id:guid}", async (Guid id, UpdateGoldExpertMeasureResult request, IGoldExpertService service) =>
            {
                return await service.UpdateExpertMeasureAsync(id, request);
            })
            .WithName("UpdateGoldExpertMeasure")
            .Produces<GExMeasureResponse>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status404NotFound);

            // DELETE: /api/v1/gold-expert-measures/{id: guid}
            app.MapDelete("/api/v1/gold-expert-measures/{id:guid}", async (Guid id, IGoldExpertService service) =>
            {
                return await service.DeleteExpertMeasureAsync(id);
            })
            .WithName("DeleteGoldExpertMeasure")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ErrorResponse>(StatusCodes.Status404NotFound);

            return app;
        }
    }
}
