using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniCardApi.Models
{
    public class Request
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
