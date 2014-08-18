using System.Collections.Generic;

namespace ConsoleMenu
{
    public interface ITextMenu : IMenu
    {
        string InstructionalText { get; set; }
        InstructionPosition InstructionPosition { get; set; }
        IEnumerable<IMenuItem> MenuItems { get; }
        bool CanBeCancelled { get; set; }
    }
}