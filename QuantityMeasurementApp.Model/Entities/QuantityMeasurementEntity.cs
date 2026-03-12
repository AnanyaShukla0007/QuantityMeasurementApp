using QuantityMeasurementApp.Model.Enums;

namespace QuantityMeasurementApp.Model.Entities
{
    public class QuantityMeasurementEntity
    {
        public DateTime Timestamp { get; set; }

        public OperationType OperationType { get; set; }

        public string? FormattedResult { get; set; }

        public string? ErrorMessage { get; set; }

        public QuantityMeasurementEntity()
        {
            Timestamp = DateTime.UtcNow;
        }

        public QuantityMeasurementEntity(OperationType operation, string result)
        {
            Timestamp = DateTime.UtcNow;
            OperationType = operation;
            FormattedResult = result;
        }

        public QuantityMeasurementEntity(OperationType operation, string error, bool isError)
        {
            Timestamp = DateTime.UtcNow;
            OperationType = operation;
            ErrorMessage = error;
        }
    }
}