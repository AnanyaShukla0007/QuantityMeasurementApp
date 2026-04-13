namespace QMAService.Model.Entities;
public class QuantityMeasurementEntity
{
    public int Id { get; set; }
    public string Operation { get; set; } = string.Empty;
    public double Result { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}