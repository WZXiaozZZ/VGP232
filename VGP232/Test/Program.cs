using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Test
{
    class RSACSPSample
    {
        static void Main()
        {
            var test = new Class();
            test.strlist.Sort((x, y) => 1 * Class.Compare(x, y));
            test.strlist.ForEach(Console.WriteLine);
            Console.WriteLine();
            test.sortby(Class.Compare);
            test.strlist.ForEach(Console.WriteLine);
            
            Console.WriteLine();
            test.strlist.Sort();
            test.strlist.ForEach(Console.WriteLine);

            Console.WriteLine();
            test.strlist.Reverse();
            test.strlist.ForEach(Console.WriteLine);

            Console.WriteLine();
            test.unique();

            Console.WriteLine(default(Class) == null);
        }
    }
}
