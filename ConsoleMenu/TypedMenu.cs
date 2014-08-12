using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ConsoleMenu
{
    public class TypedMenu<T>
    {
        private readonly IList<T> m_choices;
        private readonly Func<T, string> m_nameFunc;
        private readonly Func<T, bool> m_lastUsedFunc;
        private readonly string m_instructionalText;
        private readonly IMenuIOProvider m_io;

        public TypedMenu(IList<T> choices, Func<T, string> nameFunc, Func<T, bool> lastUsedFunc, string instructionalText)
            : this(choices, nameFunc, lastUsedFunc, instructionalText, new ConsoleMenuIOProvider())
        {
        }

        internal TypedMenu(IList<T> choices, Func<T, string> nameFunc, Func<T, bool> lastUsedFunc, string instructionalText, IMenuIOProvider io)
        {
            m_choices = choices;
            m_instructionalText = instructionalText;
            m_io = io;
            m_lastUsedFunc = lastUsedFunc;
            m_nameFunc = nameFunc;
        }

        // TODO: CANCELLABLE

        public T Display()
        {
            return DisplayFrom(0);
        }

        private T DisplayFrom(int startIndex)
        {
            var choices = m_choices.Skip(startIndex).ToList();

            const int maxOnScreen = 9;
            var numberOfChoices = Math.Min((int) choices.Count, maxOnScreen);
            var moreThanFits = choices.Count > maxOnScreen;

            int? lastUsed = null;
            int currentIndex;
            for (currentIndex = 1; currentIndex <= numberOfChoices; currentIndex++)
            {
                var choice = choices[currentIndex - 1];
                m_io.WriteNumberedChoice(currentIndex, m_nameFunc.Invoke(choice));

                if (m_lastUsedFunc.Invoke(choice))
                {
                    lastUsed = currentIndex;
                }
            }
            if (currentIndex > maxOnScreen)
            {
                currentIndex = 0;
            }

            if (moreThanFits || startIndex > 0)
            {
                m_io.WriteMore(currentIndex);
            }

            m_io.WriteInstructions(m_instructionalText, lastUsed);

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
                    var newStart = moreThanFits ? maxOnScreen + startIndex : 0;
                    return DisplayFrom(newStart);
                }
                else if (key >= '1' && key <= numberOfChoices.ToString(CultureInfo.InvariantCulture)[0])
                {
                    chosen = int.Parse(key.ToString(CultureInfo.InvariantCulture));
                    validKey = true;
                }
                else
                {
                    m_io.ClearLastInput();
                }
            }

            m_io.Clear();
            return m_choices[startIndex + chosen.Value - 1];
        }
    }
}
