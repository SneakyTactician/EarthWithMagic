﻿using MagicalLifeAPI.DataTypes;
using MagicalLifeAPI.Filing.Logging;
using MagicalLifeAPI.Registry.WorldGeneration;
using MagicalLifeAPI.Util;
using MagicalLifeAPI.World;
using MagicalLifeAPI.World.Data;
using MagicalLifeAPI.World.Generation;
using MagicalLifeMod.Core.Settings;
using System;
using System.Collections.Generic;

namespace MagicalLifeMod.Core.WorldGeneration
{
    /// <summary>
    /// Default control of how the world is generated.
    /// </summary>
    public class GenerationAllocator : DimensionGenerator
    {
        public GenerationAllocator()
        {
        }

        private ProtoArray<Chunk> GenerateTerrain(ProtoArray<Chunk> blankWorld, string dimensionName, Random seededRandom)
        {
            int[,] terrainGeneratorMap = this.AssignGenerators(blankWorld.Width, blankWorld.Height, seededRandom);

            MasterLog.DebugWriteLine("Organizing terrain");

            int done = 0;
            int toDo = blankWorld.Width * blankWorld.Height;

            //<Index of terrain generator to use, locations to use it at>
            Dictionary<int, List<Point2D>> generatorToAllocatedChunks = new Dictionary<int, List<Point2D>>();
            int width = blankWorld.Width;
            int height = blankWorld.Height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (generatorToAllocatedChunks.ContainsKey(terrainGeneratorMap[x, y]))
                    {
                        generatorToAllocatedChunks.TryGetValue(terrainGeneratorMap[x, y], out List<Point2D> value);
                        value.Add(new Point2D(x, y));
                    }
                    else
                    {
                        List<Point2D> firstLocation = new List<Point2D>
                        {
                            new Point2D(x, y)
                        };
                        generatorToAllocatedChunks.Add(terrainGeneratorMap[x, y], firstLocation);
                    }
                    done++;
                    MasterLog.DebugWriteLine(done.ToString() + "/" + toDo.ToString());
                }
            }

            MasterLog.DebugWriteLine("Done organizing terrain");

            MasterLog.DebugWriteLine("Generating terrain");
            done = 0;
            toDo = generatorToAllocatedChunks.Count;
            foreach (KeyValuePair<int, List<Point2D>> item in generatorToAllocatedChunks)
            {
                int length = item.Value.Count;
                Chunk[] toGenerator = new Chunk[length];
                //A reused Point2D.
                Point2D location;
                for (int i = 0; i < length; i++)
                {
                    location = item.Value[i];
                    toGenerator[i] = blankWorld[location.X, location.Y];
                }

                WorldGeneratorRegistry.TerrainGenerators[item.Key].GenerateTerrain(toGenerator, dimensionName, seededRandom);
                done++;
                MasterLog.DebugWriteLine(done.ToString() + "/" + toDo.ToString());
            }

            MasterLog.DebugWriteLine("Done generating terrain");
            return blankWorld;
        }

        /// <summary>
        /// This function determines for each chunk what terrain generator should generate it.
        /// What terrain generator neighboring chunks use has an impact upon surrounding chunks.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private int[,] AssignGenerators(int width, int height, Random seededRandom)
        {
            MasterLog.DebugWriteLine("Assigning generators");
            List<int> terrainWeights = new List<int>();

            foreach (TerrainGenerator item in WorldGeneratorRegistry.TerrainGenerators)
            {
                terrainWeights.Add(item.Weight);
            }

            WeightedRandom randomTerrainGenerators = new WeightedRandom(terrainWeights, seededRandom);

            int[,] terrainMap = new int[width, height];
            ArrayUtil.FillAll<int>(terrainMap, -1);

            int done = 0;
            int toDo = width * height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    terrainMap[x, y] = this.ChooseTerrainGenerator(terrainMap, randomTerrainGenerators, x, y, seededRandom);
                    done++;
                    MasterLog.DebugWriteLine(done.ToString() + "/" + toDo.ToString());
                }
            }

            MasterLog.DebugWriteLine("Done assigning generators");
            return terrainMap;
        }

        /// <summary>
        /// Choses which terrain generator to use for a single chunk.
        /// </summary>
        /// <returns></returns>
        private int ChooseTerrainGenerator(int[,] terrainMap, WeightedRandom randomTerrainGenerators, int x, int y, Random seededRandom)
        {
            //<terrainGenerator, weight>
            List<ValueTuple<int, int>> neighborGeneratorWeights = new List<(int, int)>();

            //The total weight of neighboring terrain generators.
            int totalWeight = 0;
            if (terrainMap[x, y] != -1)
            {
                int index = neighborGeneratorWeights.FindIndex(a => a.Item1 == terrainMap[x, y]);

                if (index == -1)
                {
                    neighborGeneratorWeights.Add((terrainMap[x, y], 1));
                }
                else
                {
                    neighborGeneratorWeights[index] = (neighborGeneratorWeights[index].Item1, neighborGeneratorWeights[index].Item2 + 1);
                }

                totalWeight += CoreSettingsHandler.GenerationSettings.Settings.NeighborWeight;
            }

            List<int> weights = new List<int>();
            foreach ((int, int) item in neighborGeneratorWeights)
            {
                int weightToAdd = item.Item2 * CoreSettingsHandler.GenerationSettings.Settings.NeighborWeight;
                weights.Add(weightToAdd);
            }

            if (totalWeight < 100)
            {
                //Add the chance for a new terrain generator to be used, not a neighboring one
                weights.Add(100 - totalWeight);
            }

            WeightedRandom neighborWeightedRandom = new WeightedRandom(weights, seededRandom);

            int terrainGeneratorIndex = neighborWeightedRandom.GetNext();

            if (terrainGeneratorIndex == weights.Count - 1)
            {
                //Use new terrain generator,
                return randomTerrainGenerators.GetNext();
            }
            else
            {
                //Use the chosen terrain generator.
                return neighborGeneratorWeights[terrainGeneratorIndex].Item1;
            }
        }

        protected override ProtoArray<Chunk> GenerateWorld(ProtoArray<Chunk> blankWorld, string dimensionName, Random seededRandom)
        {
            this.GenerateTerrain(blankWorld, dimensionName, seededRandom);

            //Let all the vegetation generators decide for themselves if they want to generate
            foreach (VegetationGenerator item in WorldGeneratorRegistry.VegetationGenerators)
            {
                item.GenerateVegetation(blankWorld.Data, dimensionName, seededRandom);
            }

            //Let all the structure generators decide for themselves if they want to generate
            foreach (StructureGenerator item in WorldGeneratorRegistry.StructureGenerators)
            {
                item.GenerateStructures(blankWorld.Data, dimensionName, seededRandom);
            }

            return blankWorld;
        }
    }
}