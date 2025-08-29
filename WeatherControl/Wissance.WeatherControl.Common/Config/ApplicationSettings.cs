using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wissance.WeatherControl.Common.Config
{
    public class ApplicationSettings
    {
        public ApplicationSettings()
        {
        }

        public DatabaseSettings Database { get; set; }
    }
}
