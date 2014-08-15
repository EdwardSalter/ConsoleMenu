namespace ConsoleMenu
{
    public interface IMenuItem
    {
        string DisplayText { get; set; }
        bool IsDefault { get; set; }
        char Shortcut { get; set; }
        bool IsMore { get; set; }
    }
}
