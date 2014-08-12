namespace ConsoleMenu
{
    // ReSharper disable once InconsistentNaming
    public interface IMenuIOProvider
    {
        void WriteNumberedChoice(int index, string choice);
        void WriteMore(int index);
        void WriteInstructions(string instructions, int? lastUsed);
        void Clear();
        char ReadCharacter();
    }
}
