using System.Windows;

namespace FinalProject_ToolName
{
    /// <summary>
    /// Interaction logic for CategoryWindow.xaml
    /// </summary>
    public partial class CategoryWindow : Window
    {
        private bool SearchedData = false;
        public CategoryWindow()
        {
            InitializeComponent();
            categorylist.ItemsSource = GlobalVariable.InventoryData.Categories;
            GlobalVariable.InventoryData.SortingOrder = false;
        }

        private void Sort_Btn(object sender, RoutedEventArgs e)
        {
            GlobalVariable.InventoryData.SortCategories();
            if (SearchedData)
            {
                categorylist.ItemsSource = GlobalVariable.InventoryData.Categories;
                SearchedData = false;
            }
            categorylist.Items.Refresh();
        }

        private void Search_Btn(object sender, RoutedEventArgs e)
        {
            if (searchwith.Text == "")
            {
                SearchedData = false;
                categorylist.ItemsSource = GlobalVariable.InventoryData.Categories;
            }
            else
            {
                SearchedData = true;
                categorylist.ItemsSource = GlobalVariable.InventoryData.Categories.FindAll(x => x.ToLower().StartsWith(searchwith.Text.ToLower()));
            }
            categorylist.Items.Refresh();
        }

        private void New_Btn(object sender, RoutedEventArgs e)
        {
            ModifyCategoryWindow window = new ModifyCategoryWindow();
            window.ShowDialog();
            SearchedData = false;
            categorylist.ItemsSource = GlobalVariable.InventoryData.Categories;
            categorylist.Items.Refresh();
        }

        private void Remove_Btn(object sender, RoutedEventArgs e)
        {
            if (categorylist.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select a category to remove.", "Fail to remove");
                return;
            }
            
            if (categorylist.SelectedItem.ToString() == "None")
            {
                MessageBox.Show("Can not remove None category.", "Forbid to remove");
            }
            if (SearchedData)
            {

                GlobalVariable.InventoryData.RemoveCategory(categorylist.SelectedItem.ToString());
                categorylist.ItemsSource = GlobalVariable.InventoryData.Categories;
                SearchedData = false;
            }
            else
            {
                GlobalVariable.InventoryData.RemoveCategory(categorylist.SelectedIndex);
            }
            MessageBox.Show("Successfully remove a category.");
            categorylist.Items.Refresh();
        }

        private void Edit_Btn(object sender, RoutedEventArgs e)
        {
            if (categorylist.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select a category to edit.", "Fail to edit");
                return;
            }
            if (categorylist.SelectedItem.ToString() == "None")
            {
                MessageBox.Show("Can not edit None category.", "Forbid to edit");
            }

            ModifyCategoryWindow window = new ModifyCategoryWindow(categorylist.SelectedItem.ToString());
            window.ShowDialog();
            SearchedData = false;
            categorylist.ItemsSource = GlobalVariable.InventoryData.Categories;
            categorylist.Items.Refresh();
        }

        private void Back_Btn(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Ascending_Checked(object sender, RoutedEventArgs e)
        {
            GlobalVariable.InventoryData.SortingOrder = false;
        }

        private void Descending_Checked(object sender, RoutedEventArgs e)
        {
            GlobalVariable.InventoryData.SortingOrder = true;
        }
    }
}
