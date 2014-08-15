﻿using System;
using System.Collections.Generic;
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

        [Test]
        public void ToNumberedMenuItems_GivenANullMenuItemList_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => MenuItemFactory.ToNumberedMenuItems(null));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void ToNumberedMenuItems_GivenAMaximumAllowedOfLessThanOne_Throws(int numAllowed)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new List<MenuItem>().ToNumberedMenuItems(numAllowed));
        }

        [TestCase('1', 0)]
        [TestCase('2', 1)]
        public void ToNumberedMenuItems_GivenAListOfMenuItems_MenuItemsHaveCorrectShortcutCharacter(char expected, int index)
        {
            var menuItems = new[] { new MenuItem(), new MenuItem() };

            var numberedMenuItems = menuItems.ToNumberedMenuItems().ToList();

            Assert.AreEqual(expected, numberedMenuItems[index].Shortcut);
        }

        [Test]
        public void ToNumberedMenuItems_GivenAMaximumAllowedOfOneAndTwoItemsGiven_HasAMoreMenuInSecondIndex()
        {
            var menuItems = new[] { new MenuItem(), new MenuItem() };

            var numberedMenuItems = menuItems.ToNumberedMenuItems(1).ToList();

            Assert.IsTrue(numberedMenuItems[1].IsMore);
        }

        [Test]
        public void ToNumberedMenuItems_GivenAMaximumAllowedOfOneAndTwoItemsGiven_MoreMenuItemHasShortcutOfTwo()
        {
            var menuItems = new[] { new MenuItem(), new MenuItem() };

            var numberedMenuItems = menuItems.ToNumberedMenuItems(1).ToList();

            Assert.AreEqual('2', numberedMenuItems[1].Shortcut);
        }

        [Test]
        public void ToNumberedMenuItems_GivenAMaximumAllowedOfOneAndTwoItemsGiven_ThirdMenuItemHasShortcutOfOne()
        {
            var menuItems = new[] { new MenuItem(), new MenuItem() };

            var numberedMenuItems = menuItems.ToNumberedMenuItems(1).ToList();

            Assert.AreEqual('1', numberedMenuItems[2].Shortcut);
        }

        [Test]
        public void ToNumberedMenuItems_GivenAMaximumAllowedOfOneAndTwoItemsGiven_AnExtraMoreMenuItemIsAddedToTheEnd()
        {
            var menuItems = new[] { new MenuItem(), new MenuItem() };

            var numberedMenuItems = menuItems.ToNumberedMenuItems(1).ToList();

            Assert.IsTrue(numberedMenuItems[3].IsMore);
        }

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

            Assert.AreEqual(3, menus[0].MenuItems.Count());
        }

        [Test]
        public void ToMenuList_GivenAListOfMenuItemsThatExceedsMaxAllowed_SecondMenuIsNotFull()
        {
            var menuItems = new[] { new MenuItem(), new MenuItem(), new MenuItem() };

            var menus = menuItems.ToMenuList("SomeText", 2).ToList();

            Assert.AreEqual(2, menus[1].MenuItems.Count());
        }

        [Test]
        public void ToMenuList_GivenAListOfMenuItemsThatExceedsMaxAllowed_FirstMenuContainsMoreItem()
        {
            var menuItems = new[] { new MenuItem(), new MenuItem()};

            var menus = menuItems.ToMenuList("SomeText", 1).ToList();

            Assert.IsTrue(menus[0].MenuItems.Any(menu => menu.IsMore));
        }

        [Test]
        public void ToMenuList_GivenAListOfMenuItemsThatExceedsMaxAllowed_SecondMenuContainsMoreItem()
        {
            var menuItems = new[] { new MenuItem(), new MenuItem() };

            var menus = menuItems.ToMenuList("SomeText", 1).ToList();

            Assert.IsTrue(menus[1].MenuItems.Any(menu => menu.IsMore));
        }
    }
}
