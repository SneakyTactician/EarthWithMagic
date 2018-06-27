﻿using MagicalLifeAPI.Entities;
using MagicalLifeAPI.Entities.Humanoid;
using MagicalLifeAPI.Filing.Logging;
using MagicalLifeAPI.Load;
using MagicalLifeAPI.Networking;
using MagicalLifeAPI.Networking.External_Type_Serialization;
using MagicalLifeAPI.Protobuf.Serialization;
using MagicalLifeAPI.Universal;
using MagicalLifeAPI.World.Data;
using MagicalLifeNetworking.Client;
using MagicalLifeServer;
using MagicalLifeServer.Networking;
using MagicalLifeServer.ServerWorld.World_Generation.Generators;
using MagicalLifeServerShell.API.Settings;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MagicalLifeServerShell.API.Commands
{
    /// <summary>
    /// Creates a new world and launches a new game.
    /// </summary>
    public class NewGame : ICommand
    {
        public string getHelp()
        {
            return "Creates a new world and hosts a new game";
        }

        public string getName()
        {
            return "NewGame";
        }

        public void run(List<string> input)
        {
            Server.Load();

            WorldGenerationSettings wset = SettingsHandler.WorldGenerationSettings.GetSettings();
            Util.WriteLine("Generating world!");
            World.Initialize(wset.DimensionWidth, wset.DimensionHeight, new StoneSprinkle(0));
            Util.WriteLine("World generated!");

            Util.WriteLine("Initializing networking!");
            int port = SettingsHandler.NetworkSettings.GetSettings().Port;
            ServerSendRecieve.Initialize(new NetworkSettings(port));

            Util.WriteLine("Done!");
            Server.StartGame();
            Util.WriteLine("Game started!");
            //string world = MagicalLifeAPI.Protobuf.Serialization.ProtoUtil.Serialize(World.Dimensions);
            Point point = new Point(3, 1);
            MagicalLifeAPI.World.Tiles.Dirt test = new MagicalLifeAPI.World.Tiles.Dirt(3, 1);
            string te = test.Location.ToString();
            MasterLog.DebugWriteLine("Test" + te);
            string world = MagicalLifeAPI.Protobuf.Serialization.ProtoUtil.Serialize<MagicalLifeAPI.World.Tiles.Dirt>(test);
            MagicalLifeAPI.World.Tiles.Dirt dirt = ProtoUtil.Deserialize<MagicalLifeAPI.World.Tiles.Dirt>(world);
            //string world = ProtoUtil.Serialize<Test>(new MagicalLifeAPI.GUI.Test3());
            //MagicalLifeAPI.GUI.Test3 test = ProtoUtil.Deserialize<MagicalLifeAPI.GUI.Test3>(world);
            MasterLog.DebugWriteLine(world);
        }
    }
}