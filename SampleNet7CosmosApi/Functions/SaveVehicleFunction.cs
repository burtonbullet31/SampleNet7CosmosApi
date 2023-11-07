using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SampleNet7CosmosApi.Interfaces;
using SampleNet7CosmosApi.Models;

namespace SampleNet7CosmosApi.Functions;

public class SaveVehicleFunction
{
    private readonly IService _service;

    public SaveVehicleFunction(IService service) => _service = service;
    
    [Function("SaveVehicleFunctionV1")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/vehicles")] HttpRequestData req)
    {
        var data = await req.ReadFromJsonAsync<InboundBaseVehicle>();

        if (data == null)
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        BaseVehicleModel vehicle = data.Type.ToLower() switch
        {
            "sedan" => new SedanModel
            {
                AllWheelDrive = (bool)data.AllWheelDrive!
            },
            "pickup" => new PickupModel
            {
                TowCapacityPounds = (ushort)data.TowCapacityPounds!,
                BedLengthInFeet = (ushort)data.BedLengthInFeet!,
                FourWheelDrive = (bool)data.FourWheelDrive!
            },
            "suv" => new SuvModel
            {
                FourWheelDrive = (bool)data.FourWheelDrive!,
                AllWheelDrive = (bool)data.AllWheelDrive!
            },
            _ => throw new Exception("Invalid vehicle")
        };

        vehicle.FuelCapacity = data.FuelCapacity;
        vehicle.EngineType = data.EngineType;
        vehicle.TransmissionType = data.TransmissionType;
        vehicle.Id = Guid.NewGuid().ToString();
        vehicle.Make = data.Make;
        vehicle.Model = data.Model;
        vehicle.Year = data.Year;
        vehicle.FuelType = data.FuelType;
        vehicle.NumberOfDoors = data.NumberOfDoors;
        
        var vehicleModel = await _service.SaveVehicle(vehicle, data.Type);

        var response = req.CreateResponse();
        await response.WriteAsJsonAsync(vehicleModel);
        response.StatusCode = HttpStatusCode.OK;
        return response;
        
    }
}