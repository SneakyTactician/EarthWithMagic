﻿using MagicalLifeAPI.Sound;
using MagicalLifeGUIWindows.GUI.Load_Game_Menu;
using MagicalLifeGUIWindows.GUI.Reusable;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Input.InputListeners;

namespace MagicalLifeGUIWindows.GUI.MainMenu.Buttons
{
    public class LoadGameButton : MonoButton
    {
        public LoadGameButton() : base("MenuButton", GetLocation(), true, "Load Game")
        {
        }

        public override void Click(MouseEventArgs e, GUIContainer container)
        {
            FMODUtil.RaiseEvent(EffectsTable.UIClick);
            LoadGameMenu.Initialize();
            MainMenu.MainMenuID.PopupChild(LoadGameMenu.Menu);
        }

        public override void DoubleClick(MouseEventArgs e, GUIContainer container)
        {
            //Single click is good enough
        }

        private static Rectangle GetLocation()
        {
            int width = MainMenuLayout.ButtonWidth;
            int height = MainMenuLayout.ButtonHeight;
            int x = MainMenuLayout.ButtonX;
            int y = MainMenuLayout.LoadGameButtonY;

            return new Rectangle(x, y, width, height);
        }
    }
}