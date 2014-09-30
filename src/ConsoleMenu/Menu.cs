using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleMenu
{
    public class Menu : ITextMenu
    {
        internal const int MaxOnScreen = 9;
        private readonly IMenuIOProvider m_io;
        private readonly IList<IMenuItem> m_menuItems;
        public string InstructionalText { get; set; }
        public InstructionPosition InstructionPosition { get; set; }

        public IEnumerable<IMenuItem> MenuItems
        {
            get { return m_menuItems; }
        }

        public bool CanBeCancelled { get; set; }


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
            m_menuItems = menuItems.ToList();
            m_io = ioProvider;
        }

        public IMenuItem Display()
        {
            if (!MenuItems.Any())
            {
                throw new InvalidOperationException("Cannot display menu as there are no menu items to display");
            }


            var displayed = MenuItems.ToList();


            // TODO: THROW IF 2 DISPLAYED MENU ITEMS HAVE SAME SHORTCUT


            int? lastUsed = displayed.FindIndex(mi => mi.IsDefault);
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
                    validKey = true;
                }
            }

            m_io.Clear();

            return chosenMenu;
        }

        public void AddMenuItem(IMenuItem menuItem)
        {
            m_menuItems.Add(menuItem);
        }
    }
}
