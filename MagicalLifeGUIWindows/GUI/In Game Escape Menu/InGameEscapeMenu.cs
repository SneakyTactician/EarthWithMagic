﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicalLifeGUIWindows.GUI.In_Game_Escape_Menu
{
    public static class InGameEscapeMenu
    {
        public static InGameEscapeMenuContainer menu;

        internal static void Initialize()
        {
            InGameEscapeMenuContainer mainMenu = new InGameEscapeMenuContainer();
            menu = mainMenu;
            MenuHandler.DisplayMenu(menu);
        }
    }
}