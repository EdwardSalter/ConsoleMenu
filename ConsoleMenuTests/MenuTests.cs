// 
// Copyright (C) 2014 Weatherford International Ltd
// Gotham Road, East Leake, Loughborough, Leicestershire, LE12 6JX, U.K
// 
// Author: Edward Salter              Date: 2014/08/08
// 
// 
// $Id:  $
// 
// $Log: $

using ConsoleMenu;
using Moq;
using NUnit.Framework;

namespace ConsoleMenuTests
{
    public class MenuTests
    {
        [Test]
        public void Display_GivenChoices_DisplaysInstructionalText()
        {
            var mockIo = new Mock<IMenuIOProvider>();
            mockIo.Setup(i => i.ReadCharacter()).Returns('1');
            const string instructionalText = "Instructions";
            var menu = new Menu<string>(new[] { "one" }, x => x, s => false, instructionalText, mockIo.Object);

            menu.Display();

            mockIo.Verify(i => i.WriteInstructions(instructionalText, It.IsAny<int?>()));
        }
    }
}