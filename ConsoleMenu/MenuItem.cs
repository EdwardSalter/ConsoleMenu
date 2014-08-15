namespace ConsoleMenu
{
    public class MenuItem : IMenuItem
    {
        public char Shortcut { get; set; }
        public bool IsMore { get; set; }
        public string DisplayText { get; set; }
        public bool IsDefault { get; set; }

        public MenuItem()
        { }

        public MenuItem(char shortcut)
            : this(shortcut, string.Empty)
        { }

        public MenuItem(char shortcut, string displayText)
        {
            Shortcut = shortcut;
            DisplayText = displayText;
        }
    }
}