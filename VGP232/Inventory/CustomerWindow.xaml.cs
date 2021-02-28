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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private bool SearchedData = false;
        public CustomerWindow()
        {
            InitializeComponent();
            sortby.ItemsSource = new List<string>() { "ID", "Firstname", "Lastname"};
            sortby.SelectedIndex = 0;
            searchby.ItemsSource = new List<string>() { "ID", "Firstname", "Lastname", "Phone number", "Email", "All" };
            searchby.SelectedIndex = 0;
            customerlist.ItemsSource = GlobalVariable.InventoryData.Customers;
            GlobalVariable.InventoryData.SortingOrder = false;
        }

        private void Sort_Btn(object sender, RoutedEventArgs e)
        {
            if (sortby.SelectedIndex == 0)
            {
                GlobalVariable.InventoryData.SortCustomers(Customer.CompareByID);
            }
            else if (sortby.SelectedIndex == 1)
            {
                GlobalVariable.InventoryData.SortCustomers(Customer.CompareByFirstName);
            }
            else
            {
                GlobalVariable.InventoryData.SortCustomers(Customer.CompareByLastName);
            }
           
            if (SearchedData)
            {
                customerlist.ItemsSource = GlobalVariable.InventoryData.Customers;
                SearchedData = false;
            }
            customerlist.Items.Refresh();
        }

        private void Search_Btn(object sender, RoutedEventArgs e)
        {
            if (searchby.SelectedIndex == 0)
            {
                if (int.TryParse(searchwith.Text, out int id) || id <= 0)
                {
                    customerlist.ItemsSource = GlobalVariable.InventoryData.Customers.FindAll(x => x.ID == id);
                }
                else
                {
                    MessageBox.Show("ID should be an positive integer.", "Fail to search by ID");
                    searchwith.Text = "";
                    SearchedData = false;
                    customerlist.ItemsSource = GlobalVariable.InventoryData.Customers;
                    customerlist.Items.Refresh();
                    return;
                }
            }
            else if (searchby.SelectedIndex == 1)
            {
                customerlist.ItemsSource = GlobalVariable.InventoryData.Customers.FindAll(x => x.FirstName.ToLower().StartsWith(searchwith.Text.ToLower()));
            }
            else if (searchby.SelectedIndex == 2)
            {
                customerlist.ItemsSource = GlobalVariable.InventoryData.Customers.FindAll(x => x.LastName.ToLower().StartsWith(searchwith.Text.ToLower()));
            }
            else if (searchby.SelectedIndex == 3)
            {
                customerlist.ItemsSource = GlobalVariable.InventoryData.Customers.FindAll(x => x.PhoneNumber.StartsWith(searchwith.Text));
            }
            else
            {
                customerlist.ItemsSource = GlobalVariable.InventoryData.Customers.FindAll(x => x.Email.ToLower().StartsWith(searchwith.Text.ToLower()));
            }

            SearchedData = true;
            customerlist.Items.Refresh();
        }

        private void New_Btn(object sender, RoutedEventArgs e)
        {
            NewCustomerWindow window = new NewCustomerWindow();
            window.ShowDialog();
            SearchedData = false;
            customerlist.ItemsSource = GlobalVariable.InventoryData.Customers;
            customerlist.Items.Refresh();
        }

        private void Remove_Btn(object sender, RoutedEventArgs e)
        {
            if (customerlist.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select a customer to remove.", "Fail to remove");
                return;
            }
            if (SearchedData)
            {
                int id = int.Parse(customerlist.SelectedItem.ToString().Split(',')[0]);
                GlobalVariable.InventoryData.RemoveCustomer(GlobalVariable.InventoryData.Customers.FindIndex(x => x.ID == id));
                customerlist.ItemsSource = GlobalVariable.InventoryData.Customers;
                SearchedData = false;
            }
            else
            {
                GlobalVariable.InventoryData.RemoveCustomer(customerlist.SelectedIndex);
            }
            MessageBox.Show("Successfully remove a customer.");
            customerlist.Items.Refresh();
        }

        private void Edit_Btn(object sender, RoutedEventArgs e)
        {
            if (customerlist.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select a customer to edit.", "Fail to edit");
                return;
            }
            if (SearchedData)
            {
                int id = int.Parse(customerlist.SelectedItem.ToString().Split(',')[0]);
                EditCustomerWindow window = new EditCustomerWindow(GlobalVariable.InventoryData.Customers.FindIndex(x => x.ID == id));
                window.ShowDialog();
            }
            else
            {
                EditCustomerWindow window = new EditCustomerWindow(customerlist.SelectedIndex);
                window.ShowDialog();
            }
            
            SearchedData = false;
            customerlist.ItemsSource = GlobalVariable.InventoryData.Customers;
            customerlist.Items.Refresh();
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
