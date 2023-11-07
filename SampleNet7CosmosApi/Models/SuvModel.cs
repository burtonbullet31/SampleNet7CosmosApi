namespace SampleNet7CosmosApi_Old.Models;

public class SuvModel: BaseVehicleModel
{
    public bool AllWheelDrive { get; set; }
    public bool FourWheelDrive { get; set; } // This one is for those where they are not full-time
}
