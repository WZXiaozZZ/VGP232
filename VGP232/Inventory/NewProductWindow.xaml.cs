using System.Windows;


namespace FinalProject_ToolName
{
    /// <summary>
    /// Interaction logic for NewProductWindow.xaml
    /// </summary>
    public partial class NewProductWindow : Window
    {
        public NewProductWindow()
        {
            InitializeComponent();
            categorybox.ItemsSource = GlobalVariable.InventoryData.Categories;
            categorybox.SelectedIndex = 0;
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalVariable.InventoryData.AddProduct(out string message, idbox.Text, namebox.Text, pricebox.Text, quantitybox.Text, categorybox.SelectedItem.ToString()))
            {
                MessageBox.Show(message);
                Close();
            }
            else
            {
                MessageBox.Show(message);
            }
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
