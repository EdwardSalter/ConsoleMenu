using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleMenu
{
    public class LinkedMenu : IMenu
    {
        private readonly IList<IMenu> m_menus;

        public LinkedMenu(IEnumerable<IMenu> menus)
        {
            if (menus == null) throw new ArgumentNullException("menus");
            m_menus = menus.ToList();
            if (!m_menus.Any()) throw new ArgumentOutOfRangeException("menus", "There must be at least one menu");

        }

        public IMenuItem Display()
        {
            var enumerator = m_menus.GetEnumerator();

            while (true)
            {
                if (!enumerator.MoveNext())
                {
                    enumerator.Reset();
                    enumerator.MoveNext();
                }

                var choice = enumerator.Current.Display();
                if (!choice.IsMore)
                {
                    return choice;
                }
            }
        }
    }
}
