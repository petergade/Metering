using System;
using Metering.Api.Models;
using Microsoft.Azure.Cosmos.Table;

namespace Metering.Api.TableStorageEntities;

public class Measurement : TableEntity
{
    public Measurement(MeasurementDto measurementDto) : base("1", measurementDto.Timestamp.ToString("o"))
    {
        Temperature = measurementDto.Temperature;
        Humidity = measurementDto.Humidity;
    }
    
    public decimal Temperature { get; }
    
    public decimal Humidity { get; }
}