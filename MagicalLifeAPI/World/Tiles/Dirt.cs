﻿using MagicalLifeAPI.Asset;
using MagicalLifeAPI.Components.Generic.Renderable;
using MagicalLifeAPI.DataTypes;
using MagicalLifeAPI.Util;
using MagicalLifeAPI.World.Base;

namespace MagicalLifeAPI.World.Tiles
{
    /// <summary>
    /// A dirt tile.
    /// </summary>
    [ProtoBuf.ProtoContract]
    public class Dirt : Tile
    {
        public override ComponentRenderer CompositeRenderer { get; set; }

        public Dirt(Point2D location) : base(location, 10, 0)
        {
            this.CompositeRenderer = new ComponentRenderer();
            this.CompositeRenderer.RenderQueue.Visuals.Add(new StaticTexture(Dirt.GetTextureID(), RenderLayer.DirtBase));
        }

        public Dirt(int x, int y) : this(new Point2D(x, y))
        {
        }

        public Dirt() : base()
        {
        }

        protected static ComponentRenderer GetRenderer()
        {
            ComponentRenderer renderer = new ComponentRenderer();
            StaticTexture texture = new StaticTexture(GetTextureID(), RenderLayer.DirtBase);

            renderer.RenderQueue.Visuals.Add(texture);
            return renderer;
        }

        public override string GetName()
        {
            return "Dirt";
        }

        public static int GetTextureID()
        {
            return AssetManager.GetTextureIndex(GetRandomDirtTexture());
        }

        private static string GetRandomDirtTexture()
        {
            int r = StaticRandom.Rand(0, 2);
            string ret;

            if (r == 0)
            {
                ret = TextureLoader.TextureDirt1;
            }
            else
            {
                ret = TextureLoader.TextureDirt2;
            }

            return ret;
        }
    }
}