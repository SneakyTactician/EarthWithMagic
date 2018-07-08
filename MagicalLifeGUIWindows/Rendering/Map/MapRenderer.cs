﻿using MagicalLifeAPI.Asset;
using MagicalLifeAPI.Entities;
using MagicalLifeAPI.World;
using MagicalLifeAPI.World.Data;
using MagicalLifeAPI.World.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MagicalLifeGUIWindows.Rendering.Map
{
    /// <summary>
    /// Handles rendering the map.
    /// </summary>
    public static class MapRenderer
    {
        /// <summary>
        /// Draws the tiles that make up the map.
        /// </summary>
        /// <param name="spBatch"></param>
        public static void DrawMap(ref SpriteBatch spBatch, int dimension)
        {
            foreach (Tile tile in World.Dimensions[dimension])
            {
                Microsoft.Xna.Framework.Point start = new Microsoft.Xna.Framework.Point(RenderingPipe.tileSize.X * tile.Location.X, RenderingPipe.tileSize.Y * tile.Location.Y);
                DrawTile(tile, ref spBatch, start);
            }

            DrawEntities(ref spBatch, dimension);
        }

        public static void DrawEntities(ref SpriteBatch spBatch, int dimension)
        {
            int chunkHeight = Chunk.Height;
            int chunkWidth = Chunk.Width;
            int xSize = World.Dimensions[dimension].Width;
            int ySize = World.Dimensions[dimension].Height;

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    Chunk chunk = World.Dimensions[dimension].GetChunk(x, y);

                    foreach (Living item in chunk.Creatures)
                    {
                        if (item != null)
                        {
                            Texture2D livingTexture = AssetManager.Textures[AssetManager.GetTextureIndex(item.GetTextureName())];
                            Vector2 livingScreenLocation = new Vector2(item.ScreenLocation.X * Tile.GetTileSize().X, item.ScreenLocation.Y * Tile.GetTileSize().Y);
                            spBatch.Draw(livingTexture, livingScreenLocation, RenderingPipe.colorMask);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Draws a tile.
        /// </summary>
        private static void DrawTile(Tile tile, ref SpriteBatch spBatch, Point start)
        {
            Microsoft.Xna.Framework.Rectangle target = new Microsoft.Xna.Framework.Rectangle(start, RenderingPipe.tileSize);
            spBatch.Draw(AssetManager.Textures[tile.TextureIndex], target, RenderingPipe.colorMask);

            DrawStone(tile, ref spBatch, target);
        }

        /// <summary>
        /// Draws stone if it is present in the tile.
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="spBatch"></param>
        /// <param name="target"></param>
        private static void DrawStone(Tile tile, ref SpriteBatch spBatch, Rectangle target)
        {
            if (tile.Resources != null)
            {
                switch (tile.Resources)
                {
                    case StoneBase stone:
                        Texture2D stoneTexture = AssetManager.Textures[AssetManager.GetTextureIndex(stone.GetUnconnectedTexture())];
                        spBatch.Draw(stoneTexture, target, RenderingPipe.colorMask);
                        break;
                }
            }
        }
    }
}