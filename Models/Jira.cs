using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GttApiWeb.Models
{
    public class Jira
    {
        public long id { get; set; }
        public long user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string url { get; set; }
        public string proyect { get; set; }
        public string componente { get; set; }
    }
}
