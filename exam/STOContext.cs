using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using exam.models;

namespace exam
{
    class STOContext: DbContext
    {
        public STOContext():base("DefaultConnection")
        {

        }
        public DbSet<Goods> goods { get; set; }
        public DbSet<GoodsType> goodsType { get; set; }
        public DbSet<Car> car { get; set; }
        public DbSet<CarModel> carModel { get; set; }
        public DbSet<Customers> customers { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<OrderCotnext> ordersContext { get; set; }

    }
}
