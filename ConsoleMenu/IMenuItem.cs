using System;

namespace ConsoleMenu
{
    public interface IMenuItem
    {
        event EventHandler Selected;
        string DisplayText { get; set; }
        bool IsDefault { get; set; }
    }
}
