using exam.models;
using System.Windows;
using System.Data.Entity;
using System.Windows.Controls;
using System.Collections;
using System.Collections.Generic;

namespace exam
{
    /// <summary>
    /// Interaction logic for addIntoOrder.xaml
    /// </summary>
    public partial class addIntoOrder : Window
    {
        List<Goods> lstGoods = new List<Goods>();
        public List<Goods> LstGoogs{ get {return lstGoods; } }

        STOContext db;
        public addIntoOrder()
        {
            InitializeComponent();
            db = new STOContext();
            db.goods.Load();
            GoodsGrid.ItemsSource = db.goods.Local.ToBindingList(); 
            this.Closing += Window_Closing;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void GoodsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GoodsGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < GoodsGrid.SelectedItems.Count; i++)
                {
                    Goods currentItem = GoodsGrid.SelectedItems[i] as Goods;
                    if (currentItem != null)
                    {
                        lstGoods.Add(currentItem);
                    }
                }
            }
        }

        private void GoodsGrid_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
