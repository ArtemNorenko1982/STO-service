using System;
using System.ComponentModel.DataAnnotations;

namespace exam.models
{
    public class CarModel
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}