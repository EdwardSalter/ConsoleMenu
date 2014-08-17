using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleMenu
{
    public class TypedMenu<T>
    {
        private readonly IList<T> m_items;
        private readonly IMenu m_menu;
        private readonly IEnumerable<IMenuItem> m_nonNumberedItems;

        // TODO: FACTORY METHOD
        public TypedMenu(IList<T> choices, Func<T, string> nameFunc, T lastUsed, string instructionalText)
        {
            m_items = choices;

            m_nonNumberedItems = MenuItemFactory.CreateMenuItemsFromObjects(choices, nameFunc, lastUsed);

            m_menu = m_nonNumberedItems.ToMenu(instructionalText);
        }

        public T Display()
        {
            var indexChosen = m_menu.Display();
            var index = m_nonNumberedItems.ToList().IndexOf(indexChosen);

            return m_items[index];
        }
    }
}
