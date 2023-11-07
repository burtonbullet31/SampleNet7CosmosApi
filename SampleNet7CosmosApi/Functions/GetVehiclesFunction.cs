using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SampleNet7CosmosApi.Interfaces;
using SampleNet7CosmosApi.Models;

namespace SampleNet7CosmosApi.Functions;

public class GetVehiclesFunction
{
    private readonly IService _vehicleService;

    public GetVehiclesFunction(IService service) => _vehicleService = service;
    
    [Function("GetVehicles")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/vehicles")] HttpRequestData req)
    {
        var vehicleType = req.Query["type"];
        if (string.IsNullOrWhiteSpace(vehicleType))
        {
            var res = req.CreateResponse(HttpStatusCode.BadRequest);
            return res;
        }
        
        IEnumerable<BaseVehicleModel> vehicles = vehicleType switch
        {
            "sedan" => await _vehicleService.GetVehicles<SedanModel>(vehicleType),
            "pickup" => await _vehicleService.GetVehicles<PickupModel>(vehicleType),
            "suv" => await _vehicleService.GetVehicles<SuvModel>(vehicleType),
            _ => Array.Empty<BaseVehicleModel>()
        };

        var response = req.CreateResponse();
        if (!vehicles.Any())
        {
            response.StatusCode = HttpStatusCode.NoContent;
            return response;
        }

        await response.WriteAsJsonAsync(vehicles);
        return response;
    }
}