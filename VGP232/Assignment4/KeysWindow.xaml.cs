using Microsoft.Win32;
using System;
using System.Windows;

namespace Assignment4
{
    /// <summary>
    /// Interaction logic for KeysWindow.xaml
    /// </summary>
    public partial class KeysWindow : Window
    {
        private Crypto MyCrypto = new Crypto();
        private CryptoAlgorithm Algo;

        public KeysWindow()
        {
            InitializeComponent();
        }
        public KeysWindow(CryptoAlgorithm algo)
        {
            InitializeComponent();
            Algo = algo;
            MyCrypto.Initialize(algo);
            if (algo == CryptoAlgorithm.RSA)
            {
                import_btn1.Content = "Import Private Key";
                import_btn2.Content = "Import Public Key";
                export_btn1.Content = "Export Private Key";
                export_btn2.Content = "Export Public Key";
            }
            else
            {
                import_btn1.Content = "Import Shared Key";
                import_btn2.Content = "Import IV";
                export_btn1.Content = "Export Shared Key";
                export_btn2.Content = "Export IV";
            }
        }

        private void import_btn1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (Algo == CryptoAlgorithm.RSA)
            {
                openFile.Filter = " XML Files | *.xml";
                openFile.DefaultExt = "xml";
            }

            if (openFile.ShowDialog() == true)
            {
                try
                {
                    MyCrypto.LoadK1(openFile.FileName);
                }
                catch (Exception)
                {
                    if (Algo == CryptoAlgorithm.AES)
                    {
                        MessageBox.Show("Failed to import shared key", "Error");
                    }
                    else
                    {
                        MessageBox.Show("Failed to import private key", "Error");
                    }
                }
            }
        }

        private void import_btn2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (Algo == CryptoAlgorithm.RSA)
            {
                openFile.Filter = " XML Files | *.xml";
                openFile.DefaultExt = "xml";
            }

            if (openFile.ShowDialog() == true)
            {
                try
                {
                    MyCrypto.LoadK2(openFile.FileName);
                }
                catch (Exception)
                {
                    if (Algo == CryptoAlgorithm.AES)
                    {
                        MessageBox.Show("Failed to import IV", "Error");
                    }
                    else
                    {
                        MessageBox.Show("Failed to import public key", "Error");
                    }
                }
            }
        }

        private void export_btn1_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            if (Algo == CryptoAlgorithm.RSA)
            {
                saveFile.Filter = " XML Files | *.xml";
                saveFile.DefaultExt = "xml";
            }

            if (saveFile.ShowDialog() == true)
            {
                try
                {
                    MyCrypto.SaveK1(saveFile.FileName);
                }
                catch (Exception)
                {
                    if (Algo == CryptoAlgorithm.AES)
                    {
                        MessageBox.Show("Failed to export shared key", "Error");
                    }
                    else
                    {
                        MessageBox.Show("Failed to export private key", "Error");
                    }
                }
            }
        }

        private void export_btn2_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            if (Algo == CryptoAlgorithm.RSA)
            {
                saveFile.Filter = " XML Files | *.xml";
                saveFile.DefaultExt = "xml";
            }

            if (saveFile.ShowDialog() == true)
            {
                try
                {
                    MyCrypto.SaveK2(saveFile.FileName);
                }
                catch (Exception)
                {
                    if (Algo == CryptoAlgorithm.AES)
                    {
                        MessageBox.Show("Failed to export IV", "Error");
                    }
                    else
                    {
                        MessageBox.Show("Failed to export public key", "Error");
                    }
                }
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            EncryptionToolWindow window = new EncryptionToolWindow(MyCrypto);
            window.Show();
            Close();
        }
    }
}
