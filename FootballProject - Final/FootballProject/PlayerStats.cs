using System;
using System.Collections.Generic;

namespace FootballProject
{
    public partial class PlayerStats
    {
        public int StatId { get; set; }
        public int? PlayerId { get; set; }
        public int? GoalsScored { get; set; }
        public int? GoalsAssisted { get; set; }
        public int? LeagueTrophiesWon { get; set; }
        public int? EuropeanTrophiesWon { get; set; }

        public virtual Players Player { get; set; }
    }
}
