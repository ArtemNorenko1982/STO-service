using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.models
{
    public class Orders
    {
        [Key]
        public int ID { get; set; }
        //order date
        public DateTime Date { get; set; }
        //car
        [ForeignKey(nameof(Cars))]
        public int carId { get; set; }
        public Car Cars { get; set; }
        //customer
        //[ForeignKey(nameof(Customer))]
        //public int customerId { get; set; }
        //public Customers Customer { get; set; }
        //context
        //public OrderCotnext orderContext { get; set; }
        
    }
}
