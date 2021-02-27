using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryDLL
{
    [Serializable]
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }

        public Product(int id, string name, double price, int quantity, string category)
        {
            ID = id;
            Name = name;
            Price = price;
            Quantity = quantity;
            Category = category;
        }

        /// <summary>
        /// The Comparator function to check for ID
        /// </summary>
        /// <param name="left">Left side Product</param>
        /// <param name="right">Right side Product</param>
        /// <returns> -1 (or any other negative value) for "less than", 0 for "equals", or 1 (or any other positive value) for "greater than"</returns>
        public static int CompareByID(Product left, Product right)
        {
            return left.ID.CompareTo(right.ID);
        }

        /// <summary>
        /// The Comparator function to check for Name
        /// </summary>
        /// <param name="left">Left side Product</param>
        /// <param name="right">Right side Product</param>
        /// <returns> -1 (or any other negative value) for "less than", 0 for "equals", or 1 (or any other positive value) for "greater than"</returns>
        public static int CompareByName(Product left, Product right)
        {
            return left.Name.CompareTo(right.Name);
        }

        /// <summary>
        /// The Comparator function to check for Price
        /// </summary>
        /// <param name="left">Left side Product</param>
        /// <param name="right">Right side Product</param>
        /// <returns> -1 (or any other negative value) for "less than", 0 for "equals", or 1 (or any other positive value) for "greater than"</returns>
        public static int CompareByPrice(Product left, Product right)
        {
            return left.Price.CompareTo(right.Price);
        }

        /// <summary>
        /// The Comparator function to check for Category
        /// </summary>
        /// <param name="left">Left side Product</param>
        /// <param name="right">Right side Product</param>
        /// <returns> -1 (or any other negative value) for "less than", 0 for "equals", or 1 (or any other positive value) for "greater than"</returns>
        public static int CompareByCategory(Product left, Product right)
        {
            return left.Category.CompareTo(right.Category);
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4}", ID, Name, Price, Quantity, Category);
        }

    }
}
