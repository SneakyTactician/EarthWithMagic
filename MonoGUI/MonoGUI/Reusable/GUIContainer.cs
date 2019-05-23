﻿using MagicalLifeAPI.Asset;
using MagicalLifeGUIWindows.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MagicalLifeGUIWindows.GUI.Reusable
{
    /// <summary>
    /// Contains other <see cref="GUIElement"/>s.
    /// All coordinates of <see cref="GUIElement"/> objects are relative to the position of this container.
    /// </summary>
    public abstract class GuiContainer
    {
        public GuiContainer Child { get; set; }

        /// <summary>
        /// If true, then this form is draggable.
        /// </summary>
        public bool IsMovable { get; set; }

        /// <summary>
        /// If true, this container is part of the HUD and shouldn't be cleared from the screen. Ever.
        /// </summary>
        public bool IsHUD { get; set; }

        /// <summary>
        /// Constructs a new instance of the <see cref="GUIContainer"/> class.
        /// Anything that implements this must have an empty constructor, which references the empty base constructor.
        /// </summary>
        /// <param name="image">The texture of this GUI container.</param>
        /// <param name="drawingBounds">The bounds for which to draw the texture on the screen at.</param>
        /// <param name="priority">Determines if this GUI container should have priority over other GUI elements when sorting through input.</param>
        /// <param name="isMovable">If true, then this GUI is movable.</param>
        protected GuiContainer(string image, Rectangle drawingBounds, bool isMovable)
        {
            this.Image = AssetManager.Textures[AssetManager.GetTextureIndex(image)];
            this.DrawingBounds = drawingBounds;
            this.Controls = new List<GUIElement>();
            this.Priority = RenderingData.GetGUIContainerPriority();
            this.IsMovable = isMovable;
        }

        /// <param name="isHUD">If true, this container is part of the HUD and shouldn't be cleared from the screen. Ever.</param>
        protected GuiContainer(string image, Rectangle drawingBounds, bool isMovable, bool isHUD)
            : this(image, drawingBounds, isMovable)
        {
            this.IsHUD = isHUD;
        }

        protected GuiContainer()
        {
        }

        /// <summary>
        /// The priority level of this <see cref="GuiContainer"/>
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// The area on the screen to draw the container at.
        /// </summary>
        public Rectangle DrawingBounds { get; set; }

        /// <summary>
        /// The visibility of this container.
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// The image of the container.
        /// Generally just a background for other GUIElements.
        /// </summary>
        public Texture2D Image { get; set; }

        /// <summary>
        /// The controls that are within this <see cref="GuiContainer"/>
        /// </summary>
        public List<GUIElement> Controls { get; set; }

        public abstract string GetTextureName();

        public void PopupChild(GuiContainer child)
        {
            this.Child = child;
        }
    }
}