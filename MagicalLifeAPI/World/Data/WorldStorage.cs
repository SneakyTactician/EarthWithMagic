﻿using MagicalLifeAPI.DataTypes;
using MagicalLifeAPI.Filing;
using MagicalLifeAPI.Networking.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MagicalLifeAPI.World.Data
{
    /// <summary>
    /// Knows how to load and save data from every dimension in the world.
    /// </summary>
    public class WorldStorage
    {
        /// <summary>
        /// The name of the save game.
        /// </summary>
        private readonly string SaveName;

        private readonly string GameSaveRoot;

        /// <summary>
        /// Contains the path to the root of each dimension directory, where the chunk files go.
        /// Key: The ID of the dimension.
        /// Value: The path to the root of where all of the chunks are stored for the dimension.
        /// </summary>
        private readonly Dictionary<Guid, string> DimensionPaths = new Dictionary<Guid, string>();

        public WorldStorage(string saveName)
        {
            this.SaveName = saveName;

            DirectoryInfo gameSavePath = Directory.CreateDirectory(FileSystemManager.SaveDirectory + Path.DirectorySeparatorChar + this.SaveName);
            this.GameSaveRoot = gameSavePath.FullName;
        }

        /// <summary>
        /// Creates folders for a new dimension.
        /// </summary>
        public void PrepareForDimension(Guid dimensionID)
        {
            DirectoryInfo info = Directory.CreateDirectory(this.GameSaveRoot + Path.DirectorySeparatorChar + dimensionID);
            this.DimensionPaths.Add(dimensionID, info.FullName);
        }

        /// <summary>
        /// Saves a chunk to disk.
        /// </summary>
        /// <param name="chunk">The chunk to save.</param>
        /// <param name="dimensionID">The ID of the dimension the chunk belongs to.</param>
        public void SaveChunk(Chunk chunk, Guid dimensionID)
        {
            bool dimensionExists = this.DimensionPaths.TryGetValue(dimensionID, out string path);

            if (!dimensionExists)
            {
                throw new DirectoryNotFoundException("Dimension save folder does not exist!");
            }

            using (FileStream fs = File.Create(path + chunk.ChunkLocation.ToString() + ".chunk"))
            {
                string serialized = Convert.ToBase64String(ProtoUtil.Serialize<Chunk>(chunk));

                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteAsync(serialized);
                }
            }
        }

        /// <summary>
        /// Loads a chunk from disk.
        /// </summary>
        /// <param name="chunkX">The x position of the chunk within the dimension.</param>
        /// <param name="chunkY">The y position of the chunk within the dimension.</param>
        /// <param name="dimensionID">The ID of the dimension that the chunk belongs to.</param>
        /// <returns></returns>
        public Chunk LoadChunk(int chunkX, int chunkY, Guid dimensionID)
        {
            return this.LoadChunk(new Point2D(chunkX, chunkY), dimensionID);
        }

        /// <summary>
        /// Loads a chunk from disk.
        /// </summary>
        /// <param name="chunkX">The x position of the chunk within the dimension.</param>
        /// <param name="chunkY">The y position of the chunk within the dimension.</param>
        /// <param name="dimensionID">The ID of the dimension that the chunk belongs to.</param>
        /// <returns></returns>
        public Chunk LoadChunk(Point2D chunkLocation, Guid dimensionID)
        {
            bool dimensionExists = this.DimensionPaths.TryGetValue(dimensionID, out string path);

            if (!dimensionExists)
            {
                throw new DirectoryNotFoundException("Dimension save folder does not exist!");
            }

            using (StreamReader sr = new StreamReader(path + chunkLocation.ToString() + ".chunk"))
            {
                Task<string> serialized = sr.ReadToEndAsync();
                return ProtoUtil.Deserialize<Chunk>(serialized.Result);
            }
        }
    }
}