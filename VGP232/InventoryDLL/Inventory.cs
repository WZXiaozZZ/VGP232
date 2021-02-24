using System;
using System.Collections.Generic;

namespace InventoryDLL
{
    public class Inventory
    {
        public List<Product> Products { get; set; }
        public List<Customer> Customers { get; set; }
        public List<string> Categories { get; set; }

        public int SortingOrderProduct { get; set; } // 1 for Ascending order, -1 for Descending order
        public int SortingOrderCustomer { get; set; } // 1 for Ascending order, -1 for Descending order
        public bool SortingOrderCategory { get; set; } // False for Ascending order, True for Descending order


        public Inventory()
        {
            Products = new List<Product>();
            Customers = new List<Customer>();
            Categories = new List<string>();

            SortingOrderProduct = 1;
            SortingOrderCustomer = 1;
            SortingOrderCategory = false;
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
        public bool AddProduct(out string error, params string[] properties)
        {
            try
            {
                int id;
                if (!int.TryParse(properties[0], out id) || id <= 0 || Products.Find(x => x.ID == id) == default(Product))
                {
                    error = "ID should be an unique and positive integer.";
                    return false;
                }

                string name = properties[1];
                if (name == "")
                {
                    error = "Name should not be empty.";
                    return false;
                }

                double price;
                if(!double.TryParse(properties[2], out price) || price < 0)
                {
                    error = "Price should be a positive number.";
                    return false;
                }

                int quantity;
                if (!int.TryParse(properties[3], out quantity) || quantity <= 0)
                {
                    error = "Quantity should be a positive integer.";
                    return false;
                }

                Products.Add(new Product(id, name, price, quantity, properties[4]));

                error = "";
                return true;
            }
            catch (Exception)
            {
                error = "Invalid number of property provided";
                return false;
            }
        }
    }
}
