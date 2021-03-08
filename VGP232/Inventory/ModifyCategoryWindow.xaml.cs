using System.Windows;


namespace FinalProject_ToolName
{
    /// <summary>
    /// Interaction logic for ModifyCategoryWindow.xaml
    /// </summary>
    public partial class ModifyCategoryWindow : Window
    {
        private readonly string value = "";
        public ModifyCategoryWindow()
        {
            InitializeComponent();
        }

        public ModifyCategoryWindow(string category)
        {
            InitializeComponent();
            value = category;
            textblock.Text = "Edit the Category";
            btn.Content = "Save";
            namebox.Text = category;
        }


        private void Modify_Btn(object sender, RoutedEventArgs e)
        {
            // add a new category
            if (value == "")
            {
                if (GlobalVariable.InventoryData.AddCategory(out string message, namebox.Text))
                {
                    MessageBox.Show(message);
                    Close();
                }
                else
                {
                    MessageBox.Show(message);
                }
            }
            // edit the category
            else
            {
                if (GlobalVariable.InventoryData.EditCategory(out string message, GlobalVariable.InventoryData.Categories.FindIndex(x=>x==value), namebox.Text))
                {
                    MessageBox.Show(message);
                    Close();
                }
                else
                {
                    MessageBox.Show(message);
                }
            }
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
