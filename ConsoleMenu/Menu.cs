using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleMenu
{
    public class Menu : IMenu
    {
        internal const int MaxOnScreen = 9;
        private readonly IMenuIOProvider m_io;
        public string InstructionalText { get; set; }
        public InstructionPosition InstructionPosition { get; set; }
        public IEnumerable<IMenuItem> MenuItems { get; private set; }
        public bool CanBeCancelled { get; set; }

        private IEnumerable<IMenuItem> RealChoices
        {
            get { return MenuItems.Where(menu => !menu.IsMore); }
        }

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

        public IMenuItem Display()
        {
            if (!MenuItems.Any())
            {
                throw new InvalidOperationException("Cannot display menu as there are no menu items to display");
            }

            return DisplayFrom(0);
        }

        private IMenuItem DisplayFrom(int startIndex)
        {
            var choices = MenuItems.Skip(startIndex).ToList();
            var displayed = choices.Take(MaxOnScreen + 1).ToList();

            bool allDisplayed = !choices.Skip(MaxOnScreen).Any();

            // TODO: THROW IF 2 DISPLAYED MENU ITEMS HAVE SAME SHORTCUT


            int? lastUsed = choices.FindIndex(mi => mi.IsDefault);
            if (lastUsed < 0)
            {
                lastUsed = null;
            }

            if (InstructionPosition == InstructionPosition.Above)
            {
                m_io.WriteInstructions(InstructionalText, lastUsed == null ? null : lastUsed + 1);
            }


            foreach (var menuItem in displayed)
            {
                m_io.WriteMenuItem(menuItem.Shortcut, menuItem.DisplayText);
            }

            if (InstructionPosition == InstructionPosition.Below)
            {
                m_io.WriteInstructions(InstructionalText, lastUsed == null ? null : lastUsed + 1);
            }

            IMenuItem chosenMenu = null;
            bool validKey = false;
            while (!validKey)
            {
                var key = m_io.ReadCharacter();
                chosenMenu = displayed.FirstOrDefault(menu => key == menu.Shortcut);

                if (key == Environment.NewLine[0])
                {
                    if (lastUsed.HasValue)
                    {
                        chosenMenu = MenuItems.ElementAt(lastUsed.Value);
                        validKey = true;
                    }
                }
                else if (chosenMenu != null)
                {
                    if (chosenMenu.IsMore)
                    {
                        m_io.Clear();
                        var newStart = !allDisplayed ? MaxOnScreen + startIndex + 1: 0;
                        return DisplayFrom(newStart);
                    }

                    validKey = true;
                }
            }

            m_io.Clear();

            return chosenMenu;
        }
    }
}
