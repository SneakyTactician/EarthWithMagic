﻿using MagicalLifeAPI.Asset;
using MagicalLifeAPI.Components.Generic.Renderable;
using MagicalLifeAPI.Components.Resource;
using MagicalLifeAPI.DataTypes;
using MagicalLifeAPI.Properties;
using MagicalLifeAPI.Util;
using MagicalLifeAPI.World.Base;
using ProtoBuf;

namespace MagicalLifeAPI.World.Tiles
{
    [ProtoContract]
    public class Grass : Tile
    {
        public static readonly string GrassTileName = Lang.Grass;

        public Grass(Point2D location, int dimension) : base(location, dimension, 11, 1)
        {
            this.InitializeComponents();
        }

        private void InitializeComponents()
        {
            ComponentTillable tillingBehavior = new TillablePercentDone();
            ComponentRenderer renderer = this.GetExactComponent<ComponentRenderer>();
            this.AddComponent(tillingBehavior);

            renderer.AddVisual(new StaticTexture(AssetManager.GetTextureIndex(this.GetRandomDirtTexture()), RenderLayer.DirtBase));
            renderer.AddVisual(new StaticTexture(AssetManager.GetTextureIndex(this.GetRandomGrassTexture()), RenderLayer.GrassBase));
        }

        public Grass(int x, int y, int dimension) : this(new Point2D(x, y), dimension)
        {
        }

        public Grass()
        {
            //Protobuf-net constructor
        }

        private string GetRandomGrassTexture()
        {
            switch (StaticRandom.Rand(0, 4))
            {
                case 1:
                    return TextureLoader.TextureGrass2;

                case 2:
                    return TextureLoader.TextureGrass3;

                case 3:
                    return TextureLoader.TextureGrass4;

                default:
                    return TextureLoader.TextureGrass1;
            }
        }

        private string GetRandomDirtTexture()
        {
            int r = StaticRandom.Rand(0, 2);

            if (r == 0)
            {
                return TextureLoader.TextureDirt1;
            }
            else
            {
                return TextureLoader.TextureDirt2;
            }
        }

        public override string GetName()
        {
            return GrassTileName;
        }
    }
}