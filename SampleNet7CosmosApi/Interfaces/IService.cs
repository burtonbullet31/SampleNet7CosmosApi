using SampleNet7CosmosApi.Models;

namespace SampleNet7CosmosApi.Interfaces;

public interface IService
{
    Task<TVehicleType?> GetVehicle<TVehicleType>(string id, string type);
    Task<IEnumerable<TVehicleType>> GetVehicles<TVehicleType>(string type);
    Task<TVehicleType> SaveVehicle<TVehicleType>(TVehicleType vehicleModel, string type);
}