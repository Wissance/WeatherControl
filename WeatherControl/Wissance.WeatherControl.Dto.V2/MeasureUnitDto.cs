using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wissance.WeatherControl.Dto.V2
{
    public class MeasureUnitDto
    {
        //todo(UMV): add constructor with parameters
        public MeasureUnitDto()
        {
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
