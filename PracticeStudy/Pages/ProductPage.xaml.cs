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
        public ObservableCollection<Product> products
        {
            get { return (ObservableCollection<Product>)GetValue(productsProperty); }
            set { SetValue(productsProperty, value); }
        }
        public static readonly DependencyProperty productsProperty = DependencyProperty.Register("products", typeof(ObservableCollection<Product>), typeof(ProductPage));
        public ProductPage()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();

            DBConnect.db.Product.Load();
            products = DBConnect.db.Product.Local;

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += UpdateData;
            timer.Start();
            ListProduct.ItemsSource = DBConnect.db.Product.Where(x => x.IsActive == false || x.IsActive == null).ToList();
            GeneralCount.Text = DBConnect.db.Product.Where(x => x.IsActive != false).Count().ToString();
        }
        public void UpdateData(object sender, object e)
        {
            var HistoryProduct = DBConnect.db.Product.ToList();
            ListProduct.ItemsSource = HistoryProduct;
            ListProduct.ItemsSource = DBConnect.db.Product.Where(x => x.Name.StartsWith(TxtSearch.Text) || x.Description.StartsWith(TxtSearch.Text)).ToList();
        }
        public static List<Product> ItemsSource { get; private set; }
        private void Refresh()
        {
            IEnumerable<Product> filterProduct = DBConnect.db.Product.Where(x => x.IsActive == true);
            if(CbSort.SelectedIndex > 0)
            {
                if (CbSort.SelectedIndex == 1)
                    filterProduct = filterProduct.OrderBy(x => x.Name);

                
                else
                    filterProduct = filterProduct.OrderByDescending(x => x.Name);
                Refresh();

            }
            if(CbCount.SelectedIndex > -1 && filterProduct.Count() > 0)
            {
                int selCount = Convert.ToInt32((CbCount.SelectedItem as ComboBoxItem).Content);
                filterProduct = filterProduct.Skip(selCount * actualPage).Take(selCount);
                if (filterProduct.Count() == 0)
                {
                    actualPage--;
                    Refresh();
                }
            }
            ListProduct.ItemsSource = filterProduct.ToList();
            FoundCount.Text = filterProduct.Count().ToString() + "из";

        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selProduct = (sender as Button).DataContext as Product;
            Navigation.frameMain.Navigate(new EditPage(selProduct));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void CbDown_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    CbSort.ItemsSource = DBConnect.db.Product.OrderByDescending(x => x.Name).ToList();
        //    CbSort.ItemsSource = DBConnect.db.Product.OrderByDescending(x => x.DateOfAddition).ToList();
        //}

        //private void CbUp_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    CbSort.ItemsSource = DBConnect.db.Product.OrderBy(x => x.Name).ToList();
        //    CbSort.ItemsSource = DBConnect.db.Product.OrderBy(x => x.DateOfAddition).ToList();
        //}

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
    }
}
