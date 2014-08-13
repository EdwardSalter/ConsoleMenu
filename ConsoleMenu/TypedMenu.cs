using System;
using System.Collections.Generic;

namespace ConsoleMenu
{
    public class TypedMenu<T>
    {
        private readonly IList<T> m_items;
        private readonly Menu m_menu;

        public TypedMenu(IList<T> choices, Func<T, string> nameFunc, T lastUsed, string instructionalText)
            : this(choices, nameFunc, lastUsed, instructionalText, new ConsoleMenuIOProvider())
        {
        }

        internal TypedMenu(IList<T> choices, Func<T, string> nameFunc, T lastUsed, string instructionalText, IMenuIOProvider io)
        {
            m_items = choices;

            var menuItems = MenuItemFactory.CreateMenuItemsFromObjects(choices, nameFunc, lastUsed);
            m_menu = new Menu(instructionalText, menuItems, io);
        }

        public T Display()
        {
            var indexChosen = m_menu.Display();
            return m_items[indexChosen];
        }
    }
}
