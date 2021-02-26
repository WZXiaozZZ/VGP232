using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InventoryDLL;

namespace Inventory
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private bool SearchedData = false;
        public ProductWindow()
        {
            InitializeComponent();
            sortby.ItemsSource = new List<string>() { "ID", "Name", "Price", "Category" };
            sortby.SelectedIndex = 0;
            searchby.ItemsSource = new List<string>() { "ID", "Name", "Price", "Category", "All" };
            searchby.SelectedIndex = 0;
            productlist.ItemsSource = GlobalVariable.InventoryData.Products;
        }

        private void sort_btn(object sender, RoutedEventArgs e)
        {
            if (sortby.SelectedIndex == 0)
            {
                GlobalVariable.InventoryData.SortProducts(Product.CompareByID);
            }
            else if (sortby.SelectedIndex == 1)
            {
                GlobalVariable.InventoryData.SortProducts(Product.CompareByName);
            }
            else if (sortby.SelectedIndex == 2)
            {
                GlobalVariable.InventoryData.SortProducts(Product.CompareByPrice);
            }
            else
            {
                GlobalVariable.InventoryData.SortProducts(Product.CompareByCategory);
            }
            if (SearchedData)
            {
                productlist.ItemsSource = GlobalVariable.InventoryData.Products;
                SearchedData = false;
            }
            productlist.Items.Refresh();
        }

        private void search_btn(object sender, RoutedEventArgs e)
        {
            if (searchby.SelectedIndex == 0)
            {
                int id;
                if (int.TryParse(searchwith.Text, out id) || id <= 0)
                {
                    productlist.ItemsSource = GlobalVariable.InventoryData.Products.FindAll(x => x.ID == id);
                }
                else
                {
                    MessageBox.Show("ID should be an positive integer.", "Fail to search by ID");
                    searchwith.Text = "";
                    SearchedData = false;
                    productlist.ItemsSource = GlobalVariable.InventoryData.Products;
                    productlist.Items.Refresh();
                    return;
                }
            }
            else if (searchby.SelectedIndex == 1)
            {
                productlist.ItemsSource = GlobalVariable.InventoryData.Products.FindAll(x => x.Name.ToLower().StartsWith(searchwith.Text.ToLower()));
            }
            else if (searchby.SelectedIndex == 2)
            {
                double price;
                if (double.TryParse(searchwith.Text, out price) || price <= 0)
                {
                    productlist.ItemsSource = GlobalVariable.InventoryData.Products.FindAll(x => x.Price <= price);
                }
                else
                {
                    MessageBox.Show("Price should be a positive number.", "Fail to search by Price");
                    searchwith.Text = "";
                    SearchedData = false;
                    productlist.ItemsSource = GlobalVariable.InventoryData.Products;
                    productlist.Items.Refresh();
                    return;
                }
            }
            else if (searchby.SelectedIndex == 3)
            {
                productlist.ItemsSource = GlobalVariable.InventoryData.Products.FindAll(x => x.Category.ToLower().StartsWith(searchwith.Text.ToLower()));
            }
            else
            {
                searchwith.Text = "";
                SearchedData = false;
                productlist.ItemsSource = GlobalVariable.InventoryData.Products;
                productlist.Items.Refresh();
                return;
            }

            SearchedData = true;
            productlist.Items.Refresh();
        }

        private void new_btn(object sender, RoutedEventArgs e)
        {
            
        }

        private void remove_btn(object sender, RoutedEventArgs e)
        {
            if (productlist.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select a product to remove.", "Fail to remove");
                return;
            }
            if (SearchedData)
            {
                int id = int.Parse(productlist.SelectedItem.ToString().Split(',')[0]);
                GlobalVariable.InventoryData.RemoveProduct(GlobalVariable.InventoryData.Products.FindIndex(x => x.ID == id));
                productlist.ItemsSource = GlobalVariable.InventoryData.Products;
                SearchedData = false;
            }
            else
            {
                GlobalVariable.InventoryData.RemoveProduct(productlist.SelectedIndex);
            }
            MessageBox.Show("Successfully remove a product.");
            productlist.Items.Refresh();
        }

        private void edit_btn(object sender, RoutedEventArgs e)
        {

        }

        private void Ascending_Checked(object sender, RoutedEventArgs e)
        {
            GlobalVariable.InventoryData.SortingOrderProduct = 1;
        }

        private void Descending_Checked(object sender, RoutedEventArgs e)
        {
            GlobalVariable.InventoryData.SortingOrderProduct = -1;
        }
    }
}
