using System;

namespace MyConsoleApp
{
    class Program
    {
        private const string Value = "Hello World!! (value)";

        static void Main(string[] args)
        {
            Console.WriteLine(Value);
            Console.WriteLine("Version: {0}", Environment.Version.ToString());            
            Console.WriteLine("GetType: {0}", Environment.Version.GetType().ToString());            
	        foreach ( string arg in args ) Console.WriteLine("Item :{0}",arg);
        }
    }
}
