using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam.models
{
    public class OrderCotnext
    {
        [Key]
        public int ID { get; set; }
        
        //order
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public Orders Order { get; set; }

        //services
        [ForeignKey(nameof(GoodsID))]
        public int goodsID { get; set; }
        public Goods GoodsID { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public int Suma { get; set; }
    }
}