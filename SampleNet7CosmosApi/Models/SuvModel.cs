using Newtonsoft.Json;

namespace SampleNet7CosmosApi.Models;

public class SuvModel: BaseVehicleModel
{
    [JsonProperty("allWheelDrive")]
    public bool AllWheelDrive { get; set; }
    [JsonProperty("fourWheelDrive")]
    public bool FourWheelDrive { get; set; } // This one is for those where they are not full-time
}
