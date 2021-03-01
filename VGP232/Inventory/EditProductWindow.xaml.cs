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
    /// Interaction logic for EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        readonly private int index;
        public EditProductWindow()
        {
            InitializeComponent();
        }

        public EditProductWindow(int i)
        {
            InitializeComponent();
            index = i;
            categorybox.ItemsSource = GlobalVariable.InventoryData.Categories;
            idbox.Text = GlobalVariable.InventoryData.Products[i].ID.ToString();
            namebox.Text = GlobalVariable.InventoryData.Products[i].Name;
            pricebox.Text = GlobalVariable.InventoryData.Products[i].Price.ToString();
            quantitybox.Text = GlobalVariable.InventoryData.Products[i].Quantity.ToString();
            categorybox.SelectedItem = GlobalVariable.InventoryData.Products[i].Category;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalVariable.InventoryData.EditProduct(out string message, index, idbox.Text, namebox.Text, pricebox.Text, quantitybox.Text, categorybox.SelectedItem.ToString()))
            {
                MessageBox.Show(message);
                Close();
            }
            else
            {
                MessageBox.Show(message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
