using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleMenu
{
    public class TypedMenu<T>
    {
        private readonly IList<T> m_items;
        private readonly LinkedMenu m_menu;
        private readonly IEnumerable<IMenuItem> m_nonNumberedItems;

        public TypedMenu(IList<T> choices, Func<T, string> nameFunc, T lastUsed, string instructionalText)
            : this(choices, nameFunc, lastUsed, instructionalText, new ConsoleMenuIOProvider())
        {
        }

        internal TypedMenu(IList<T> choices, Func<T, string> nameFunc, T lastUsed, string instructionalText, IMenuIOProvider io)
        {
            m_items = choices;

            m_nonNumberedItems = MenuItemFactory.CreateMenuItemsFromObjects(choices, nameFunc, lastUsed);

            m_menu = new LinkedMenu(m_nonNumberedItems.ToMenuList(instructionalText));
        }

        public T Display()
        {
            var indexChosen = m_menu.Display();
            var index = m_nonNumberedItems.ToList().IndexOf(indexChosen);

            return m_items[index];
        }
    }
}
