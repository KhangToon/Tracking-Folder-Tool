using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
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
        private static readonly string apiVer = "v1";
        private static readonly string gExDefaultSerial = "default";
        /// <summary>
        /// Maps the Gold Expert endpoints to the specified <see cref="IEndpointRouteBuilder"/>.
        /// </summary>
        /// <param name="app">The endpoint route builder to map the endpoints to.</param>
        /// <returns>The updated <see cref="IEndpointRouteBuilder"/>.</returns>
        public static IEndpointRouteBuilder MapGoldExpertEndpoints(this IEndpointRouteBuilder app)
        {
            // GET: /api/v1/gold-expert-headers
            app.MapGet($"/api/{apiVer}/{gExDefaultSerial}/headers", async (IGoldExpertService service) =>
            {
                var request = new GetGExColumnHeadersRequest { GExSerial = gExDefaultSerial };

                var result = await service.GetCsvColumnHeaders(request);

                return Results.Ok(result.Data);
            })
            .WithName("GetAllGoldExpertDataHeadersBySerialNumber")
            .WithOpenApi();

            var dataStore = new List<Dictionary<string, string>>(); // for testing
            app.MapGet($"/api/{apiVer}/{gExDefaultSerial}", () => Results.Ok(dataStore));

            app.MapPost($"/api/{apiVer}/{gExDefaultSerial}", async ([FromBody] List<Dictionary<string, string>> data, IGoldExpertService service) =>
            {
                var result = await service.AddGExListMeasureAsync(data);

                if (result.Success)
                {
                    dataStore = data;
                }
                else dataStore.Clear();

            })
            .WithName("AddGExListMeasure")
            .WithOpenApi()
            .Produces<Response<string>>(StatusCodes.Status201Created)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest);

            // GET: /api/v1/gold-expert-measures
            app.MapGet("/api/v1/gold-expert-measures", async (IGoldExpertService service) =>
            {
                var result = await service.GetGExpertMeasuresAsync();

                return Results.Ok(result);
            })
            .WithName("GetAllGoldExpertMeasures")
            .WithOpenApi();

            // GET: /api/v1/gold-expert-measures/{id:guid}
            app.MapGet("/api/v1/gold-expert-measures/{id:guid}", async (Guid id, IGoldExpertService service) =>
            {
                return await service.GetGExpertMeasureByIdAsync(id);
            })
            .WithName("GetGoldExpertMeasureById")
            .WithOpenApi()
            .Produces<Response<GExMeasureResponse>>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status404NotFound);

            // POST: /api/v1/gold-expert-measures
            app.MapPost("/api/v1/gold-expert-measures", async (CreateGoldExpertMeasureResult request, IGoldExpertService service) =>
            {
                return await service.AddGExpertMeasureAsync(request);
            })
            .WithName("AddGoldExpertMeasure")
            .WithOpenApi()
            .Produces<Response<string>>(StatusCodes.Status201Created)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest);

            // PUT: /api/v1/gold-expert-measures/{id:guid}
            app.MapPut("/api/v1/gold-expert-measures/{id:guid}", async (Guid id, UpdateGoldExpertMeasureResult request, IGoldExpertService service) =>
            {
                return await service.UpdateGExpertMeasureAsync(id, request);
            })
            .WithName("UpdateGoldExpertMeasure")
            .WithOpenApi()
            .Produces<Response<string>>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status404NotFound);

            // DELETE: /api/v1/gold-expert-measures/{id:guid}
            app.MapDelete("/api/v1/gold-expert-measures/{id:guid}", async (Guid id, IGoldExpertService service) =>
            {
                return await service.DeleteGExpertMeasureAsync(id);
            })
            .WithName("DeleteGoldExpertMeasure")
            .WithOpenApi()
            .Produces<Response<bool>>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status404NotFound);

            return app;
        }
    }
}
