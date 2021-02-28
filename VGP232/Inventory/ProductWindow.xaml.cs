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

namespace FinalProject_ToolName
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
            GlobalVariable.InventoryData.SortingOrder = false;
        }

        private void Sort_Btn(object sender, RoutedEventArgs e)
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

        private void Search_Btn(object sender, RoutedEventArgs e)
        {
            if (searchby.SelectedIndex == 0)
            {
                if (int.TryParse(searchwith.Text, out int id) || id <= 0)
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
                if (double.TryParse(searchwith.Text, out double price) || price <= 0)
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

        private void New_Btn(object sender, RoutedEventArgs e)
        {
            NewProductWindow window = new NewProductWindow();
            window.ShowDialog();
            SearchedData = false;
            productlist.ItemsSource = GlobalVariable.InventoryData.Products;
            productlist.Items.Refresh();
        }

        private void Remove_Btn(object sender, RoutedEventArgs e)
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

        private void Edit_Btn(object sender, RoutedEventArgs e)
        {
            if (productlist.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select a product to edit.", "Fail to edit");
                return;
            }
            if (SearchedData)
            {
                int id = int.Parse(productlist.SelectedItem.ToString().Split(',')[0]);
                EditProductWindow window = new EditProductWindow(GlobalVariable.InventoryData.Products.FindIndex(x => x.ID == id));
                window.ShowDialog();
            }
            else
            {
                EditProductWindow window = new EditProductWindow(productlist.SelectedIndex);
                window.ShowDialog();
            }
            SearchedData = false;
            productlist.ItemsSource = GlobalVariable.InventoryData.Products;
            productlist.Items.Refresh();
        }

        private void Back_Btn(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Ascending_Checked(object sender, RoutedEventArgs e)
        {
            GlobalVariable.InventoryData.SortingOrder = false;
        }

        private void Descending_Checked(object sender, RoutedEventArgs e)
        {
            GlobalVariable.InventoryData.SortingOrder = true;
        }
    }
}
