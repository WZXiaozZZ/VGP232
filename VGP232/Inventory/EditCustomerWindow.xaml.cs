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

namespace FinalProject_ToolName
{
    /// <summary>
    /// Interaction logic for EditCustomerWindow.xaml
    /// </summary>
    public partial class EditCustomerWindow : Window
    {
        private int index;
        public EditCustomerWindow()
        {
            InitializeComponent();
        }

        public EditCustomerWindow(int i)
        {
            InitializeComponent();
            index = i;
            idbox.Text = GlobalVariable.InventoryData.Customers[i].ID.ToString();
            firstnamebox.Text = GlobalVariable.InventoryData.Customers[i].FirstName;
            lastnamebox.Text = GlobalVariable.InventoryData.Customers[i].LastName;
            phonebox.Text = GlobalVariable.InventoryData.Customers[i].PhoneNumber;
            emailbox.Text = GlobalVariable.InventoryData.Customers[i].Email;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalVariable.InventoryData.EditCustomer(out string message, index, idbox.Text, firstnamebox.Text, lastnamebox.Text, phonebox.Text, emailbox.Text))
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
