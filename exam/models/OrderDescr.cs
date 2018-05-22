using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.models
{
    //describe googs in order doc
    public class OrderDescr
    {
        public OrderDescr(int goodsID, int number, string goods, int count, int price, int summa)
        {
            this.GoodsId = goodsID;
            this.Number = number;
            this.Goods = goods;
            this.Count = count;
            this.Price = price;
            this.Summa = summa;
        }
        public int GoodsId { get; private set; }
        public int Number { get; set; }
        public string Goods { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public int Summa { get; set; }
    }
}
