﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballBackEndAspNetCoreApiF.Models
{
    public class Refree
    {
        public int RefreeId { get; set; }
        public string Name { get; set; }
        public string Rank { get; set; }
        public string Location { get; set; }
        public int TotalNumberOfMatches { get; set; }
    }
}
