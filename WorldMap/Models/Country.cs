using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldMap.Models
{
    public class Country
    {
        public int CountryID { get; set; }
        public string Name { get; set; }
        public string ISOCode { get; set; }
        public int ContinentID { get; set; }
        public string Currency { get; set; }
        public long? Population { get; set; }
        public double? AreaKm2 { get; set; }
    }
}
