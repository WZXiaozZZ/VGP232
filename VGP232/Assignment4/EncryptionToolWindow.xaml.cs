using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace Assignment4
{
    /// <summary>
    /// Interaction logic for EncryptionToolWindow.xaml
    /// </summary>
    public partial class EncryptionToolWindow : Window
    {
        private Crypto MyCrypto;

        public EncryptionToolWindow()
        {
            InitializeComponent();
            MyCrypto = new Crypto();
        }

        public EncryptionToolWindow(Crypto crypto)
        {
            InitializeComponent();
            MyCrypto = crypto;
        }


        private void Load_Text_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = " Text Files | *.txt";
            openFile.DefaultExt = "txt";
            if (openFile.ShowDialog() == true)
            { 
                message.Text = File.ReadAllText(openFile.FileName);
                message_result.Text = string.Empty;
            }
        }

        private void Save_Encrypted_Click(object sender, RoutedEventArgs e)
        {
            if (message_result.Text == "")
            {
                MessageBox.Show("Cipher Text can not be empty.", "Error");
                return;
            }
            SaveFileDialog saveFile = new SaveFileDialog();
            if (saveFile.ShowDialog() == true)
            {
                try
                {
                    File.WriteAllBytes(saveFile.FileName, Convert.FromBase64String(message_result.Text));
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to convert result to bytes array.", "Error");
                }
            }
        }

        private void Load_Cipher_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                try
                {
                    cipher.Text = Convert.ToBase64String(File.ReadAllBytes(openFile.FileName));
                    cipher_result.Text = string.Empty;
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to convert bytes array in the file to string.", "Error");
                }
            }
        }

        private void Save_Decrypted_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = " Text Files | *.txt";
            saveFile.DefaultExt = "txt";
            if (saveFile.ShowDialog() == true)
            {
                File.WriteAllText(saveFile.FileName, cipher_result.Text);
            }
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            string plainText = message.Text;
            if (plainText.Length == 0)
            {
                MessageBox.Show("Message can not be empty.", "Error");
                message_result.Text = string.Empty;
                return;
            }
            
            plainText = plainText.Replace(' ', '+');
            int padding = plainText.Length % 4;
            if (padding != 0)
            {
                plainText += new string('+', 4 - padding);
            }
            try
            {
                message_result.Text = Convert.ToBase64String(MyCrypto.Encrypt(Convert.FromBase64String(plainText)));
            }
            catch (Exception)
            {
                MessageBox.Show("Can not encrypt the message.", "Error");
                message_result.Text = string.Empty;
            }
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            if (cipher.Text.Length == 0)
            {
                MessageBox.Show("Cipher Text can not be empty.", "Error");
                cipher_result.Text = string.Empty;
                return;
            }
            try
            {
                cipher_result.Text = Convert.ToBase64String(MyCrypto.Decrypt(Convert.FromBase64String(cipher.Text))).Replace('+', ' ');

            }
            catch (Exception)
            {
                MessageBox.Show("Can not decrypt the cipher text.", "Error");
                cipher_result.Text = string.Empty;
            }
        }

    }
}
