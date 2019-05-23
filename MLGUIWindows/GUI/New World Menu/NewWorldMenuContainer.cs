﻿using MagicalLifeAPI.Asset;
using MagicalLifeAPI.Components.Generic.Renderable;
using MagicalLifeGUIWindows.GUI.Reusable;

namespace MagicalLifeGUIWindows.GUI.New
{
    /// <summary>
    /// The menu that pops up when the user creates a new world.
    /// </summary>
    public class NewWorldMenuContainer : GuiContainer
    {
        public WorldWidthInputBox WorldWidth { get; } = new WorldWidthInputBox(false);
        public WorldLengthInputBox WorldLength { get; } = new WorldLengthInputBox(false);
        public NewWorldNextButton NextButton { get; } = new NewWorldNextButton();
        public LengthLabel LengthLabel { get; } = new LengthLabel();
        public WidthLabel WidthLabel { get; } = new WidthLabel();
        public MonoInputBox GameName { get; private set; }

        public NewWorldMenuContainer(bool visible) : base(TextureLoader.GUIMenuBackground, RenderInfo.FullScreenWindow, false)
        {
            this.Visible = visible;

            this.GameName = new MonoInputBox(TextureLoader.GUIInputBox100x50, TextureLoader.GUICursorCarrot, NewWorldMenuLayout.GameNameInputBox,
      int.MaxValue, TextureLoader.FontMainMenuFont12x, false,
      Rendering.Text.SimpleTextRenderer.Alignment.Left, true);

            this.Controls.Add(this.WorldWidth);
            this.Controls.Add(this.WorldLength);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.LengthLabel);
            this.Controls.Add(this.WidthLabel);
            this.Controls.Add(this.GameName);
        }

        public NewWorldMenuContainer() : base()
        {
        }

        public override string GetTextureName()
        {
            return TextureLoader.GUIMenuBackground;
        }
    }
}