using Moq;
using NUnit.Framework;

namespace ConsoleMenu.Tests
{
    public class TypedMenuTests
    {
        [Test]
        public void Display_GivenChoices_DisplaysInstructionalText()
        {
            var mockIo = new Mock<IMenuIOProvider>();
            mockIo.Setup(i => i.ReadCharacter()).Returns('1');
            const string instructionalText = "Instructions";
            var menu = new TypedMenu<string>(new[] { "one" }, x => x, null, instructionalText, mockIo.Object);

            menu.Display();

            mockIo.Verify(i => i.WriteInstructions(instructionalText, It.IsAny<int?>()));
        }
    }
}