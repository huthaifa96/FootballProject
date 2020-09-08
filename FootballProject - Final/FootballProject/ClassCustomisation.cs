using System;
using System.Collections.Generic;
using System.Text;

namespace FootballProject
{
    public partial class Teams
    {
        public override string ToString()
        {
            return $"{TeamName}";
        }
    }

    public partial class Players
    {
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
