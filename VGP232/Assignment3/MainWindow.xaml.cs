using Microsoft.Win32;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
/*
 * https://stackoverflow.com/questions/38460253/how-to-use-system-windows-forms-in-net-core-class-library
 * How to use System.Windows.Forms in .NET Core class library
 */
using TextureAtlasLib;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Assignment3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Spritesheet mySpritesheet { get; set; }
        public string InputXMLFile { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            mySpritesheet = new Spritesheet();
            InputXMLFile = string.Empty;
            images.ItemsSource = mySpritesheet.InputPaths;
        }

        private void browseBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = " PNG Files | *.png";
            saveFile.DefaultExt = "png";

            if (saveFile.ShowDialog() == true)
            {
                mySpritesheet.OutputDirectory = Path.GetDirectoryName(saveFile.FileName);
                tbOutputDir.Text = mySpritesheet.OutputDirectory;
                mySpritesheet.OutputFile = Path.GetFileName(saveFile.FileName);
                tbOutputFile.Text = mySpritesheet.OutputFile;
            }
        }
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = " PNG Files | *.png";
            openFile.DefaultExt = "png";
            openFile.Multiselect = true;
            if (openFile.ShowDialog() == true)
            {
                foreach (string image in openFile.FileNames)
                {
                    if (Path.GetExtension(image) == ".png")
                    {
                        mySpritesheet.InputPaths.Add(image);
                    }
                }
                images.ItemsSource = mySpritesheet.InputPaths;
                images.Items.Refresh();
            }
        }

        private void removeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (images.SelectedIndex == -1)
            {
                return;
            }
            mySpritesheet.InputPaths.RemoveAt(images.SelectedIndex);
            images.Items.Refresh();
        }

        private void generateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(tbOutputDir.Text))
            {
                MessageBox.Show($"The directory {tbOutputDir.Text} is not existed.");
                tbOutputDir.Text = mySpritesheet.OutputDirectory;
                return;
            }

            if (Path.GetExtension(tbOutputFile.Text) != ".png")
            {
                MessageBox.Show($"The output file {tbOutputFile.Text} is not a png file.");
                tbOutputFile.Text = mySpritesheet.OutputFile;
                return;
            }

            int col;
            if (!int.TryParse(tbColumns.Text, out col) || col < 1)
            {
                MessageBox.Show($"The column number should be an postive integer.");
                tbColumns.Text = mySpritesheet.Columns.ToString();
                return;
            }

            try
            {
                mySpritesheet.Generate(true);
                System.Windows.Forms.DialogResult dialogResult = (System.Windows.Forms.DialogResult)MessageBox.Show("Would you like to view the output?", "Generate Successfully", (MessageBoxButton)System.Windows.Forms.MessageBoxButtons.YesNo);
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    Process.Start("explorer.exe", mySpritesheet.OutputDirectory);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void tbOutputDir_TextChanged(object sender, TextChangedEventArgs e)
        {
            string directory = (sender as TextBox).Text;
            if (Directory.Exists(directory))
            {
                mySpritesheet.OutputDirectory = directory;
            }
        }

        private void tbOutputFile_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filename = (sender as TextBox).Text;
            if (Path.GetExtension(filename) == ".png")
            {
                mySpritesheet.OutputFile = filename;
            }
        }

        private void tbColumns_TextChanged(object sender, TextChangedEventArgs e)
        {
            string column = (sender as TextBox).Text;
            int col;
            if (int.TryParse(column, out col))
            {
                if (col > 0)
                {
                    mySpritesheet.Columns = col;
                }
            }
        }

        private void new_Click(object sender, RoutedEventArgs e)
        {
            ClearPage();
            System.Windows.Forms.DialogResult dialogResult = (System.Windows.Forms.DialogResult)MessageBox.Show("Would you like to save first?", "Save First", (MessageBoxButton)System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = " XML Files | *.xml";

                if (saveFile.ShowDialog() == true)
                {
                    if (saveFile.FileName == "")
                    {
                        MessageBox.Show($"Empty Path");
                        return;
                    }

                    if (Path.GetExtension(saveFile.FileName) != ".xml")
                    {
                        MessageBox.Show($"Only xml file is permitted.");
                        return;
                    }

                    InputXMLFile = saveFile.FileName;
                    nameBlock.Text = Path.GetFileName(InputXMLFile);
                    try
                    {
                        using (FileStream fs = new FileStream(InputXMLFile, FileMode.Create))
                        {
                            XmlSerializer xs = new XmlSerializer(typeof(Spritesheet));
                            xs.Serialize(fs, mySpritesheet);
                        }
                        saveMenu.IsEnabled = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"Unable to save file {InputXMLFile}");
                        ClearPage();
                    }
                }
            }
            else
            {
                saveMenu.IsEnabled = false;
            }
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = " XML files | *.xml";
            if (openFile.ShowDialog() == true)
            {
                InputXMLFile = openFile.FileName;
                try
                {
                    using (FileStream fs = new FileStream(InputXMLFile, FileMode.Open))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(Spritesheet));
                        mySpritesheet = (Spritesheet)xs.Deserialize(fs);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show($"Unable to open file {InputXMLFile}");
                    ClearPage();
                    return;
                }
            }

            nameBlock.Text = Path.GetFileName(InputXMLFile);
            tbOutputDir.Text = mySpritesheet.OutputDirectory;
            tbOutputFile.Text = mySpritesheet.OutputFile;
            tbColumns.Text = mySpritesheet.Columns.ToString();
            metaDataBtn.IsChecked = mySpritesheet.IncludeMetaData;
            images.ItemsSource = mySpritesheet.InputPaths;
            images.Items.Refresh();
            saveMenu.IsEnabled = true;

        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            SaveXML();
        }

        private void saveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = " XML Files | *.xml";

            if (saveFile.ShowDialog() == true)
            {
                if (saveFile.FileName == "")
                {
                    MessageBox.Show($"Empty Path");
                    return;
                }

                if (Path.GetExtension(saveFile.FileName) != ".xml")
                {
                    MessageBox.Show($"Only xml file is permitted.");
                    return;
                }

                InputXMLFile = saveFile.FileName;
                nameBlock.Text = Path.GetFileName(InputXMLFile);
                SaveXML();

            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult dialogResult = (System.Windows.Forms.DialogResult)MessageBox.Show("Would you like to save before exit the program?", "Exit", (MessageBoxButton)System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                if (saveMenu.IsEnabled)
                {
                    SaveXML();
                }
            }
            Application.Current.Shutdown();
        }

        private void metaDataBtn_Click(object sender, RoutedEventArgs e)
        {
            mySpritesheet.IncludeMetaData = !mySpritesheet.IncludeMetaData;
        }

        private void ClearPage()
        {
            InputXMLFile = string.Empty;
            nameBlock.Text = string.Empty;
            tbOutputDir.Text = string.Empty;
            tbOutputFile.Text = string.Empty;
            tbColumns.Text = string.Empty;
            metaDataBtn.IsChecked = false;
            mySpritesheet = new Spritesheet();
            images.ItemsSource = mySpritesheet.InputPaths;
            images.Items.Refresh();
        }

        private void SaveXML()
        {
            if (!Directory.Exists(tbOutputDir.Text))
            {
                MessageBox.Show($"The directory {tbOutputDir.Text} is not existed.");
                tbOutputDir.Text = mySpritesheet.OutputDirectory;
                return;
            }

            if (Path.GetExtension(tbOutputFile.Text) != ".png")
            {
                MessageBox.Show($"The output file {tbOutputFile.Text} is not a png file.");
                tbOutputFile.Text = mySpritesheet.OutputFile;
                return;
            }

            int col;
            if (!int.TryParse(tbColumns.Text, out col) || col < 1)
            {
                MessageBox.Show($"The column number should be an postive integer.");
                tbColumns.Text = mySpritesheet.Columns.ToString();
                return;
            }

            try
            {
                using (FileStream fs = new FileStream(InputXMLFile, FileMode.Create))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Spritesheet));
                    xs.Serialize(fs, mySpritesheet);
                }
                MessageBox.Show("Saved!");
                saveMenu.IsEnabled = true;

            }
            catch (Exception)
            {
                MessageBox.Show($"Unable to save file {InputXMLFile}");
                ClearPage();
            }
        }


    }
}
