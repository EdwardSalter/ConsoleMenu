using System.Linq;
using NUnit.Framework;

namespace ConsoleMenu.Tests
{
    class MenuItemFactoryTests
    {
        [Test]
        public void CreateMenuItemsFromObjects_NotGivenADefaultValue_NoMenuItemIsMarkedAsDefault()
        {
            var items = new[] { "one", "two" };
            var menuItems = MenuItemFactory.CreateMenuItemsFromObjects(items, s => s);

            Assert.IsFalse(menuItems.Any(mi => mi.IsDefault));
        }

        [Test]
        public void CreateMenuItemsFromObjects_GivenADefaultValue_ReturnsAMenuItemThatIsMarkedAsDefault()
        {
            var items = new[] {"non-default", "default"};
            var menuItems = MenuItemFactory.CreateMenuItemsFromObjects(items, s => s, "default");

            Assert.IsTrue(menuItems.Any(mi => mi.IsDefault));
        }
    }
}
