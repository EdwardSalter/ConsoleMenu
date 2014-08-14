using System;
using System.Collections.Generic;
using ConsoleMenu;

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

            var menu = new TypedMenu<SimpleClass>(choices, x => x.Name, null, "Pick one");
            ChooseMenu(menu);

            const int defaultVal = 2;
            var menuWithDefault = new TypedMenu<SimpleClass>(choices, x => x.Name, choices[defaultVal -1], 
                "This should have default value of " + defaultVal);
            ChooseMenu(menuWithDefault);
        }

        private static void ChooseMenu(TypedMenu<SimpleClass> menu)
        {
            var choice = menu.Display();

            Console.WriteLine("You chose: {0}", choice.Name);
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}
