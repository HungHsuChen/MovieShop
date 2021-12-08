﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Trailer
    {
        public int Id { get; set; }
        public string? TrailerUrl { get; set; }
        public string? Name { get; set; }

        // Foreign Key
        public int MovieId { get; set; }

        // Navigation property
        public Movie Movie { get; set; }
    }
}
