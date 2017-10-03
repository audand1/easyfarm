using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyFarm.Models
{
    public class Field
    {
        public long Id { get; set; }
        public string user_id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public float size { get; set; }
        public string last_action { get; set; }
    }
}