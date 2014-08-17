using System;
using System.Collections.Generic;
using System.Globalization;

namespace ConsoleMenu
{
    internal static class MenuItemFactory
    {
        public static IEnumerable<IMenuItem> CreateMenuItemsFromObjects<T>(
            IEnumerable<T> items,
            Func<T, string> displayTextFunc,
            T lastUsed = default(T))
        {
            var menuItems = new List<IMenuItem>();

            foreach (var item in items)
            {
                var menuItem = new MenuItem(default(char), displayTextFunc.Invoke(item));
                if (item.Equals(lastUsed))
                {
                    menuItem.IsDefault = true;
                }

                menuItems.Add(menuItem);
            }

            return menuItems;
        }

        public static IEnumerable<IMenuItem> ToNumberedMenuItems(this IEnumerable<IMenuItem> menuItems, int maxAllowed = Menu.MaxOnScreen)
        {
            if (menuItems == null) throw new ArgumentNullException("menuItems");
            if (maxAllowed < 1) throw new ArgumentOutOfRangeException("maxAllowed", "You must be able to display at least one menu.");

            var newMenus = new List<IMenuItem>();

            bool moreAdded = false;
            int i = 0;
            foreach (var menuItem in menuItems)
            {
                if (++i > maxAllowed)
                {
                    var index = maxAllowed == 9 ? 0 : i;
                    newMenus.Add(new MenuItem(index.ToChar(), " -- MORE --") { IsMore = true });
                    moreAdded = true;
                    i = 1;
                }
                menuItem.Shortcut = i.ToChar();
                newMenus.Add(menuItem);
            }

            if (moreAdded)
            {
                newMenus.Add(new MenuItem((++i).ToChar(), " -- MORE --") { IsMore = true });
            }
            return newMenus;
        }

        private static char ToChar(this int value)
        {
            return value.ToString(CultureInfo.InvariantCulture)[0];
        }
    }
}
