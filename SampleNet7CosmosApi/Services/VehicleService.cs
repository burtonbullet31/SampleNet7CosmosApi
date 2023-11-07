using SampleNet7CosmosApi.Interfaces;
using SampleNet7CosmosApi.Models;

namespace SampleNet7CosmosApi.Services;

public class VehicleService: IService
{
    private readonly IDbRepository _sedanRepository;
    private readonly IDbRepository _pickupRepository;
    private readonly IDbRepository _suvRepository;
    
    public VehicleService(IDbRepository sedanRepository, IDbRepository pickupRepository, IDbRepository suvRepository)
    {
        _sedanRepository = sedanRepository;
        _pickupRepository = pickupRepository;
        _suvRepository = suvRepository;
    }

    public async Task<TVehicleType?> GetVehicle<TVehicleType>(string id, string type) =>
        await _sedanRepository.GetVehicle<TVehicleType>(id, type);
    
    public async Task<IEnumerable<TVehicleType>> GetVehicles<TVehicleType>(string type) =>
        await _sedanRepository.GetVehicles<TVehicleType>(type);
    
    public async Task<TVehicleType> SaveVehicle<TVehicleType>(TVehicleType vehicleModel, string type) =>
        await _sedanRepository.SaveVehicle(vehicleModel, type);
}
