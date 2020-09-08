using System;
using System.Collections.Generic;

namespace FootballProject
{
    public partial class Teams
    {
        public Teams()
        {
            Players = new HashSet<Players>();
        }

        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamDescription { get; set; }
        public int? LeagueTrophies { get; set; }
        public int? EuropeanTrophies { get; set; }

        
        public virtual ICollection<Players> Players { get; set; }
    }
}
