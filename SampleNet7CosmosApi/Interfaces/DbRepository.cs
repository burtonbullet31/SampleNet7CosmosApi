using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;
using SampleNet7CosmosApi_Old.Models;

namespace SampleNet7CosmosApi_Old.Interfaces;

public interface IDbRepository
{
    public Task DeleteVehicle(string id);
    public Task<BaseVehicleModel> GetVehicle(string id);
    public Task<IEnumerable<BaseVehicleModel>> GetVehicles();
    public Task<BaseVehicleModel> SaveVehicle(BaseVehicleModel vehicle);
    public Task<BaseVehicleModel> UpdateVehicle(string id, BaseVehicleModel vehicle);
}
