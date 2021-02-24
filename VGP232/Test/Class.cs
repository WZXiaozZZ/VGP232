using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    public class Class
    {
        public SortedSet<string> theSet { get; set; }
        public List<string> strlist { get; set; }
        public Class()
        {
            theSet = new SortedSet<string>();
            theSet.Add("Phone");
            theSet.Add("phone");
            theSet.Add("Laptop");
            theSet.Add("Ipad");
            theSet.Add("Screen");

            strlist = new List<string>();
            strlist.Add("Phone");
            strlist.Add("phone");
            strlist.Add("Laptop");
            strlist.Add("Ipad");
            strlist.Add("Screen");
        }
        public delegate int SortProduct(string x, string y);

        public static int Compare(string left, string right)
        {
            return left.CompareTo(right);
        }

        public void sortby(SortProduct myFunction)
        {
            strlist.Sort((x, y) => -1 * myFunction(x, y));
        }

        public void unique()
        {
            if(strlist.Find(x=>x[0] == 'a') == default(string))
            {
                Console.WriteLine("no a");
            }
        }
    }
}
