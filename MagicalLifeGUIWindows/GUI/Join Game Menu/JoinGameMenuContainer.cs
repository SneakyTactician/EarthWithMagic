﻿using MagicalLifeAPI.Asset;
using MagicalLifeAPI.Components.Generic.Renderable;
using MagicalLifeGUIWindows.GUI.Reusable;
using MagicalLifeGUIWindows.Rendering;

namespace MagicalLifeGUIWindows.GUI.Join
{
    public class JoinGameMenuContainer : GUIContainer
    {
        public JoinButton JoinButton = new JoinButton();
        public IPInputBox IpInputBox = new IPInputBox(false);
        public PortInputBox PortInputBox = new PortInputBox(false);

        public JoinGameMenuContainer() : base(TextureLoader.GUIMenuBackground, RenderInfo.FullScreenWindow)
        {
            this.Controls.Add(this.JoinButton);
            this.Controls.Add(this.IpInputBox);
            this.Controls.Add(this.PortInputBox);
        }

        public override string GetTextureName()
        {
            return TextureLoader.GUIMenuBackground;
        }
    }
}