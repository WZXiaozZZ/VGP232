using Microsoft.Win32;
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
using WeaponLib;


// Assignment 2C
// NAME: Zixiao Wang
// STUDENT NUMBER: 2022599
// Marks: 97/100
// Comments: Great job!

namespace Assignment2C
{
    /// <summary>
    /// Interaction logic for WeaponEditor.xaml
    /// </summary>
    public partial class WeaponEditor : Window
    {
        public WeaponCollection MyWeaponCollection { get; set; }
        public WeaponEditor()
        {
            InitializeComponent();
            MyWeaponCollection = new WeaponCollection();
            List<string> types = new List<string>(Enum.GetNames(typeof(WeaponType)));
            types.Add("All");
            typeshow.ItemsSource = types;
            typeshow.SelectedItem = "All";
        }

        private void LoadButtonClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = " CSV Files | *.csv| XML files | *.XML| Json Files | *.json";
            if (openFile.ShowDialog() == true)
            {
                if (!MyWeaponCollection.Load(openFile.FileName))
                {
                    MessageBox.Show("Unable to load the file!");
                }
                else
                {
                    weaponList.ItemsSource = MyWeaponCollection.weapons;
                    weaponList.Items.Refresh();
                }
            }
        }

        private void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = " CSV Files | *.csv| XML files | *.XML| Json Files | *.json";

            if (saveFile.ShowDialog() == true)
            {
                if (!MyWeaponCollection.Save(saveFile.FileName))
                {
                    MessageBox.Show("Unable to save file");
                }
            }
        }

        private void AddButtonClicked(object sender, RoutedEventArgs e)
        {
            AddWeapon addWeapon = new AddWeapon();
            if(addWeapon.ShowDialog() == true)
            {
                MyWeaponCollection.weapons.Add(addWeapon.MyWeapon);
                if(weaponList.ItemsSource == null)
                {
                    weaponList.ItemsSource = MyWeaponCollection.weapons;
                }
                weaponList.Items.Refresh();
            }

        }
        private void EditButtonClicked(object sender, RoutedEventArgs e)
        {
            if(weaponList.SelectedIndex == -1)
            {
                return;
            }
            EditWeapon editWeapon = new EditWeapon();
            editWeapon.MyWeapon = weaponList.SelectedItem as Weapon;
            if (editWeapon.ShowDialog() == true)
            {
                MyWeaponCollection.weapons[weaponList.SelectedIndex] = editWeapon.MyWeapon;
                weaponList.Items.Refresh();
            }
        }
        private void RemoveButtonClicked(object sender, RoutedEventArgs e)
        {
            if (weaponList.SelectedIndex == -1)
            {
                return;
            }
            MyWeaponCollection.weapons.RemoveAt(weaponList.SelectedIndex);
            weaponList.Items.Refresh();
        }

        //TODO_ERROR: -3
        //TODO_COMMENT: In this part, you are just treating if it's different than all.
        //if you keep pressing this filter and go to all, your list will become empty.
        //Follow a suggestion for a fix:

        //private void typeshow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    string type = (sender as ComboBox).SelectedItem as string;

        //    if (Enum.TryParse<WeaponType>(type, out WeaponType weaponType))
        //    {
        //        if (weaponType == WeaponType.None)
        //        {
        //            weaponList.ItemsSource = MyWeaponCollection.weapons;
        //            weaponList.Items.Refresh();
        //        }
        //        else
        //        {
        //            weaponList.ItemsSource = MyWeaponCollection.GetAllWeaponsOfType(weaponType);
        //            weaponList.Items.Refresh();
        //        }
        //    }
        //    else
        //    {
        //        weaponList.ItemsSource = MyWeaponCollection.weapons;
        //        weaponList.Items.Refresh();
        //    }
        //}


        private void typeshow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string type = (sender as ComboBox).SelectedItem as string;
            if (type != "All")
            {
                MyWeaponCollection.weapons = MyWeaponCollection.GetAllWeaponsOfType((WeaponType)Enum.Parse(typeof(WeaponType), type));
                weaponList.ItemsSource = MyWeaponCollection.weapons;
                weaponList.Items.Refresh();
            }
        }

        private void filterByName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string name = (sender as TextBox).Text;
            MyWeaponCollection.weapons =  MyWeaponCollection.weapons.FindAll(x => x.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase));
            weaponList.ItemsSource = MyWeaponCollection.weapons;
            weaponList.Items.Refresh();
        }
        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            string sortBy = (sender as RadioButton).Name;
            MyWeaponCollection.SortBy(sortBy);
            weaponList.Items.Refresh();
        }
    }
}
