using System.Collections.Generic;

namespace ConsoleMenu
{
    public class Menu
    {
        public string InstructionalText { get; set; }
        public InstructionPosition InstructionPosition { get; set; }
        public IEnumerable<MenuItem> MenuItems { get; private set; }
        public bool CanBeCancelled { get; set; }

        public Menu()
            : this(string.Empty)
        { }

        public Menu(string instructionalText)
            : this(instructionalText, InstructionPosition.Below)
        { }

        public Menu(string instructionalText, InstructionPosition instructionPosition)
            : this(instructionalText, instructionPosition, new List<MenuItem>())
        { }

        public Menu(string instructionalText, InstructionPosition instructionPosition, IEnumerable<MenuItem> menuItems)
        {
            InstructionalText = instructionalText;
            InstructionPosition = instructionPosition;
            MenuItems = menuItems;
        }
    }

    public enum InstructionPosition
    {
        Above,
        Below
    }
}
