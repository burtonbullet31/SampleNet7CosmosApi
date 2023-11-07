namespace SampleNet7CosmosApi_Old.Models;

public class PickupModel: BaseVehicleModel
{
    public ushort BedLengthInFeet { get; set; }
    public bool FourWheelDrive { get; set; }
    public ushort TowCapacityPounds { get; set; }
}
