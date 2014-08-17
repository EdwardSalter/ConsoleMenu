using System.Linq;
using NUnit.Framework;

namespace ConsoleMenu.Tests
{
    [ TestFixture]
    public class MenuFactoryTests
    {
        [Test]
        public void ToMenuList_GivenAListOfMenuItemsThatExceedsMaxAllowed_ReturnsMultipleMenus()
        {
            var menuItems = new[] { new MenuItem(), new MenuItem() };

            var menus = menuItems.ToMenuList("SomeText", 1).ToList();

            Assert.AreEqual(2, menus.Count);
        }

        [Test]
        public void ToMenuList_GivenAListOfMenuItemsThatExceedsMaxAllowed_FirstMenuIsFull()
        {
            var menuItems = new[] { new MenuItem(), new MenuItem(), new MenuItem() };

            var menus = menuItems.ToMenuList("SomeText", 2).ToList();

            Assert.AreEqual(3, AsTextMenu(menus[0]).MenuItems.Count());
        }

        [Test]
        public void ToMenuList_GivenAListOfMenuItemsThatExceedsMaxAllowed_SecondMenuIsNotFull()
        {
            var menuItems = new[] { new MenuItem(), new MenuItem(), new MenuItem() };

            var menus = menuItems.ToMenuList("SomeText", 2).ToList();

            Assert.AreEqual(2, AsTextMenu(menus[1]).MenuItems.Count());
        }

        [Test]
        public void ToMenuList_GivenAListOfMenuItemsThatExceedsMaxAllowed_FirstMenuContainsMoreItem()
        {
            var menuItems = new[] { new MenuItem(), new MenuItem() };

            var menus = menuItems.ToMenuList("SomeText", 1).ToList();

            Assert.IsTrue(AsTextMenu(menus[0]).MenuItems.Any(menu => menu.IsMore));
        }

        [Test]
        public void ToMenuList_GivenAListOfMenuItemsThatExceedsMaxAllowed_SecondMenuContainsMoreItem()
        {
            var menuItems = new[] { new MenuItem(), new MenuItem() };

            var menus = menuItems.ToMenuList("SomeText", 1).ToList();

            Assert.IsTrue(AsTextMenu(menus[1]).MenuItems.Any(menu => menu.IsMore));
        }

        [Test]
        public void ToMenu_GivenAListOfMenuItemsThatDoesNotExceedsMaxAllowed_ReturnsAMenuObject()
        {
            var menuItems = new[] { new MenuItem() };

            var menu = menuItems.ToMenu("SomeText");

            Assert.IsInstanceOf<Menu>(menu);
        }

        [Test]
        public void ToMenu_GivenAListOfMenuItemsThatExceedsMaxAllowed_ReturnsALinkedMenuObject()
        {
            var menuItems = new[] { new MenuItem(), new MenuItem() };

            var menu = menuItems.ToMenu("SomeText", 1);

            Assert.IsInstanceOf<LinkedMenu>(menu);
        }


        private static ITextMenu AsTextMenu(IMenu menu)
        {
            return menu as ITextMenu;
        }
    }
}
