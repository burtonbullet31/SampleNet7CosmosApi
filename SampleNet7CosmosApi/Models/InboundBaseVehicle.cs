namespace SampleNet7CosmosApi.Models;

public class InboundBaseVehicle
{
    public bool? AllWheelDrive { get; set; }
    public ushort? BedLengthInFeet { get; set; }
    public string EngineType { get; set; }
    public bool? FourWheelDrive { get; set; }
    public ushort FuelCapacity { get; set; }
    public string FuelType { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public ushort NumberOfDoors { get; set; }
    public ushort? TowCapacityPounds { get; set; }
    public string TransmissionType { get; set; }
    public string Type { get; set; }
    public ushort Year { get; set; }
}