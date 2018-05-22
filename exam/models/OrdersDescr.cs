using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.models
{ 
//describe orders list in main window
    public class OrdersDescr
    {
        public OrdersDescr(DateTime date, int id, string customer, string phone, string auto, string number, int summa)
        {
            this.OrderDate = date;
            this.OrderId = id;
            this.OrderCustomer = customer;
            this.OrderCustomerPhone = phone;
            this.OrderAuto = auto;
            this.OrderAutoNumber = number;
            this.OrderSuma = summa;
        }
        public DateTime OrderDate { get; set; }
        public int OrderId { get; set; }
        public string OrderCustomer { get; set; }
        public string OrderCustomerPhone { get; set; }
        public string OrderAuto { get; set; }
        public string OrderAutoNumber { get; set; }
        public int OrderSuma { get; set; }
    }
}
