using System.Diagnostics;
using Microsoft.Azure.Cosmos;
using SampleNet7CosmosApi.Interfaces;
using SampleNet7CosmosApi.Models;

namespace SampleNet7CosmosApi.Repositories;

public class CosmosDbRepository: IDbRepository
{
    private readonly Container _sedanContainer;
    private readonly Container _pickupContainer;
    private readonly Container _suvContainer;

    public CosmosDbRepository(Container sedanContainer, Container pickupContainer, Container suvContainer)
    {
        _sedanContainer = sedanContainer;
        _pickupContainer = pickupContainer;
        _suvContainer = suvContainer;
    }

    public async Task DeleteVehicle(string id) =>
    await _sedanContainer.DeleteItemAsync<BaseVehicleModel>(id, new PartitionKey());

    public async Task<TVehicleModel?> GetVehicle<TVehicleModel>(string id, string type)
    {
        try
        {
            var response = type switch
            {
                "sedan" => await _sedanContainer.ReadItemAsync<TVehicleModel>(id, new PartitionKey(id)),
                "pickup" => await _pickupContainer.ReadItemAsync<TVehicleModel>(id, new PartitionKey(id)),
                "suv" => await _suvContainer.ReadItemAsync<TVehicleModel>(id, new PartitionKey(id)),
                _ => throw new Exception("Invalid car type")
            };
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return default;
        }
    }

    public async Task<IEnumerable<TVehicleModel>> GetVehicles<TVehicleModel>(string type)
    {
        var query = type switch
        {
            "sedan" => _sedanContainer.GetItemQueryIterator<TVehicleModel>(new QueryDefinition("SELECT * FROM c")),
            "pickup" => _pickupContainer.GetItemQueryIterator<TVehicleModel>(new QueryDefinition("SELECT * FROM c")),
            "suv" => _suvContainer.GetItemQueryIterator<TVehicleModel>(new QueryDefinition("SELECT * FROM c")),
            _ => throw new Exception("Invalid car type")
        };
        var results = new List<TVehicleModel>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }
        return results;
    }

    public async Task<TVehicleModel> SaveVehicle<TVehicleModel>(TVehicleModel vehicle, string type)
    {
        var response = type switch
        {
            "sedan" => await _sedanContainer.CreateItemAsync(vehicle, new PartitionKey((vehicle as BaseVehicleModel)?.Id)),
            "pickup" => await _pickupContainer.CreateItemAsync(vehicle, new PartitionKey((vehicle as BaseVehicleModel)?.Id)),
            "suv" => await _suvContainer.CreateItemAsync(vehicle, new PartitionKey((vehicle as BaseVehicleModel)?.Id)),
            _ => throw new Exception("Invalid car type")
        };
        return response.Resource;
    }

    public async Task<TVehicleModel> UpdateVehicle<TVehicleModel>(string id, TVehicleModel vehicle, string type)
    {
        var response = type switch
        {
            "sedan" => await _sedanContainer.UpsertItemAsync(vehicle, new PartitionKey(id)),
            "pickup" => await _pickupContainer.UpsertItemAsync(vehicle, new PartitionKey(id)),
            "suv" => await _suvContainer.UpsertItemAsync(vehicle, new PartitionKey(id)),
            _ => throw new Exception("Invalid car type")
        };
        return response.Resource;
    }
}