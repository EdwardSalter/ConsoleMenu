using System;

namespace ConsoleMenu
{
    // ReSharper disable once InconsistentNaming
    internal class ConsoleMenuIOProvider : IMenuIOProvider
    {
        public void WriteNumberedChoice(int index, string choice)
        {
            Console.WriteLine("{0}. {1}", index, choice);
        }

        public void WriteMore(int index)
        {
            Console.WriteLine("{0}. -- More --", index);
        }

        public void WriteInstructions(string instructions, int? lastUsed)
        {
            Console.Write("{0}{1}: ", instructions, lastUsed == null ? "" : " (" + lastUsed + ")");
        }

        public void ClearLastInput()
        {
            Console.Write("\b\0\b");
        }

        public void Clear()
        {
            Console.Clear();
        }

        public char ReadCharacter()
        {
            return Console.ReadKey().KeyChar;
        }
    }
}