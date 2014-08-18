using System;

namespace ConsoleMenu
{
    internal class ConsoleMenuIOProvider : IMenuIOProvider
    {
        public void WriteMenuItem(char index, string choice)
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

        public void Clear()
        {
            Console.Clear();
        }

        public char ReadCharacter()
        {
            return Console.ReadKey(true).KeyChar;
        }
    }
}