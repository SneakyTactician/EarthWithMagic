﻿using MagicalLifeAPI.Asset;
using MagicalLifeAPI.DataTypes;
using MagicalLifeGUIWindows.GUI.Reusable.API;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MagicalLifeGUIWindows.GUI.Reusable.Collections
{
    /// <summary>
    /// A scrollable grid element.
    /// </summary>
    public class MonoGenericGrid<T> : GUIElement, IScrollable
        where T : GUIElement
    {
        private int ItemBackgroundTexture { get; set; }

        /// <summary>
        /// The items that are displayed in this <see cref="ListBox"/>.
        /// The elements within the list are each row.
        /// </summary>
        public List<T[]> Items { get; set; }

        /// <summary>
        /// How many items should be displayed at any given time.
        /// </summary>
        private int ItemRenderCount { get; set; }

        /// <summary>
        /// The index of the first item in <see cref="this.Items"/> that is being displayed.
        /// </summary>
        private int FirstItemIndex { get; set; }

        /// <summary>
        /// The index of the selected item.
        /// Is -1 when no item is selected.
        /// </summary>
        public int SelectedIndex { get; private set; } = -1;

        /// <summary>
        /// The number of columns in this grid.
        /// </summary>
        public int Columns { get; private set; }

        /// <summary>
        /// The size of all items that will be rendered in this collection.
        /// </summary>
        public Point2D ItemDisplayBounds { get; private set; }

        /// <summary>
        /// Raised whenever there is a click in this <see cref="ListBox"/>, and has a parameter of what the index of the item that was clicked on is.
        /// </summary>
        public event EventHandler<int> ItemClick;

        /// <summary>
        /// Raised whenever there is a double click in this <see cref="ListBox"/>, and has a parameter of what the index of the item that was double clicked on is.
        /// </summary>
        public event EventHandler<int> ItemDoubleClick;

        /// <param name="itemRenderCount">How many items should be displayed at any given time.</param>
        /// <param name="items">The items that will be displayed.</param>
        /// <param name="displayBounds">The size of each item that will be displayed. Any items that are not this size will cause errors to occur in rendering.</param>
        public MonoGenericGrid(Point2D displayBounds, Rectangle drawingBounds, int priority, bool isContained, string font, int itemRenderCount)
            : base(drawingBounds, priority, isContained, font)
        {
            this.InitializeItems();
            this.ItemDisplayBounds = displayBounds;

            this.ItemBackgroundTexture = AssetManager.GetTextureIndex(TextureLoader.GUIListBoxItemBackground);
            this.ItemRenderCount = itemRenderCount;

            this.ClickEvent += this.ScrollableGrid_ClickEvent;
            this.DoubleClickEvent += this.MonoGenericGrid_DoubleClickEvent;
        }

        private void MonoGenericGrid_DoubleClickEvent(object sender, Event.ClickEventArgs e)
        {
            this.SelectedIndex = ((e.MouseEventArgs.Position.Y + e.GUIContainer.DrawingBounds.Y) / this.ItemDisplayBounds.Y) - 1;
            this.ItemDoubleClickHandler(this.SelectedIndex);
        }

        private void ScrollableGrid_ClickEvent(object sender, Event.ClickEventArgs e)
        {
            this.SelectedIndex = ((e.MouseEventArgs.Position.Y + e.GUIContainer.DrawingBounds.Y) / this.ItemDisplayBounds.Y) - 1;
            this.ItemClickHandler(this.SelectedIndex);
        }

        private void InitializeItems()
        {
            this.Items = new List<T[]>();
        }

        /// <summary>
        /// Calculates the height and width of each item to be displayed in this <see cref="ListBox"/>.
        /// </summary>
        /// <returns></returns>
        private Point CalculateItemBounds()
        {
            int width = this.DrawingBounds.Width / this.Columns;
            int height = this.DrawingBounds.Height / this.ItemRenderCount;

            return new Point(width, height);
        }

        public override void Render(SpriteBatch spBatch, Rectangle containerBounds)
        {
            int x = containerBounds.X + this.DrawingBounds.X;
            int y = containerBounds.Y + this.DrawingBounds.Y;

            int length;

            //How many items to display in our given bounds
            if (this.ItemRenderCount <= this.Items.Count)
            {
                length = this.ItemRenderCount;
            }
            else
            {
                length = this.Items.Count;
            }

            //Display each row
            for (int i = 0; i < length; i++)
            {
                //Draw the background
                Color colorMask;

                if (i == this.SelectedIndex)
                {
                    colorMask = new Color(255, 255, 255, 30);
                }
                else
                {
                    colorMask = Color.White;
                }

                //Have the item draw itself
                int dynamicLength = this.Items[i].Length;
                for (int ii = 0; ii < dynamicLength; ii++)
                {
                    Rectangle target = new Rectangle(new Point(x + (ii * this.ItemDisplayBounds.X), y), this.ItemDisplayBounds);
                    spBatch.Draw(AssetManager.Textures[this.ItemBackgroundTexture], target, colorMask);
                    this.Items[this.FirstItemIndex + i][ii].Render(spBatch, target);
                }
                y += this.ItemDisplayBounds.Y;
            }
        }

        public void Forward()
        {
            //Forward is in this case down

            if (this.ItemRenderCount + this.FirstItemIndex < this.Items.Count)
            {
                this.FirstItemIndex++;
            }
        }

        public void Backwards()
        {
            //Backwards is in this case up

            if (this.FirstItemIndex > 0)
            {
                this.FirstItemIndex--;
            }
        }

        /// <summary>
        /// Adds an item to this grid.
        /// </summary>
        public void Add(params T[] elements)
        {
            if (elements.Length > this.Columns)
            {
                this.Columns = elements.Length;
            }

            this.Items.Add(elements);
        }

        /// <summary>
        /// Raises the dimension added event.
        /// </summary>
        /// <param name="e"></param>
        private void ItemClickHandler(int e)
        {
            this.ItemClick?.Invoke(this, e);
        }

        private void ItemDoubleClickHandler(int e)
        {
            this.ItemDoubleClick?.Invoke(this, e);
        }
    }
}