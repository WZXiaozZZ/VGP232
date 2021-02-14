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

namespace Assignment4
{
    /// <summary>
    /// Interaction logic for SelectionWindow.xaml
    /// </summary>
    public partial class SelectionWindow : Window
    {
        public SelectionWindow()
        {
            InitializeComponent();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            CryptoAlgorithm algo = CryptoAlgorithm.AES;
            if (RSA.IsChecked == true)
            {
                algo = CryptoAlgorithm.RSA;
            }
            KeysWindow window = new KeysWindow(algo);
            window.Show();
            Close();
        }
    }
}
