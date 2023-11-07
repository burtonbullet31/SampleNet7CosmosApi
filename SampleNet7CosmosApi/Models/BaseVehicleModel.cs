using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SampleNet7CosmosApi.Models;

public class BaseVehicleModel
{
    [JsonProperty("engineType")]
    public string EngineType { get; set; }
    [JsonProperty("fuelCapacity")]
    public ushort FuelCapacity { get; set; }
    [JsonProperty("fuelType")]
    public string FuelType { get; set; }
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("make")]
    public string Make { get; set; }
    [JsonProperty("model")]
    public string Model { get; set; }
    [JsonProperty("numberOfDoors")]
    public ushort NumberOfDoors { get; set; }
    [JsonProperty("transmissionType")]
    public string TransmissionType { get; set; }
    [JsonProperty("year")]
    public ushort Year { get; set; }
}
