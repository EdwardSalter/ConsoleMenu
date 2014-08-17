using System.Collections.Generic;
using System.Linq;

namespace ConsoleMenu
{
    public static class MenuFactory
    {
        public static IMenu ToMenu(this IEnumerable<IMenuItem> menuItems, string instructionalText, int maxAllowed = Menu.MaxOnScreen)
        {
            var menus = menuItems.ToMenuList(instructionalText, maxAllowed).ToList();
            if (menus.Count() > 1)
            {
                return new LinkedMenu(menus);
            }
            return menus[0];
        }

        internal static IEnumerable<IMenu> ToMenuList(this IEnumerable<IMenuItem> menuItems, string instructionalText, int maxAllowed = Menu.MaxOnScreen)
        {
            var source = menuItems.ToNumberedMenuItems(maxAllowed).ToList();
            var currentMenu = new Menu(instructionalText);
            foreach (var menuItem in source)
            {
                currentMenu.AddMenuItem(menuItem);

                if (menuItem.IsMore)
                {
                    yield return currentMenu;
                    currentMenu = new Menu(instructionalText);
                }
            }

            if (!source.Last().IsMore)
            {
                yield return currentMenu;
            }
        }
    }
}
