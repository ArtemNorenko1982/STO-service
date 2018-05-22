using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.models
{
    public class Car
    {
        [Key]
        public int ID { get; set; }
        public string carNumber { get; set; }
        public int YearProduce { get; set; }

        [ForeignKey(nameof(carModel))]
        public int carmodelId { get; set; }
        public CarModel carModel { get; set; }

        [ForeignKey(nameof(customer))]
        public int customerId { get; set; }
        public Customers customer { get; set; }
    }
}
