﻿using MagicalLifeAPI.Components.Generic.Renderable;
using MagicalLifeAPI.DataTypes;
using MagicalLifeAPI.World.Base;
using MagicalLifeAPI.World.Data;

namespace MagicalLifeGUIWindows
{
    /// <summary>
    /// Various utilities.
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Returns the in game tile coordinates of the specified Point2D on the screen.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Point2D GetMapLocation(int x, int y, int dimension, out bool success)
        {
            int x2 = x - RenderInfo.XViewOffset;
            int y2 = y - RenderInfo.YViewOffset;
            Point2D size = Tile.GetTileSize();

            x2 /= size.X;
            y2 /= size.Y;

            if (World.Dimensions.Count > 0 && World.Dimensions[dimension].DoesTileExist(x2, y2))
            {
                success = true;
                return new Point2D(x2, y2);
            }
            else
            {
                success = false;
                return null;
            }
        }
    }
}