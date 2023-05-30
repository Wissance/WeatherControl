using System;
using System.Collections.Generic;
using System.Text;
using Wissance.WeatherControl.Dto.V2;
using Wissance.WeatherControl.GraphData.Entity;
using Wissance.WeatherControl.WebApi.V2.Helpers;

namespace Wissance.WeatherControl.WebApi.V2.Factories
{
    public static class MeasurementFactory
    {
        public static MeasurementDto Create(MeasurementEntity entity)
        {
            return new MeasurementDto()
            {
                Id = entity.Id,
                SampleDate = entity.SampleDate,
                MeasureUnitId = entity.Unit.Id,
                SensorId = entity.Sensor.Id,
                Value = entity.Value
            };
        }
        
        
        public static IDictionary<string, object?> Create(MeasurementDto dto, bool generateId, string suffix = null)
        {
            IDictionary<string, object?> dict = new Dictionary<string, object?>()
            {
                {ParamsSuffixAppender.Append("SampleDate", suffix), dto.SampleDate},
                {ParamsSuffixAppender.Append("Value", suffix), dto.Value.ToString()},
                {ParamsSuffixAppender.Append("MeasureUnitId", suffix), dto.MeasureUnitId},
                {ParamsSuffixAppender.Append("SensorId", suffix), dto.SensorId}
            };
            
            // TODO(this if for further getting created object)
            dict[ParamsSuffixAppender.Append("id", suffix)] = generateId ? Guid.NewGuid() : dto.Id;

            return dict;
        }
        
    }
}