using System.Collections.Generic;

namespace ConsoleMenu
{
    class LinkedMenu
    {
        private readonly IEnumerable<IMenu> m_menus;

        public LinkedMenu(IEnumerable<IMenu> menus)
        {
            m_menus = menus;
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
