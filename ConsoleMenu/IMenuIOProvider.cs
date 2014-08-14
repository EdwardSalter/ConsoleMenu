namespace ConsoleMenu
{
    public interface IMenuIOProvider
    {
        void WriteMenuItem(char index, string choice);
        void WriteMore(int index);
        void WriteInstructions(string instructions, int? lastUsed);
        void Clear();
        char ReadCharacter();
    }
}
