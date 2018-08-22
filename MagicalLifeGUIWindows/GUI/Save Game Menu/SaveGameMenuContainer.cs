﻿using MagicalLifeGUIWindows.GUI.Reusable;
using MagicalLifeGUIWindows.GUI.Save_Game_Menu.Buttons;
using MagicalLifeGUIWindows.GUI.Save_Game_Menu.InputBoxes;
using MagicalLifeGUIWindows.GUI.Save_Game_Menu.ListBoxes;
using MagicalLifeGUIWindows.Rendering;

namespace MagicalLifeGUIWindows.GUI.Save_Game_Menu
{
    public class SaveGameMenuContainer : GUIContainer
    {
        public OverwriteSaveListBox SavesList { get; private set; } = new OverwriteSaveListBox();

        public OverwriteButton OverwriteButton { get; private set; } = new OverwriteButton();

        public NewSaveInputBox SaveInputBox { get; private set; } = new NewSaveInputBox();

        public NewSaveButton NewButton { get; private set; } = new NewSaveButton();

        public SaveGameMenuContainer() : base("MenuBackground", RenderingPipe.FullScreenWindow)
        {
            this.Controls.Add(this.SavesList);
            this.Controls.Add(this.OverwriteButton);
            this.Controls.Add(this.SaveInputBox);
            this.Controls.Add(this.NewButton);
        }

        public override string GetTextureName()
        {
            return "MenuBackground";
        }
    }
}