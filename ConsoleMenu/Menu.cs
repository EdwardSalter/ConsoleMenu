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
            var displayed = choices.Take(MaxOnScreen).ToList();

            var numberOfChoices = Math.Min(choices.Count, MaxOnScreen);
            var moreThanFits = choices.Count > MaxOnScreen;

            int? lastUsed = choices.FindIndex(mi => mi.IsDefault);
            if (lastUsed < 0)
            {
                lastUsed = null;
            }

            if (InstructionPosition == InstructionPosition.Above)
            {
                m_io.WriteInstructions(InstructionalText, lastUsed == null ? null : lastUsed + 1);
            }

            int currentIndex;
            for (currentIndex = 1; currentIndex <= numberOfChoices; currentIndex++)
            {
                var choice = choices[currentIndex - 1];
                // TODO: DON'T DO THIS HERE AND TEST OTHER KEYS
                choice.Shortcut = currentIndex.ToString()[0];
                // TODO: THIS SHOULD ALREADY BE DONE, MOVE TO A FORMATTER
                m_io.WriteMenuItem(choice.Shortcut, choice.DisplayText);
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
                m_io.WriteInstructions(InstructionalText, lastUsed == null ? null : lastUsed + 1);
            }

            int? chosen = null;
            bool validKey = false;
            while (!validKey)
            {
                var key = m_io.ReadCharacter();
                var chosenMenu = displayed.FirstOrDefault(menu => key == menu.Shortcut);

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
                else if (chosenMenu != null)
                {
                    chosen = MenuItems.ToList().IndexOf(chosenMenu);
                    validKey = true;
                }
            }

            m_io.Clear();

            return chosen.Value;
        }
    }
}
