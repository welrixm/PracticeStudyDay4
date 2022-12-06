using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using PracticeStudy.Components;
using System.IO;

namespace PracticeStudy.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        Components.Product product;
       
        public EditPage(Components.Product _product)
        {
            InitializeComponent();
            product = _product;
            DataContext = product;
           
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(product.Id == 0)
            {
                DBConnect.db.Product.Add(product);

            }
            
            DBConnect.db.SaveChanges();
            MessageBox.Show("Успешно сохранено!");

        }

        private void AddImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpg|*.jpg"
            };
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                product.MainImage = File.ReadAllBytes(openFileDialog.FileName);
                ProductImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}
