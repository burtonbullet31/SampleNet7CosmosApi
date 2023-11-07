using SampleNet7CosmosApi.Models;

namespace SampleNet7CosmosApi.Interfaces;

public interface IDbRepository
{
    public Task DeleteVehicle(string id);
    public Task<TVehicleModel?> GetVehicle<TVehicleModel>(string id, string type);
    public Task<IEnumerable<TVehicleModel>> GetVehicles<TVehicleModel>(string types);
    public Task<TVehicleModel> SaveVehicle<TVehicleModel>(TVehicleModel vehicle, string type);
    public Task<TVehicleModel> UpdateVehicle<TVehicleModel>(string id, TVehicleModel vehicle, string type);
}
