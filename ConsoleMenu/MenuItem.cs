using System;

namespace ConsoleMenu
{
    public class MenuItem : IMenuItem, IDisposable
    {
        private readonly EventHandler m_onSelect;
        public event EventHandler Selected;
        public string DisplayText { get; set; }
        public bool IsDefault { get; set; }

        public MenuItem()
            : this(string.Empty)
        { }

        public MenuItem(string displayText)
            : this(displayText, null)
        { }

        public MenuItem(string displayText, EventHandler onSelect)
        {
            m_onSelect = onSelect;
            DisplayText = displayText;
            Selected += m_onSelect;
        }

        internal void Select()
        {
            if (Selected != null)
            {
                Selected.Invoke(this, EventArgs.Empty);
            }
        }

        public void Dispose()
        {
            if (m_onSelect != null)
            {
                Selected -= m_onSelect;
            }
        }
    }
}