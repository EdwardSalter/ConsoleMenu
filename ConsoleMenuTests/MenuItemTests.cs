using NUnit.Framework;

namespace ConsoleMenu.Tests
{
    public class MenuItemTests
    {
        [Test]
        public void Constructor_GivenAnEventHandler_AutomaticallySubscribesToTheSelectedEvent()
        {
            bool eventFired = false;
            var menuItem = new MenuItem("AnyString", (sender, args) => eventFired = true);

            menuItem.Select();

            Assert.IsTrue(eventFired);
        }

        [Test]
        public void Constructor_GivenANullEventHandler_DoesNotThrow()
        {
            var menuItem = new MenuItem("AnyString", null);
           
            Assert.DoesNotThrow(menuItem.Select);
        }

        [Test]
        public void Dispose_GivenEventFiredAfter_DoesNotPropagateEvent()
        {
            bool eventFired = false;
            var menuItem = new MenuItem("AnyString", (sender, args) => eventFired = true);

            menuItem.Dispose();
            menuItem.Select();

            Assert.IsFalse(eventFired);
        }
    }
}