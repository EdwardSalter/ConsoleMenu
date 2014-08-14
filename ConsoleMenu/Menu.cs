using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ConsoleMenu
{
    public class Menu
    {
        internal const int MaxOnScreen = 9;
        private readonly IMenuIOProvider m_io;
        public string InstructionalText { get; set; }
        public InstructionPosition InstructionPosition { get; set; }
        public IEnumerable<IMenuItem> MenuItems { get; private set; }
        public bool CanBeCancelled { get; set; }

        public Menu()
            : this(string.Empty)
        { }

        public Menu(string instructionalText)
            : this(instructionalText, new List<MenuItem>())
        { }

        public Menu(string instructionalText, IEnumerable<IMenuItem> menuItems)
            : this(instructionalText, menuItems, new ConsoleMenuIOProvider())
        { }

        internal Menu(string instructionalText, IEnumerable<IMenuItem> menuItems, IMenuIOProvider ioProvider)
        {
            if (menuItems == null) throw new ArgumentNullException("menuItems");

            InstructionalText = instructionalText;
            InstructionPosition = InstructionPosition.Below;
            MenuItems = menuItems;
            m_io = ioProvider;
        }

        public int Display()
        {
            if (!MenuItems.Any())
            {
                throw new InvalidOperationException("Cannot display menu as there are no menu items to display");
            }

            return DisplayFrom(0);
        }

        private int DisplayFrom(int startIndex)
        {
            var choices = MenuItems.Skip(startIndex).ToList();

            var numberOfChoices = Math.Min(choices.Count, MaxOnScreen);
            var moreThanFits = choices.Count > MaxOnScreen;

            int? lastUsed = choices.FindIndex(mi => mi.IsDefault);
            if (lastUsed < 0)
            {
                lastUsed = null;
            }
            else
            {
                lastUsed++;
            }

            if (InstructionPosition == InstructionPosition.Above)
            {
                m_io.WriteInstructions(InstructionalText, lastUsed);
            }

            int currentIndex;
            for (currentIndex = 1; currentIndex <= numberOfChoices; currentIndex++)
            {
                var choice = choices[currentIndex - 1];
                m_io.WriteNumberedChoice(currentIndex, choice.DisplayText);
            }
            if (currentIndex > MaxOnScreen)
            {
                currentIndex = 0;
            }

            if (moreThanFits || startIndex > 0)
            {
                m_io.WriteMore(currentIndex);
            }

            if (InstructionPosition == InstructionPosition.Below)
            {
                m_io.WriteInstructions(InstructionalText, lastUsed);
            }

            int? chosen = null;
            bool validKey = false;
            while (!validKey)
            {
                var key = m_io.ReadCharacter();

                if (key == Environment.NewLine[0])
                {
                    if (lastUsed.HasValue)
                    {
                        chosen = lastUsed;
                        validKey = true;
                    }
                }
                else if ((moreThanFits || startIndex > 0) && key == currentIndex.ToString(CultureInfo.InvariantCulture)[0])
                {
                    m_io.Clear();
                    var newStart = moreThanFits ? MaxOnScreen + startIndex : 0;
                    return DisplayFrom(newStart);
                }
                else if (key >= '1' && key <= numberOfChoices.ToString(CultureInfo.InvariantCulture)[0])
                {
                    chosen = int.Parse(key.ToString(CultureInfo.InvariantCulture));
                    validKey = true;
                }
            }

            m_io.Clear();

            return startIndex + chosen.Value - 1;
        }
    }
}
