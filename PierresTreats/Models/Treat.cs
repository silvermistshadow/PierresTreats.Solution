using System.Collections.Generic;

namespace PierresTreats.Models
{
    public class Treat
    {
        public int TreatId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<FlavorTreat> Flavors { get; }

        public Treat()
        {
            this.Flavors = new HashSet<FlavorTreat>();
        }
    }
}