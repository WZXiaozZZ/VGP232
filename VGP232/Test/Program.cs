using System;

namespace Test
{
    class Program
    {
        enum MyEnum
        {
            a,
            b,
            c
        }

        static void Main(string[] args)
        {
            try
            {
                MyEnum e = (MyEnum)Enum.Parse(typeof(MyEnum), "agf");
                Console.WriteLine(e);
            }
            catch (Exception)
            {
                Console.WriteLine("error");
            }

        }
    }
}
