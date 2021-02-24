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

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        
        public Window1()
        {
            InitializeComponent();
            rarity.ItemsSource = hi.TestClass.theSet;
            list.ItemsSource = hi.TestClass.theSet;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (list.SelectedIndex == -1)
            {
                return;
            }
            hi.TestClass.theSet.Remove((string)list.SelectedItem);
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }
    }
}
