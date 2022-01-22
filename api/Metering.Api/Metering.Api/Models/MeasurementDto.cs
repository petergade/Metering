using System;

namespace Metering.Api.Models
{
    public class MeasurementDto
    {
        public DateTimeOffset Timestamp { get; set; }
        
        public decimal Temperature { get; set; }
        
        public decimal Humidity { get; set; }
    }
}