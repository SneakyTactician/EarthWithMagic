﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicalLifeGUIWindows.GUI.Load_Game_Menu
{
    public static class LoadGameMenu
    {
        public static LoadGameMenuContainer menu;

        internal static void Initialize()
        {
            LoadGameMenuContainer mainMenu = new LoadGameMenuContainer();
            menu = mainMenu;
            MenuHandler.DisplayMenu(menu);
        }
    }
}