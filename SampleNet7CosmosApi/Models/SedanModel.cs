using Newtonsoft.Json;

namespace SampleNet7CosmosApi.Models;

public class SedanModel: BaseVehicleModel
{
    [JsonProperty("allWheelDrive")] public bool AllWheelDrive { get; set; }
}
