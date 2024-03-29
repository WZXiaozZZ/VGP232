﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace InventoryDLL
{
    [Serializable]
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

        [JsonProperty(Required = Required.Always)]
        public List<Product> Products { get; set; }

        [JsonProperty(Required = Required.Always)]
        public List<Customer> Customers { get; set; }
        
        [JsonProperty(Required = Required.Always)]
        public List<string> Categories { get; set; }

        public bool SortingOrder { get; set; } // False for Ascending order, True for Descending order
        public bool ProductChanged { get; set; }
        public bool CustomerChanged { get; set; }
        public bool CategoryChanged { get; set; }

        public Inventory()
        {
            Products = new List<Product>();
            Customers = new List<Customer>();
            Categories = new List<string>();

            SortingOrder = false;
            ProductChanged = false;
            CustomerChanged = false;
            CategoryChanged = false;


            //DeserializeData();
        }

        // Sort Products
        public delegate int SortProductFunction(Product x, Product y);
        public void SortProducts(SortProductFunction sorter)
        {
            if (SortingOrder)
            {
                Products.Sort((x, y) => sorter(y, x));
            }
            else
            {
                Products.Sort((x, y) => sorter(x, y));
            }
        }

        // Sort Customers
        public delegate int SortCustomerFunction(Customer x, Customer y);
        public void SortCustomers(SortCustomerFunction sorter)
        {
            if (SortingOrder)
            {
                Customers.Sort((x, y) => sorter(y, x)); 
            }
            else
            {
                Customers.Sort((x, y) => sorter(x, y)); 
            }
        }

        // Sort Categories
        public void SortCategories()
        {
            Categories.Sort();
            if (SortingOrder)
            {
                Categories.Reverse();
            }
        }


        // Add Product
        public bool AddProduct(out string message, params string[] properties)
        {
            if(properties.Length != 5)
            {
                message = "Invalid number of property provided";
                return false;
            }

            if (!int.TryParse(properties[0], out int id) || id <= 0 || Products.Find(x => x.ID == id) != default(Product))
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

            if (!double.TryParse(properties[2], out double price) || price < 0)
            {
                message = "Price should be a positive number.";
                return false;
            }

            if (!int.TryParse(properties[3], out int quantity) || quantity <= 0)
            {
                message = "Quantity should be a positive integer.";
                return false;
            }

            Products.Add(new Product(id, name, price, quantity, properties[4]));
            ProductChanged = true;
            message = "Successfully add a new product.";
            return true;

        }

        // Edit Product
        public bool EditProduct(out string message, int index, params string[] properties)
        {
            if (properties.Length != 5)
            {
                message = "Invalid number of property provided";
                return false;
            }

            if (!int.TryParse(properties[0], out int id) || id <= 0 || (id != Products[index].ID && Products.Find(x => x.ID == id) != default(Product)))
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

            if (!double.TryParse(properties[2], out double price) || price < 0)
            {
                message = "Price should be a positive number.";
                return false;
            }

            if (!int.TryParse(properties[3], out int quantity) || quantity <= 0)
            {
                message = "Quantity should be a positive integer.";
                return false;
            }

            Products[index] = new Product(id, name, price, quantity, properties[4]);
            ProductChanged = true;
            message = "Successfully edit the product.";
            return true;

        }

        // Add Customer
        public bool AddCustomer(out string message, params string[] properties)
        {
            if (properties.Length != 5)
            {
                message = "Invalid number of property provided";
                return false;
            }

            if (!int.TryParse(properties[0], out int id) || id <= 0 || Customers.Find(x => x.ID == id) != default(Customer))
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
            CustomerChanged = true;
            message = "Successfully add a new customer.";
            return true;
        }

        // Edit Customer
        public bool EditCustomer(out string message, int index, params string[] properties)
        {
            if (properties.Length != 5)
            {
                message = "Invalid number of property provided";
                return false;
            }

            if (!int.TryParse(properties[0], out int id) || id <= 0 || (id != Customers[index].ID && Customers.Find(x => x.ID == id) != default(Customer)))
            {
                message = "ID should be an unique and positive integer.";
                return false;
            }

            if (properties[1] == "" || properties[2] == "")
            {
                message = "First and Last Name can not be empty.";
                return false;
            }
            Customers[index] = new Customer(id, properties[1], properties[2], properties[3], properties[4]);
            CustomerChanged = true;
            message = "Successfully edit the customer.";
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

        // Edit Category
        public bool EditCategory(out string message, int index, string value)
        {
            if (Categories[index] == value)
            {
                message = "The category is unchanged.";
                return false;
            }
            if (Categories.Contains(value))
            {
                message = "The category already exists.";
                return false;
            }

            CategoryChanged = true;
            foreach (Product product in Products)
            {
                if (product.Category == Categories[index])
                {
                    product.Category = value;
                    ProductChanged = true;
                }
            }

            Categories[index] = value;
            message = "Successfully edit the category.";
            return true;
        }

        private void SerializeProduct()
        {
            FileStream fs = File.Open(GetPath(0), FileMode.Create);
            using StreamWriter writer = new StreamWriter(fs);
            writer.WriteLine("ID,Name,Price,Quantity,Category");
            Products.ForEach(writer.WriteLine);
        }

        private void SerializeCustomer()
        {
            FileStream fs = File.Open(GetPath(1), FileMode.Create);
            using StreamWriter writer = new StreamWriter(fs);
            writer.WriteLine("ID,FirstName,LastName,PhoneNubmer,Email");
            Customers.ForEach(writer.WriteLine);
        }

        private void SerializeCategory()
        {
            FileStream fs = File.Open(GetPath(2), FileMode.Create);
            using StreamWriter writer = new StreamWriter(fs);
            Categories.ForEach(writer.WriteLine);
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
            using StreamReader reader = new StreamReader(fs);
            reader.ReadLine();
            string line = reader.ReadLine();
            while (line != null)
            {
                string[] values = line.Split(',');
                Products.Add(new Product(int.Parse(values[0]), values[1], double.Parse(values[2]), int.Parse(values[3]), values[4]));
                line = reader.ReadLine();
            }
        }

        private void DeserializeCustomer()
        {
            FileStream fs = File.Open(GetPath(1), FileMode.Open);
            using StreamReader reader = new StreamReader(fs);
            reader.ReadLine();
            string line = reader.ReadLine();
            while (line != null)
            {
                string[] values = line.Split(',');
                Customers.Add(new Customer(int.Parse(values[0]), values[1], values[2], values[3], values[4]));
                line = reader.ReadLine();
            }
        }

        private void DeserializeCategory()
        {
            FileStream fs = File.Open(GetPath(2), FileMode.Open);
            using StreamReader reader = new StreamReader(fs);
            string line = reader.ReadLine();
            while (line != null)
            {
                _ = AddCategory(out _, line);
                line = reader.ReadLine();
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

        public void RemoveCategory(string category)
        {
            if (category == "None")
            {
                return;
            }
            CategoryChanged = true;
            foreach (Product product in Products)
            {
                if (product.Category == category)
                {
                    product.Category = "None";
                    ProductChanged = true;
                }
            }
            Categories.Remove(category);
        }


        public bool SaveToJson(string path)
        {
            try
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    string json = JsonConvert.SerializeObject(this);
                    sw.Write(json);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool LoadFromJson(string path)
        {
            try
            {
                using(StreamReader sr = new StreamReader(path))
                {
                    string json = sr.ReadToEnd();
                    Inventory data = JsonConvert.DeserializeObject<Inventory>(json);
                    if (data.Categories.Count == 0)
                    {
                        DeserializeData();
                        return false;
                    }
                    Customers.Clear();
                    Customers = data.Customers;
                    Products.Clear();
                    Products = data.Products;
                    Categories.Clear();
                    Categories = data.Categories;

                    SortingOrder = false;
                    ProductChanged = false;
                    CustomerChanged = false;
                    CategoryChanged = false;
                    return true;
                }
            }
            catch (Exception)
            {
                DeserializeData();
                return false;
            }
        }
    }
}
