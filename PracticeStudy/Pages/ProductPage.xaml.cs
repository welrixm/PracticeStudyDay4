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
using System.Windows.Threading;
using PracticeStudy.Components;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace PracticeStudy.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        int actualPage = 0;
        
        public ProductPage()
        {
            InitializeComponent();
            //DispatcherTimer timer = new DispatcherTimer();

            DBConnect.db.Product.Load();
            Products = DBConnect.db.Product.Local;

            //timer.Interval = TimeSpan.FromSeconds(1);
            //timer.Tick += UpdateData;
            //timer.Start();
            ListProduct.ItemsSource = DBConnect.db.Product.Where(x => x.IsActive == false || x.IsActive == null).ToList();
            GeneralCount.Text = DBConnect.db.Product.Where(x => x.IsActive != false).Count().ToString();
        }
        public void UpdateData(object sender, object e)
        {
            var HistoryProduct = DBConnect.db.Product.ToList();
            ListProduct.ItemsSource = HistoryProduct;
            ListProduct.ItemsSource = DBConnect.db.Product.Where(x => x.Name.StartsWith(TxtSearch.Text) || x.Description.StartsWith(TxtSearch.Text)).ToList();
        }
        public ObservableCollection<Product> Products
        {
            get { return (ObservableCollection<Product>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }
        public static readonly DependencyProperty ProductsProperty = DependencyProperty.Register("Products", typeof(ObservableCollection<Product>), typeof(ProductPage));
        private void Refresh()
        {
            ObservableCollection<Product> products = Products;
            if (CbSort == null)
                return;
            if (CbSort != null)
            {
                switch ((CbSort.SelectedItem as ComboBoxItem).Tag)
                {
                    case "1":
                        products = DBConnect.db.Product.Local;
                        break;
                    case "2":
                        products = new ObservableCollection<Product>(Products.OrderBy(x => x.Name));
                        break;
                    case "3":
                        products = new ObservableCollection<Product>(Products.OrderByDescending(x => x.Name));
                        break;
                    case "4":
                        products = new ObservableCollection<Product>(Products.OrderBy(x => x.DateOfAddition));
                        break;
                    case "5":
                        products = new ObservableCollection<Product>(Products.OrderByDescending(x => x.DateOfAddition));
                        break;

                }

            }
            ListProduct.ItemsSource = products.ToList();


        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selProduct = (sender as Button).DataContext as Product;
            Navigation.NextPage(new Navig("", new EditPage(selProduct)));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void CbCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            actualPage = 0;
            Refresh();
        }

        private void LeftBtn_Click(object sender, RoutedEventArgs e)
        {
            actualPage--;
            if (actualPage < 0)
                actualPage = 0;
            Refresh();
        }

        private void RightBtn_Click(object sender, RoutedEventArgs e)
        {
            actualPage++;
            Refresh();
        }

        private void AddNewProductBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Navig("", new EditPage(new Product())));
        }
    }
}
