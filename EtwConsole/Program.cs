using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Trycatchthat.EtwConsole
{
    class Program
    {

        public static TextWriter Out = Console.Out;
        static void Main(string[] args)
        {
            BuildIndex<Base>();
            BuildIndex<Derived>();
            //WriteLine($"Enter an option (1..{} to run the sample.");
            


        }

        private static void BuildIndex<T>()
        {
            var methods = typeof(T).GetMethods(System.Reflection.BindingFlags.Instance |
                                                     System.Reflection.BindingFlags.NonPublic |
                                                     System.Reflection.BindingFlags.Public |
                                                     System.Reflection.BindingFlags.Static);

            WriteLine($"CLASS:{typeof(T).FullName}");
            foreach (var m in methods)
            {
                WriteLine($"{m.ReflectedType}, {m.Name}");
            }
        }
    }

    class Base
    {
        public static string PublicStaticReturnString() => "a";
        public void Public() { }
        private void Private() { }

        protected virtual void Protected() { }

        public int MyProperty { get; set; }
    }

    class Derived : Base
    {
        protected override void Protected() {}
    }
}
