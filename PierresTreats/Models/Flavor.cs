using System.Collections.Generic;

namespace PierresTreats.Models
{
    public class Flavor
    {
        public int FlavorId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<FlavorTreat> Treats { get; set; }

        public Flavor()
        {
            this.Treats = new HashSet<FlavorTreat>();
        }
    }
}