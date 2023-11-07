using Newtonsoft.Json;

namespace SampleNet7CosmosApi.Models;

public class PickupModel: BaseVehicleModel
{
    [JsonProperty("bedLengthInFeet")] public ushort BedLengthInFeet { get; set; }
    [JsonProperty("fourWheelDrive")] public bool FourWheelDrive { get; set; }
    [JsonProperty("towCapacityPounds")] public ushort TowCapacityPounds { get; set; }
}
