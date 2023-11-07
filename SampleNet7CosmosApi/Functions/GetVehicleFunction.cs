using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using SampleNet7CosmosApi.Interfaces;
using SampleNet7CosmosApi.Models;

namespace SampleNet7CosmosApi.Functions;

public class GetVehicleFunction
{
    private readonly IService _service;

    public GetVehicleFunction(IService service) => _service = service;
    
    [Function("GetVehicleFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/vehicle")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger("GetVehicleFunction");
        logger.LogInformation("C# HTTP trigger function processed a request.");

        var id = req.Query["id"];
        var type = req.Query["type"];

        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(type))
        {
            var error = req.CreateResponse(HttpStatusCode.BadRequest);
            return error;
        }
        BaseVehicleModel? vehicle = type.ToLower() switch
        {
            "sedan" => await _service.GetVehicle<SedanModel>(id, type),
            "pickup" => await _service.GetVehicle<PickupModel>(id, type),
            "suv" => await _service.GetVehicle<SuvModel>(id, type),
            _ => throw new Exception("Invalid car type")
        };

        var response = req.CreateResponse();
        
        if (vehicle == null)
        {
            response.StatusCode = HttpStatusCode.NoContent;
            return response;
        }

        await response.WriteAsJsonAsync(vehicle);
        return response;
    }
}