using System;

namespace InventoryDLL
{
    [Serializable]
    public class Customer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Customer(int id, string first, string last, string phone, string email)
        {
            ID = id;
            FirstName = first;
            LastName = last;
            PhoneNumber = phone;
            Email = email;
        }

        /// <summary>
        /// The Comparator function to check for ID
        /// </summary>
        /// <param name="left">Left side Customer</param>
        /// <param name="right">Right side Customer</param>
        /// <returns> -1 (or any other negative value) for "less than", 0 for "equals", or 1 (or any other positive value) for "greater than"</returns>
        public static int CompareByID(Customer left, Customer right)
        {
            return left.ID.CompareTo(right.ID);
        }

        /// <summary>
        /// The Comparator function to check for First Name
        /// </summary>
        /// <param name="left">Left side Customer</param>
        /// <param name="right">Right side Customer</param>
        /// <returns> -1 (or any other negative value) for "less than", 0 for "equals", or 1 (or any other positive value) for "greater than"</returns>
        public static int CompareByFirstName(Customer left, Customer right)
        {
            return left.FirstName.CompareTo(right.FirstName);
        }

        /// <summary>
        /// The Comparator function to check for Last Name
        /// </summary>
        /// <param name="left">Left side Customer</param>
        /// <param name="right">Right side Customer</param>
        /// <returns> -1 (or any other negative value) for "less than", 0 for "equals", or 1 (or any other positive value) for "greater than"</returns>
        public static int CompareByLastName(Customer left, Customer right)
        {
            return left.LastName.CompareTo(right.LastName);
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4}", ID, FirstName, LastName, PhoneNumber, Email);
        }
    }
}

