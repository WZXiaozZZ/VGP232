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
using Test;

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
            Window1 window = new Window1();
            window.Show();
            Close();
            
        }
    }
}
