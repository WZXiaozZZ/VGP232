using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;

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
            CategoryWindow window = new CategoryWindow();
            Hide();
            window.ShowDialog();
            Show();
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog
            {
                Filter = "Json Files | *.json"
            };
            if (saveFile.ShowDialog() == true)
            {
                if (GlobalVariable.InventoryData.SaveToJson(saveFile.FileName))
                {
                    MessageBox.Show("Successfully export the inventory data.");
                }
                else
                {
                    MessageBox.Show("Unable to export the inventory data");
                }
            }

        }


        private void Import_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "Json Files | *.json"
            };
            if (openFile.ShowDialog() == true)
            {
                if (GlobalVariable.InventoryData.LoadFromJson(openFile.FileName))
                {
                    MessageBox.Show("Successfully import the inventory data.");
                }
                else
                {
                    MessageBox.Show("Unable to import the inventory data. Loading data from local database.");
                }

            }
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult dialogResult = (System.Windows.Forms.DialogResult)MessageBox.Show("Would you save the inventory data?", "Save & Exit", (MessageBoxButton)System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                GlobalVariable.InventoryData.SerializeData();
            }
            Close();
        }
    }
}
