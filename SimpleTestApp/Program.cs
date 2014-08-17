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
            var tooManyChoices = new List<SimpleClass>(choices);
            tooManyChoices.AddRange(new List<SimpleClass>
            {
                new SimpleClass{Name = "Five"},
                new SimpleClass{Name = "Six"},
                new SimpleClass{Name = "Seven"},
                new SimpleClass{Name = "Eight"},
                new SimpleClass{Name = "Nine"},
                new SimpleClass{Name = "Ten"},
                new SimpleClass{Name = "Eleven"},
                new SimpleClass{Name = "Twelve"},
            });

            var menu = new TypedMenu<SimpleClass>(choices, "Pick one", x => x.Name, null);
            ChooseMenu(menu);

            const int defaultVal = 2;
            var menuWithDefault = new TypedMenu<SimpleClass>(choices, 
                "This should have default value of " + defaultVal, x => x.Name, choices[defaultVal -1]);
            ChooseMenu(menuWithDefault);

            var tooManyChoicesMenu = new TypedMenu<SimpleClass>(tooManyChoices, "This should go on to the next screen", x => x.Name, null);
            ChooseMenu(tooManyChoicesMenu);
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
