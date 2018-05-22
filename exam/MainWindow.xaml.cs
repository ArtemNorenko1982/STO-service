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
using System.Windows.Navigation;
using System.Windows.Shapes;
using exam.models;

namespace exam
{
    /*программа для работы СТО
    Не до конца реализован механизм добавления элементов в базу данных. 
    Не реализован механизм выборки в контролы приложения при его запуске и в процессе самой работы
    Описана модель базы данных

        Для полной реализации полноценного приложения не хватило 2-3 дня.

        Если есть возможность перездать экзамен - то по возвращении из коммандировки - приложение будет дописано и
        повторно выложено на майстат

        
        */
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    //enum rbEnum {rbtnOrder, rbtnCustomer, rbtnCar, rbtnCarModel, rbtnService, rbtnAutoPart};
    public partial class MainWindow : Window
    {
        //create list variable for working with OrderList
        List<OrdersDescr> OrderItems = new List<OrdersDescr>();
        //declare variable for work with radio button
        string rbValue;
        //create instance STO context for work with db
        STOContext sc = new STOContext();
        //create instance STO context for work with db
        STOContext sto = new STOContext();
        public MainWindow()
        {
            InitializeComponent();
        }
        //before window loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //filling some filds in main window
            //filling customers
            comboFillCustomers();
            //filling curs
            comboFillCars();
            //filling order list
            listViewFillOrders();
        }

        private void comboFillCars()
        {
            //function what selected and filling combo box car
            string model;
            STOContext strContext = new STOContext();
            foreach (var item in sc.car)
            {
                //find model name for displayed in combo box model name + car number
                model = strContext.carModel.Where(a => a.ID == item.carmodelId).Single().Name;
                //added finding result into combo box
                this.selCar.Items.Add(model+" "+ item.carNumber);
            }
        }

        private void comboFillCustomers()
        {
            //function what selected and filling combo box customers
            foreach (var item in sc.customers)
            {
                //added finding result into combo box LastName + Name
                this.selCustomers.Items.Add(item.LastName + " " + item.Name);
            }
        }
        private void listViewFillOrders()
        {
            //function what selected and filling list box ListOrders
            foreach (var item in sc.orders)
            {
                //selected customer id from car directory
                int iCustID = sto.car.Where(n => n.ID == item.carId).Single().customerId;
                //selected cur number from car directory
                string strCarNumber = sto.car.Where(n => n.ID == item.carId).Single().carNumber;
                //selected cur model id from car directory
                int iCarModel = sto.car.Where(n => n.ID == item.carId).Single().carmodelId;
                //finding car model name from car model directory
                string strCarModel = sto.carModel.Where(m => m.ID == iCarModel).Single().Name;
                //finding customer lastname + name from customers directory 
                string strCustName = sto.customers.Where(n => n.ID == iCustID).Single().LastName +
                                     " "+sto.customers.Where(n => n.ID == iCustID).Single().Name;
                //select customer phone from customer directory
                string strPhone = sto.customers.Where(n => n.ID == iCustID).Single().Phone;
                //add all of them into List what describe ListOrders table
                OrderItems.Add(new OrdersDescr(item.Date,item.ID,strCustName,strPhone,strCarModel,strCarNumber,0));
            }
            //add described list into ListOrders source
            this.listOrders.ItemsSource = OrderItems;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void selCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //set filter in form listOrders for view orders only current customers
        }

        private void selCar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //set filter in form listOrders for view orders only current car
        }
        //method determines what user add want, this method call new form with some parameters, which describe UI in AddForm
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //work with STO context
            if (rbValue == "rbtnOrder")
            {
                //create new window for added new Order
                NewOrder insOrder = new NewOrder(false);
                insOrder.Owner = this;

                STOContext strContext = new STOContext();
                #region order
                if (insOrder.ShowDialog() == true)
                {
                    //find car id from add order dialog
                    int CarID = strContext.car.Where(
                            a => a.carModel.Name + " " + a.carNumber == insOrder.comCar.SelectedItem.ToString()
                        ).Single().ID;

                    //add new order into db
                    Orders ord = new Orders()
                    {
                        carId = CarID, Date= DateTime.Parse(insOrder.txtDateTime.Text)
                    };
                    sc.orders.Add(ord);
                    sc.SaveChanges();
                    //add order context into db table 
                    foreach (OrderDescr item in insOrder.listOrders.ItemsSource)
                    {
                        OrderCotnext orderTable = new OrderCotnext()
                        {
                            OrderId = ord.ID,
                            goodsID = item.GoodsId,
                            Count = item.Count,
                            Price = item.Price,
                            Suma = item.Summa
                        };
                        sc.ordersContext.Add(orderTable);
                        sc.SaveChanges();
                    }
                    //add last added order into listOrders - this described above
                    int iCustID = sto.car.Where(n => n.ID == ord.carId).Single().customerId;
                    string strCarNumber = sto.car.Where(n => n.ID == ord.carId).Single().carNumber;
                    int iCarModel = sto.car.Where(n => n.ID == ord.carId).Single().carmodelId;
                    string strCarModel = sto.carModel.Where(m => m.ID == iCarModel).Single().Name;
                    string strCustName = sto.customers.Where(n => n.ID == iCustID).Single().LastName +
                                         " " + sto.customers.Where(n => n.ID == iCustID).Single().Name;
                    string strPhone = sto.customers.Where(n => n.ID == iCustID).Single().Phone;

                    OrderItems.Add(new OrdersDescr(ord.Date, ord.ID, strCustName, strPhone, strCarModel, strCarNumber, 0));

                    this.listOrders.Items.Refresh();
                    this.listOrders.ItemsSource = OrderItems;
                }
                #endregion
            }
            else
            {
                //add new customer
                AddWindow addWnd = new AddWindow(rbValue);
                if (addWnd.ShowDialog() == true)
                {
                    #region customer
                    if (rbValue == "rbtnCustomer")
                    {
                        Customers cust = new Customers() { Name = addWnd.txtBox1.Text, LastName = addWnd.txtBox2.Text, Phone = addWnd.txtBox3.Text };
                        sc.customers.Add(cust);
                        sc.SaveChanges();
                        this.selCustomers.Items.Add(cust.LastName + " " + cust.Name);
                    }
                    #endregion
                    //add new car
                    #region car
                    if (rbValue == "rbtnCar")
                    {
                        Car car = new Car();
                        car.carNumber = addWnd.txtBox1.Text;
                        car.carmodelId = sc.carModel.Where(a => a.Name == addWnd.txtBox2.Text).Single().ID;
                        car.customerId = sc.customers.Where(a => a.LastName + " " + a.Name == addWnd.txtBox3.Text).Single().ID;
                        car.YearProduce = Int32.Parse(addWnd.txtBox4.Text);
                        sc.car.Add(car);
                        sc.SaveChanges();
                    }
                    #endregion
                    //add new car model
                    #region carmodel
                    if (rbValue == "rbtnCarModel")
                    {
                        CarModel carM = new CarModel() { Name = addWnd.txtBox1.Text };
                        sc.carModel.Add(carM);
                        sc.SaveChanges();
                        this.selCar.Items.Add(carM.Name);
                    }
                    #endregion
                    //add new godds type
                    #region GoddsType
                    if (rbValue == "rbtnGoodsType")
                    {
                        GoodsType gt = new GoodsType() { Name = addWnd.txtBox1.Text };
                        sc.goodsType.Add(gt);
                        sc.SaveChanges();
                    }
                    #endregion
                    //add new goods
                    #region goods
                    if (rbValue == "rbtnGoods")
                    {
                        Goods goods = new Goods()
                        {
                            Name = addWnd.txtBox1.Text,
                            GoodsTypeID = sc.goodsType.Where(a => a.Name == addWnd.txtBox4.Text).Single().ID,
                            Price = Int32.Parse(addWnd.txtBox3.Text),
                            CarModelID = sc.carModel.Where(a => a.Name == addWnd.txtBox2.Text).Single().ID
                        };

                        sc.goods.Add(goods);
                        sc.SaveChanges();
                    }
                    #endregion
                }
            }
        }
        //detected wich radioButton is checked
        private void rbChecked(object sender, RoutedEventArgs e)
        {
            //define wich radio button was selected
            RadioButton pressed = (RadioButton)sender;
            rbValue = pressed.Name;
        }

        private void listOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<OrderDescr> lst = new List<OrderDescr>();
            //opened current documet
            if (listOrders.SelectedItems.Count > 0)
            {
                for (int i = 0; i < listOrders.SelectedItems.Count; i++)
                {
                    OrdersDescr currentItem = listOrders.SelectedItems[i] as OrdersDescr;
                    if (currentItem != null)
                    {
                        NewOrder openWindow = new NewOrder(true);
                        openWindow.txtNumber.Text = currentItem.OrderId.ToString();
                        openWindow.txtDateTime.Text = currentItem.OrderDate.ToString();
                        string model = currentItem.OrderAuto + " " + currentItem.OrderAutoNumber;
                        openWindow.comCar.Items.Add(model);
                        openWindow.comCar.SelectedItem = model;
                        //auto select customer 
                        int CarOwnerID = sto.car.Where(a => a.carModel.Name + " " + a.carNumber == model.ToString()).Single().customerId;
                        string strCust = (sc.customers.Where(a => a.ID == CarOwnerID).Single().LastName + " "
                                        + sc.customers.Where(a => a.ID == CarOwnerID).Single().Name);
                        openWindow.comCustomer.Items.Add(strCust);
                        openWindow.comCustomer.SelectedItem = strCust;
                        openWindow.comCustomer.IsEnabled = false;
                        openWindow.txtDateTime.IsEnabled = false;
                        openWindow.txtNumber.IsEnabled = false;
                        openWindow.btnDelStr.IsEnabled = false;
                        openWindow.btnInsert.IsEnabled = false;
                        openWindow.btnAdd.IsEnabled = false;

                        //sto.ordersContext.Where(i => i.OrderId == currentItem.OrderId).SelectMany( (lst);
                        if (openWindow.ShowDialog() == true)
                        {
                        }

                    }
                }
            }
        }
    }
}
