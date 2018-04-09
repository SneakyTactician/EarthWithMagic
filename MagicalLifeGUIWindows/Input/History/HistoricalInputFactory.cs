﻿using MagicalLifeAPI.DataTypes;
using MagicalLifeAPI.GUI;
using MagicalLifeAPI.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicalLifeGUIWindows.Input.History
{
    /// <summary>
    /// Used to generate <see cref="HistoricalInput"/>.
    /// </summary>
    public class HistoricalInputFactory
    {
        public HistoricalInput Generate(InputEventArgs e)
        {
            //HistoricalInput history = new HistoricalInput()
            switch (e.MouseEventArgs.Button)
            {
                case MonoGame.Extended.Input.InputListeners.MouseButton.Left:
                    return this.SingleSelect(e);
                case MonoGame.Extended.Input.InputListeners.MouseButton.Right:
                    return this.Order(e);
                default:
                    return null;
            }
        }

        private HistoricalInput SingleSelect(InputEventArgs e)
        {
            Point mapSpot = Util.GetMapLocation(e.MouseEventArgs.Position.X, e.MouseEventArgs.Position.Y);
            Selectable select = World.mainWorld.Tiles[mapSpot.X, mapSpot.Y].Living;

            if (select != null)
            {
                //Null check select, as it is null when an entity is not found
                List<Selectable> selected = new List<Selectable>();
                selected.Add(select);

                if (e.ShiftDown)
                {
                    if (this.IsSelectableSelected(select))
                    {
                        return new HistoricalInput(false, selected);
                    }
                    else
                    {
                        return new HistoricalInput(selected);
                    }
                }
                else
                {
                    return new HistoricalInput(selected, true);
                }
            }
            else
            {
                return new HistoricalInput(true, null);
            }
        }

        /// <summary>
        /// Returns true if the <see cref="Selectable"/> is already selected.
        /// </summary>
        /// <param name="selectable"></param>
        /// <returns></returns>
        private bool IsSelectableSelected(Selectable selectable)
        {
            foreach (Selectable item in InputHistory.Selected)
            {
                if (selectable == item)
                {
                    return true;
                }
            }

            return false;
        }

        private HistoricalInput Order(InputEventArgs e)
        {
            Point screenLocation = e.MouseEventArgs.Position;
            Point mapLocation = Util.GetMapLocation(screenLocation.X, screenLocation.Y);
            return new HistoricalInput(mapLocation);
        }
    }
}