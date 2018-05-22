using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using exam.models;

namespace exam
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        //define sto context
        STOContext sc = new STOContext();
        //selected radio button item
        string elem;
        
        public AddWindow(string customElement)
        {
            //wich radio button is selecting
            elem = customElement;
            InitializeComponent();
        }
        //return item's name from different class which describe db
        public string sName1 { get { return this.txtBox1.Text; } }
        //return item's name from different class which describe db
        public string sName2 { get { return this.txtBox2.Text; } }
        //return item's name from different class which describe db
        public string sName3 { get { return this.txtBox3.Text; } }
        //return item's name from different class which describe db
        public string sName4 { get { return this.txtBox4.Text; } }
        //return item's name from different class which describe db
        public string sName5 { get { return this.txtBox5.Text; } }
        //return item's name from different class which describe db
        public string sName6 { get { return this.txtBox6.Text; } }
        //return item's name from different class which describe db
        public string sName7 { get { return this.txtBox7.Text; } }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //when we was finish to input something
            this.DialogResult = true;
        }
        private void FillingSomething()
        {
            //fill all filds for work with cars
            if (elem == "rbtnCar")
            {
                //filling fild car model
                foreach (var item in sc.carModel)
                {
                    this.txtBox2.Items.Add(item.Name);
                }
                //filling fild customers
                foreach (var item in sc.customers)
                {
                    this.txtBox3.Items.Add(item.LastName + " " + item.Name);
                }
            }
            //fill all filds for work with autoparts
            if (elem == "rbtnGoods")
            {
                //filling fild car models
                foreach (var item in sc.carModel)
                {
                    this.txtBox2.Items.Add(item.Name);
                }
                //filling goods type
                foreach (var item in sc.goodsType)
                {
                    this.txtBox4.Items.Add(item.Name);
                }
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //filling all 
            FillingSomething();
            
            if (elem == "rbtnCustomer")
            {
                this.label1.Content = "Имя";
                this.label2.Content = "Фамилия";
                this.label3.Content = "Номер телефона";
            }
            if (elem == "rbtnCar")
            {
                this.label1.Content = "Номер авто";
                this.label2.Content = "Модель";
                this.label3.Content = "Владелец";
                this.label4.Content = "Дата производства";
            }
            if (elem == "rbtnCarModel")
            {
                this.label1.Content = "Наименование";
            }
            if (elem == "rbtnGoodsType")
            {
                this.label1.Content = "Наименование";
                
            }
            if (elem == "rbtnGoods")
            {
                this.label1.Content = "Наименование";
                this.label2.Content = "Для модели";
                this.label3.Content = "Стоимость";
                this.label4.Content = "Тип товара";
            }

        }

        private void txtBox6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void txtBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void txtBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            //this.txtBox2.Items.Clear();
            //STOContext strContext = new STOContext();
            //int CarOwnerID = strContext.car.Where(
            //    a => a.carModel.Name + " " + a.carNumber==this.txtBox3.SelectedItem.ToString()
            //    ).Single().customerId;
            //this.txtBox2.Items.Add(sc.customers.Where(a => a.ID == CarOwnerID).Single().LastName+" "+sc.customers.Where(a => a.ID == CarOwnerID).Single().Name);
            //this.txtBox2.SelectedItem = (sc.customers.Where(a => a.ID == CarOwnerID).Single().LastName + " " + sc.customers.Where(a => a.ID == CarOwnerID).Single().Name);
            
        }
    }
}
