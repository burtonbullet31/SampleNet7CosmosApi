namespace SampleNet7CosmosApi_Old.Models;

public abstract class BaseVehicleModel
{
    public string EngineType { get; set; }
    public string FuelCapacity { get; set; }
    public string FuelType { get; set; }
    public string Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public ushort NumberOfDoors { get; set; }
    public string TransmissionType { get; set; }
    public ushort Year { get; set; }
}
