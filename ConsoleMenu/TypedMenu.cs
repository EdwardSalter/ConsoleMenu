using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleMenu
{
    public class TypedMenu<T>
    {
        private readonly IList<T> m_items;
        private readonly Menu m_menu;
        private readonly IEnumerable<IMenuItem> m_nonNumberedItems;

        public TypedMenu(IList<T> choices, Func<T, string> nameFunc, T lastUsed, string instructionalText)
            : this(choices, nameFunc, lastUsed, instructionalText, new ConsoleMenuIOProvider())
        {
        }

        internal TypedMenu(IList<T> choices, Func<T, string> nameFunc, T lastUsed, string instructionalText, IMenuIOProvider io)
        {
            m_items = choices;

            m_nonNumberedItems = MenuItemFactory.CreateMenuItemsFromObjects(choices, nameFunc, lastUsed);
            var menuItems = m_nonNumberedItems.ToNumberedMenuItems();
            m_menu = new Menu(instructionalText, menuItems, io);
        }

        public T Display()
        {
            var indexChosen = m_menu.Display();
            var index = m_nonNumberedItems.ToList().IndexOf(indexChosen);

            return m_items[index];
        }
    }
}
