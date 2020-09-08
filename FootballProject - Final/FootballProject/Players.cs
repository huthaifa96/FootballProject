using System;
using System.Collections.Generic;

namespace FootballProject
{
    public partial class Players
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Nationality { get; set; }
        public int? TeamId { get; set; }
        public int? HeightInches { get; set; }
        public string StrongestFoot { get; set; }
        public string Position { get; set; }

        public virtual Teams Team { get; set; }
    }
}
