using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using SampleNet7CosmosApi_Old.Interfaces;
using SampleNet7CosmosApi_Old.Models;

namespace SampleNet7CosmosApi_Old.Repositories;

public class CosmosDbRepository: IDbRepository
{
    private readonly Container _container;

    public CosmosDbRepository(CosmosClient client, string dbName, string containerName) =>
        _container = client.GetContainer(dbName, containerName);

    public async Task DeleteVehicle(string id) =>
    await _container.DeleteItemAsync<BaseVehicleModel>(id, new PartitionKey());

    public async Task<BaseVehicleModel> GetVehicle(string id)
    {
        try
        {
            var response = await _container.ReadItemAsync<BaseVehicleModel>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<IEnumerable<BaseVehicleModel>> GetVehicles()
    {
        var query = _container.GetItemQueryIterator<BaseVehicleModel>(new QueryDefinition("SELECT * FROM c"));
        var results = new List<BaseVehicleModel>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }
        return results;
    }

    public async Task<BaseVehicleModel> SaveVehicle(BaseVehicleModel vehicle)
    {
        var response = await _container.CreateItemAsync(vehicle, new PartitionKey(vehicle.Id));
        return response.Resource;
    }

    public async Task<BaseVehicleModel> UpdateVehicle(string id, BaseVehicleModel vehicle)
    {
        var response = await _container.UpsertItemAsync(vehicle, new PartitionKey(id));
        return response.Resource;
    }
}