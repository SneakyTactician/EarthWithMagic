﻿using MagicalLifeAPI.Asset;
using MagicalLifeAPI.Sound;
using MagicalLifeGUIWindows.GUI.Reusable;
using MagicalLifeGUIWindows.GUI.Save;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Input.InputListeners;

namespace MagicalLifeGUIWindows.GUI.In
{
    public class SaveButton : MonoButton
    {
        public SaveButton() : base(TextureLoader.GUIMenuButton, GetDisplayArea(), true, "Save Game")
        {
        }

        private static Rectangle GetDisplayArea()
        {
            int x = InGameEscapeMenuLayout.ButtonX;
            int y = InGameEscapeMenuLayout.SaveButtonY;
            int width = InGameEscapeMenuLayout.ButtonWidth;
            int height = InGameEscapeMenuLayout.ButtonHeight;

            return new Rectangle(x, y, width, height);
        }

        public override void Click(MouseEventArgs e, GUIContainer container)
        {
            this.Save();
        }

        private void Save()
        {
            FMODUtil.RaiseEvent(SoundsTable.UIClick);
            SaveGameMenu.Initialize();
            InGameEscapeMenu.menu.PopupChild(SaveGameMenu.menu);
        }

        public override void DoubleClick(MouseEventArgs e, GUIContainer container)
        {
            this.Save();
        }
    }
}