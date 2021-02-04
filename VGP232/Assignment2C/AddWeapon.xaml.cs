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

namespace Assignment2C
{
    /// <summary>
    /// Interaction logic for AddWeapon.xaml
    /// </summary>
    public partial class AddWeapon : Window
    {
        private Weapon myWeapon;
        public Weapon MyWeapon 
        {
            get { return myWeapon; }
            set 
            {
                myWeapon = value;
                DataContext = MyWeapon;
            }
        }

        public AddWeapon()
        {
            InitializeComponent();
            string[] elements = Enum.GetNames(typeof(WeaponType));
            type.ItemsSource = elements;
            rarity.ItemsSource = new List<int>() { 1, 2, 3, 4, 5 };
            MyWeapon = new Weapon();
        }

        private void CancelPressed(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void AddPressed(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void GeneratePressed(object sender, RoutedEventArgs e)
        {
            baseattack.Text = new Random().Next(20, 51).ToString();
            rarity.SelectedIndex = new Random().Next(5);
            type.SelectedIndex = new Random().Next(Enum.GetNames(typeof(WeaponType)).Length);

        }
    }
}
