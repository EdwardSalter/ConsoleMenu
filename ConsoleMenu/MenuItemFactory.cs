using System;
using System.Collections.Generic;

namespace ConsoleMenu
{
    public static class MenuItemFactory
    {
        public static IEnumerable<IMenuItem> CreateMenuItemsFromObjects<T>(
            IEnumerable<T> items,
            Func<T, string> displayTextFunc,
            T lastUsed = default(T))
        {
            var menuItems = new List<IMenuItem>();

            foreach (var item in items)
            {
                var menuItem = new MenuItem(displayTextFunc.Invoke(item));
                if (item.Equals(lastUsed))
                {
                    menuItem.IsDefault = true;
                }

                menuItems.Add(menuItem);
            }

            return menuItems;
        }
    }
}
