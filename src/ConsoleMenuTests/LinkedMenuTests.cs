using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace ConsoleMenu.Tests
{
    [TestFixture]
    public class LinkedMenuTests
    {
        [Test]
        public void Constructor_GivenANullListOfMenus_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new LinkedMenu(null));
        }

        [Test]
        public void Constructor_GivenAnEmptyListOfMenus_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new LinkedMenu(new List<IMenu>()));
        }

        [Test]
        public void Display_GivenASingleMenuThatReturnsANonMoreMenuItem_ReturnsThatMenuItem()
        {
            var mockMenuItem = new Mock<IMenuItem>();
            var fakeMenu = new Mock<ITextMenu>();
            fakeMenu.Setup(m => m.Display()).Returns(mockMenuItem.Object);
            var menu = new LinkedMenu(new[] { fakeMenu.Object });

            var chosen = menu.Display();

            Assert.AreSame(mockMenuItem.Object, chosen);
        }

        [Test]
        public void Display_GivenMultipleMenusAndMoreChosen_DisplayCalledOnSecondMenu()
        {
            var fakeMenu1 = CreateMenuThatReturnsMenuItem(true);
            var mockMenu2 = CreateMenuThatReturnsMenuItem(false);
            var menu = new LinkedMenu(new[] { fakeMenu1.Object, mockMenu2.Object });

            menu.Display();

            mockMenu2.Verify(m => m.Display());
        }

        [Test]
        public void Display_GivenMultipleMenusAndMoreChosenOnAllMenus_DisplayCalledAgainFirstMenu()
        {
            var mockMenu1 = CreateMenuThatReturnsMenuItem(true, false);
            var fakeMenu2 = CreateMenuThatReturnsMenuItem(true);
            var menu = new LinkedMenu(new[] { mockMenu1.Object, fakeMenu2.Object });

            menu.Display();

            mockMenu1.Verify(m => m.Display(), Times.Exactly(2));
        }

        private static Mock<ITextMenu> CreateMenuThatReturnsMenuItem(params bool[] isMore)
        {
            var menu = new Mock<ITextMenu>();
            menu.Setup(m => m.Display()).Returns(CreateMenuItem(isMore));
            return menu;
        }

        private static IMenuItem CreateMenuItem(params bool[] isMore)
        {
            var menuItem = new Mock<IMenuItem>();
            menuItem.Setup(mi => mi.IsMore).ReturnsInOrder(isMore);
            return menuItem.Object;
        }
    }
}