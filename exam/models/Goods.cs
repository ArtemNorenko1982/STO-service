using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam.models
{
    public class Goods
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        //car model
        [ForeignKey(nameof(carModel))]
        public int CarModelID { get; set; }
        public CarModel carModel { get; set; }
        
        //type of goods 
        [ForeignKey(nameof(Types))]
        public int GoodsTypeID { get; set; }
        public GoodsType Types { get; set; }
        
        //Price of goods
        public int Price { get; set; }
    }
}