using System;
using Moq;
using NUnit.Framework;

namespace ConsoleMenu.Tests
{
    public class MenuTests
    {
        [Test]
        public void Constructor_GivenANullListOfMenuItems_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new Menu("AnyString", null));
        }

        [Test]
        public void Display_FirstMenuItemIsDefault_SendsOneToInstructionPrintMethodAsDefaultValue()
        {
            var mockIoProvider = CreateMockIoProvider();
            var menuItems = new[] { new MenuItem("default") { IsDefault = true }, new MenuItem("non-default") };
            var menu = new Menu("SomeText", menuItems, mockIoProvider.Object);

            menu.Display();

            mockIoProvider.Verify(io => io.WriteInstructions(It.IsAny<string>(), 1));
        }

        [Test]
        public void Display_SecondMenuItemIsDefault_SendsTwoToInstructionPrintMethodAsDefaultValue()
        {
            var mockIoProvider = CreateMockIoProvider();
            var menuItems = new[] { new MenuItem("non-default"), new MenuItem("default") { IsDefault = true } };
            var menu = new Menu("SomeText", menuItems, mockIoProvider.Object);

            menu.Display();

            mockIoProvider.Verify(io => io.WriteInstructions(It.IsAny<string>(), 2));
        }

        [Test]
        public void Display_ThereAreNoMenuItems_Throws()
        {
            var mockIoProvider = CreateMockIoProvider();
            var menu = new Menu("SomeText", new IMenuItem[] { }, mockIoProvider.Object);

            Assert.Throws<InvalidOperationException>(() => menu.Display());
        }

        private static Mock<IMenuIOProvider> CreateMockIoProvider()
        {
            var mockIoProvider = new Mock<IMenuIOProvider>();
            mockIoProvider.Setup(io => io.ReadCharacter()).Returns('1');
            return mockIoProvider;
        }
    }
}