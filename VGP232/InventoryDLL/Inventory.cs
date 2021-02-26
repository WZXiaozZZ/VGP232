using System;
using System.Collections.Generic;
using System.IO;

namespace InventoryDLL
{
    public class Inventory
    {
        readonly private string DataFilesPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "database");
        readonly private string[] SerializedTextFiles = { "products.txt", "customers.txt", "categories.txt" };
        
        // generate data file path
        private string GetPath(int i)
        {
            if (i < 0 || i > 2)
            {
                return "";
            }
            return Path.Combine(DataFilesPath, SerializedTextFiles[i]);
        }

        public List<Product> Products { get; set; }
        public List<Customer> Customers { get; set; }
        public List<string> Categories { get; set; }

        public int SortingOrderProduct { get; set; } // 1 for Ascending order, -1 for Descending order
        public int SortingOrderCustomer { get; set; } // 1 for Ascending order, -1 for Descending order
        public bool SortingOrderCategory { get; set; } // False for Ascending order, True for Descending order
        public bool ProductChanged { get; set; }
        public bool CustomerChanged { get; set; }
        public bool CategoryChanged { get; set; }

        public Inventory()
        {
            Products = new List<Product>();
            Customers = new List<Customer>();
            Categories = new List<string>();

            SortingOrderProduct = 1;
            SortingOrderCustomer = 1;
            SortingOrderCategory = false;
            ProductChanged = false;
            CustomerChanged = false;
            CategoryChanged = false;

            DeserializeData();
        }

        // Sort Products
        public delegate int SortProductFunction(Product x, Product y);
        public void SortProducts(SortProductFunction sorter)
        {
            Products.Sort((x, y) => SortingOrderProduct * sorter(x, y));
        }

        // Sort Customers
        public delegate int SortCustomerFunction(Customer x, Customer y);
        public void SortCustomers(SortCustomerFunction sorter)
        {
            Customers.Sort((x, y) => SortingOrderCustomer * sorter(x, y));
        }

        // Sort Categories
        public void SortCategories()
        {
            Categories.Sort();
            if (SortingOrderCategory)
            {
                Categories.Reverse();
            }
        }


        // Add Product
        public bool AddProduct(out string message, string[] properties)
        {
            if(properties.Length != 5)
            {
                message = "Invalid number of property provided";
                return false;
            }

            int id;
            if (!int.TryParse(properties[0], out id) || id <= 0 || Products.Find(x => x.ID == id) == default(Product))
            {
                message = "ID should be an unique and positive integer.";
                return false;
            }

            string name = properties[1];
            if (name == "")
            {
                message = "Name should not be empty.";
                return false;
            }

            double price;
            if (!double.TryParse(properties[2], out price) || price < 0)
            {
                message = "Price should be a positive number.";
                return false;
            }

            int quantity;
            if (!int.TryParse(properties[3], out quantity) || quantity <= 0)
            {
                message = "Quantity should be a positive integer.";
                return false;
            }

            Products.Add(new Product(id, name, price, quantity, properties[4]));
            ProductChanged = true;
            message = "Successfully add a new product.";
            return true;

        }

        // Add Customer
        public bool AddCustomer(out string message, string[] properties)
        {
            if (properties.Length != 5)
            {
                message = "Invalid number of property provided";
                return false;
            }

            int id;
            if (!int.TryParse(properties[0], out id) || id <= 0 || Customers.Find(x => x.ID == id) == default(Customer))
            {
                message = "ID should be an unique and positive integer.";
                return false;
            }

            if (properties[1] == "" || properties[2] == "")
            {
                message = "First and Last Name can not be empty.";
                return false;
            }

            Customers.Add(new Customer(id, properties[1], properties[2], properties[3], properties[4]));
            message = "Successfully add a new customer.";
            return true;
        }

        // Add Category
        public bool AddCategory(out string message, string category)
        {
            if (category == "" || Categories.Contains(category))
            {
                message = "Category name should be unique and non-empty.";
                return false;
            }

            Categories.Add(category);
            message = "Successfully add a new category.";
            return true;
        }

        private void SerializeProduct()
        {
            FileStream fs = File.Open(GetPath(0), FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.WriteLine("ID,Name,Price,Quantity,Category");
                Products.ForEach(writer.WriteLine);
            }
        }

        private void SerializeCustomer()
        {
            FileStream fs = File.Open(GetPath(1), FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.WriteLine("ID,FirstName,LastName,PhoneNubmer,Email");
                Customers.ForEach(writer.WriteLine);
            }
        }

        private void SerializeCategory()
        {
            FileStream fs = File.Open(GetPath(2), FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fs))
            {
                Categories.ForEach(writer.WriteLine);
            }
        }

        public void SerializeData()
        {
            if (ProductChanged)
            {
                ProductChanged = false;
                SerializeProduct();
            }
            if (CustomerChanged)
            {
                CustomerChanged = false;
                SerializeCustomer();
            }
            if (CategoryChanged)
            {
                CategoryChanged = false;
                SerializeCategory();
            }
        }

        public void FakeData()
        {
            Categories.Add("Food");
            Categories.Add("Equipment");
            Categories.Add("Phone");
            Categories.Add("Computer");

            int i = 1;
            Products.Add(new Product(i++, "Apple", 1.3, 2, "Food"));
            Products.Add(new Product(i++, "Orange", 1.3, 2, "Food"));
            Products.Add(new Product(i++, "Ipad", 1.3, 2, "Phone"));
            Products.Add(new Product(i++, "Iphone", 1.3, 2, "Phone"));
            Products.Add(new Product(i++, "CPU", 1.3, 2, "Computer"));
            Products.Add(new Product(i++, "GPU", 1.3, 2, "Computer"));
            Products.Add(new Product(i++, "Mouse", 1.3, 2, "Computer"));
            Products.Add(new Product(i++, "Printer", 1.3, 2, "Equipment"));
            Products.Add(new Product(i++, "Scaner", 1.3, 2, "Equipment"));

            i = 1;
            Customers.Add(new Customer(i++, "firstname", "lastname", "123", "123@123"));
            Customers.Add(new Customer(i++, "firstname", "lastname", "123", "123@123"));
            Customers.Add(new Customer(i++, "firstname", "lastname", "123", "123@123"));
            Customers.Add(new Customer(i++, "firstname", "lastname", "123", "123@123"));
            Customers.Add(new Customer(i++, "firstname", "lastname", "123", "123@123"));
            Customers.Add(new Customer(i++, "firstname", "lastname", "123", "123@123"));
            Customers.Add(new Customer(i++, "firstname", "lastname", "123", "123@123"));
            Customers.Add(new Customer(i++, "firstname", "lastname", "123", "123@123"));
            Customers.Add(new Customer(i++, "firstname", "lastname", "123", "123@123"));
            Customers.Add(new Customer(i++, "firstname", "lastname", "123", "123@123"));
        }

        private void DeserializeProduct()
        {
            FileStream fs = File.Open(GetPath(0), FileMode.Open);
            using(StreamReader reader = new StreamReader(fs))
            {
                reader.ReadLine();
                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] values = line.Split(',');
                    Products.Add(new Product(int.Parse(values[0]), values[1], double.Parse(values[2]), int.Parse(values[3]), values[4]));
                    line = reader.ReadLine();
                }
            }
        }

        private void DeserializeCustomer()
        {
            FileStream fs = File.Open(GetPath(1), FileMode.Open);
            using (StreamReader reader = new StreamReader(fs))
            {
                reader.ReadLine();
                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] values = line.Split(',');
                    Customers.Add(new Customer(int.Parse(values[0]), values[1], values[2], values[3], values[4]));
                    line = reader.ReadLine();
                }
            }
        }

        private void DeserializeCategory()
        {
            FileStream fs = File.Open(GetPath(2), FileMode.Open);
            using (StreamReader reader = new StreamReader(fs))
            {
                string line = reader.ReadLine(), message = "";
                while (line != null)
                {
                    _ = AddCategory(out message, line);
                    line = reader.ReadLine();
                }
            }
        }

        public void DeserializeData()
        {
            DeserializeCategory();
            DeserializeProduct();
            DeserializeCustomer();
        }

        public void RemoveProduct(int index)
        {
            Products.RemoveAt(index);
            ProductChanged = true;
        }

        public void RemoveCustomer(int index)
        {
            Customers.RemoveAt(index);
            CustomerChanged = true;
        }

        public void RemoveCategory(int index)
        {
            if (Categories[index] == "None")
            {
                return;
            }
            CategoryChanged = true;
            foreach (Product product in Products)
            {
                if (product.Category == Categories[index])
                {
                    product.Category = "None";
                    ProductChanged = true;
                }
            }
            Categories.RemoveAt(index);
        }
    }
}
