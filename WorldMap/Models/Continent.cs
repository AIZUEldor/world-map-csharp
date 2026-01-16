using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WorldMap.Models
{
    public class Continent
    {
        public int ContinentID { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}