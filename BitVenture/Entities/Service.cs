using System;
using System.Collections.Generic;
using System.Text;

namespace BitVenture.Entities
{
    public class Service
    {
        public string BaseURL { get; set; }
        public bool Enabled { get; set; }
        public List<Endpoint> Endpoints { get; set; }
    }
}
