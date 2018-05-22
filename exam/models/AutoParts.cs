using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.models
{//not use
    class AutoParts
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(carModel))]
        public int CarModelID { get; set; }
        public CarModel carModel { get; set; }
    }
}
