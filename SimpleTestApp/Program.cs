using System;
using System.Collections.Generic;

namespace SimpleTestApp
{
    class Program
    {
        private class SimpleClass
        {
            public string Name { get; set; }
        }

        static void Main()
        {
            var choices = new List<SimpleClass>
            {
                new SimpleClass{Name = "One"},
                new SimpleClass{Name = "Two"},
                new SimpleClass{Name = "Three"},
                new SimpleClass{Name = "Four"},
            };

            var menu = new ConsoleMenu.TypedMenu<SimpleClass>(choices, x => x.Name, x => false, "Pick one");
            var choice = menu.Display();

            Console.WriteLine("You chose: {0}", choice.Name);
            Console.ReadKey(true);
        }
    }
}
