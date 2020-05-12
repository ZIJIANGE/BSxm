using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Areas.Daqwrs.Models
{
    public class Daqwr
    {
        public int id { get; set; }
        public string dates { get; set; }
        public string AQI { get; set; }
        public string nums { get; set; }
        public string types { get; set; }
        public string PM25 { get; set; }
        public string PM10 { get; set; }
        public string SO2 { get; set; }
        public string CO { get; set; }
        public string NO2 { get; set; }
        public string O3 { get; set; }

    }
}