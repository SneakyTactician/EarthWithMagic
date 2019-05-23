﻿using MagicalLifeAPI.Asset;
using MagicalLifeAPI.Components.Generic.Renderable;
using MagicalLifeGUIWindows.GUI.Reusable;
using MagicalLifeGUIWindows.GUI.Settings_Menu.Buttons;
using MagicalLifeGUIWindows.GUI.Settings_Menu.InputBoxes;
using MagicalLifeGUIWindows.GUI.Settings_Menu.Labels;

namespace MagicalLifeGUIWindows.GUI.Settings_Menu
{
    public class SettingsGameMenuContainer : GuiContainer
    {
        public SettingsGameMenuContainer(bool fromMainMenu) : base(TextureLoader.GUIMenuBackground, RenderInfo.FullScreenWindow, false)
        {
            this.Controls.Add(new MainMenuButton(fromMainMenu));
            this.Controls.Add(new MasterVolumeInputBox());
            this.Controls.Add(new MasterVolumeLabel());
        }

        public override string GetTextureName()
        {
            return TextureLoader.GUIMenuBackground;
        }
    }
}