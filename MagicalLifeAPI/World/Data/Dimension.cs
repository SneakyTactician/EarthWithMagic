﻿using MagicalLifeAPI.DataTypes;
using MagicalLifeAPI.Pathfinding;
using MagicalLifeAPI.Universal;
using ProtoBuf;
using System.Collections.Generic;

namespace MagicalLifeAPI.World.Data
{
    /// <summary>
    /// Holds some information about the level of the world.
    /// Could be a dungeon, the starting point, or some other thing.
    /// </summary>
    [ProtoContract]
    public class Dimension : Unique
    {
        /// <summary>
        /// Handles access to the chunks stored in this dimension.
        /// </summary>
        [ProtoMember(1)]
        private ChunkManager Manager;

        /// <summary>
        /// The display name of the dimension.
        /// </summary>
        [ProtoMember(2)]
        public string DimensionName { get; set; }

        /// <summary>
        /// The width of this dimension in chunks.
        /// </summary>
        public int Width
        {
            get
            {
                return this.Manager.Width;
            }
        }

        /// <summary>
        /// The height of this dimension in chunks.
        /// </summary>
        public int Height
        {
            get
            {
                return this.Manager.Height;
            }
        }

        public Tile this[int x, int y]
        {
            get
            {
                return this.Manager[x, y];
            }
            set
            {
                this.Manager[x, y] = value;
            }
        }

        public Dimension(string dimensionName, ProtoArray<Chunk> chunks)
        {
            this.Manager = new ChunkManager(this.ID, chunks);
            this.DimensionName = dimensionName;
            World.Storage.PrepareForDimension(this.ID);

            
            int dimensionID = World.AddDimension(this);

            //Anything that needs a dimensionID
        }

        public Dimension()
        {
        }

        public Chunk GetChunkForLocation(int x, int y)
        {
            return this.Manager.GetChunkForLocation(x, y);
        }

        /// <summary>
        /// Returns a chunk that correspondences with the specified chunk coordinate.
        /// </summary>
        /// <param name="chunkX"></param>
        /// <param name="chunkY"></param>
        /// <returns></returns>
        public Chunk GetChunk(int chunkX, int chunkY)
        {
            return this.Manager.GetChunk(chunkX, chunkY);
        }

        /// <summary>
        /// Determines if the specified tile exists, without loading the tile.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool DoesTileExist(int x, int y)
        {
            return this.Manager.DoesTileExist(x, y);
        }

        public IEnumerator<Tile> GetEnumerator()
        {
            foreach (Tile item in this.Manager)
            {
                yield return item;
            }
        }
    }
}