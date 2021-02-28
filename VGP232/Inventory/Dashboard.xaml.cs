using System;
using System.Collections.Generic;
using System.IO;

using System.Windows;


namespace FinalProject_ToolName
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Product_Btn(object sender, RoutedEventArgs e)
        {
            ProductWindow window = new ProductWindow();
            Hide();
            window.ShowDialog();
            Show();
        }

        private void Customer_Btn(object sender, RoutedEventArgs e)
        {
            CustomerWindow window = new CustomerWindow();
            Hide();
            window.ShowDialog();
            Show();
        }

        private void Category_Btn(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariable.InventoryData.SerializeData();
            Close();
        }
    }
}
