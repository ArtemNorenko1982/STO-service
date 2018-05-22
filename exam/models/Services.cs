using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.models
{ //not use
    class Services
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
