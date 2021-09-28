using System;
using System.Collections.Generic;
using System.Text;

namespace BitVenture.Entities
{
    public class Endpoint
    {
        public bool Enabled { get; set; }
        public string Resource { get; set; }
        public List<Response> response { get; set; }
    }
}
