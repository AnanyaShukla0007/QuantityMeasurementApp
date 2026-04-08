using System;
using System.ComponentModel.DataAnnotations;
using QuantityMeasurementApp.Model.Enums;

namespace QuantityMeasurementApp.Model.Entities
{
    public class QuantityMeasurementEntity
    {
        [Key]
        public int Id { get; set; }

        public OperationType OperationType { get; set; }

        public MeasurementCategory MeasurementCategory { get; set; }

        public double Operand1Value { get; set; }

        public string Operand1Unit { get; set; } = string.Empty;

        public double Operand2Value { get; set; }

        public string Operand2Unit { get; set; } = string.Empty;

        public double ResultValue { get; set; }

        public string ResultUnit { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}