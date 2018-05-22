using System.ComponentModel.DataAnnotations;

namespace exam.models
{
    public class GoodsType
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

    }
}