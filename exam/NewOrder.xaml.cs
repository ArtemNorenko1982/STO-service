using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using exam.models;
using System.Collections.Generic;

namespace exam
{
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Window
    {
        //numerator for order table
        int nom = 1;
        //list for describe order table
        List<OrderDescr> result = new List<OrderDescr>();
        //work whith sto context
        STOContext sc = new STOContext();
        public bool isExists { get; set; }
        public NewOrder(bool isExists)
        {
            InitializeComponent();
            this.isExists = isExists; 
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if ((this.comCar!=null)&&(this.listOrders.Items.Count!=0))
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Не указан автомобиль заказчика, или пустой наряд!");
            }
            //this.Close();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            //print current documet
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                pd.PrintVisual(this.listOrders,"Печать");
            }
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            //this.Close();
        }
        private void NewDocumentInitial()
        {
            //new document initial
            this.txtDateTime.Text = DateTime.Now.ToString();
            this.txtDateTime.IsEnabled = false;
            this.txtNumber.IsEnabled = false;
        }

        private void fillingHeader()
        {
            //filling some visual elements
            this.comCar.Items.Clear();
            this.comCustomer.Items.Clear();
            string model;
            //filling car of customer's
            foreach (var item in sc.car)
            {
                STOContext strContext = new STOContext();
                model = strContext.carModel.Where(a => a.ID == item.carmodelId).Single().Name;
                this.comCar.Items.Add(model + " " + item.carNumber);
            }
            //filling fild customers
            foreach (var item in sc.customers)
            {
                this.comCustomer.Items.Add(item.LastName + " " + item.Name);
            }
        }
        private void fillingItems()
        {
        }
        private void New_Loaded(object sender, RoutedEventArgs e)
        {
            if (isExists==false)
            {
                NewDocumentInitial();
                fillingHeader();
                fillingItems();
            }
        }

        private void comCar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            STOContext strContext = new STOContext();
            //auto select customer 
            int CarOwnerID = strContext.car.Where(
                a => a.carModel.Name + " " + a.carNumber == this.comCar.SelectedItem.ToString()
                ).Single().customerId;

            this.comCustomer.SelectedItem = (sc.customers.Where(a => a.ID == CarOwnerID).Single().LastName + " " 
                                            +sc.customers.Where(a => a.ID == CarOwnerID).Single().Name);
            this.comCustomer.IsEnabled = false;
        }

        private void comCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //STOContext strContext = new STOContext();
            //STOContext Context = new STOContext();
            //int CarOwnerID = strContext.customers.Where(
            //    a => a.LastName + " " + a.Name == this.comCustomer.SelectedItem.ToString()
            //    ).Single().ID;
            ////MessageBox.Show(CarOwnerID.ToString());
            //this.comCar.SelectedItem = (Context.car.Where(a => a.customerId == CarOwnerID).Single().carModel.Name + " "
            //                           +Context.car.Where(a => a.customerId == CarOwnerID).Single().carNumber);
            ////this.comCar.IsEnabled = false;
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            //this.comCustomer.IsEnabled = true;
        }

        private void btnXCust_Click(object sender, RoutedEventArgs e)
        {
            //this.comCar.IsEnabled = true;
        }

        private void listOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //insert into document some goods
            addIntoOrder addForm = new addIntoOrder();
            addForm.Owner = this;
            if (addForm.ShowDialog() == true)
            {
                try
                {
                    foreach (var item in addForm.LstGoogs)
                    {
                        result.Add(new OrderDescr(item.ID, nom, item.Name,1,item.Price,1*item.Price));
                        nom++;
                    }
                    this.listOrders.Items.Refresh();
                    this.listOrders.ItemsSource = result;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnDelStr_Click(object sender, RoutedEventArgs e)
        {
            //delete from documets any string
            if (this.listOrders.SelectedIndex != -1)
            {
                result.RemoveAt(this.listOrders.SelectedIndex);
                nom--;
            }

            this.listOrders.Items.Refresh();
            this.listOrders.ItemsSource = result;
        }
    }
}
