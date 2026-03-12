using System;
using QuantityMeasurementApp.Model.Enums;

namespace QuantityMeasurementApp.Model.Entities
{
    public class QuantityMeasurementEntity
    {
        public OperationType OperationType { get; set; }
        public MeasurementCategory MeasurementCategory { get; set; }

        public double Operand1Value { get; set; }
        public string Operand1Unit { get; set; }

        public double Operand2Value { get; set; }
        public string Operand2Unit { get; set; }

        public double ResultValue { get; set; }
        public string ResultUnit { get; set; }

        public string ErrorMessage { get; set; }

        public DateTime Timestamp { get; set; }

        // Empty constructor (needed for DB read)
        public QuantityMeasurementEntity()
        {
            Timestamp = DateTime.Now;
        }

        // 2-argument constructor (required by Service layer)
        public QuantityMeasurementEntity(OperationType operationType, double resultValue)
        {
            OperationType = operationType;
            ResultValue = resultValue;
            Timestamp = DateTime.Now;
        }

        // 2-argument error constructor
        public QuantityMeasurementEntity(OperationType operationType, string errorMessage)
        {
            OperationType = operationType;
            ErrorMessage = errorMessage;
            Timestamp = DateTime.Now;
        }
    }
}