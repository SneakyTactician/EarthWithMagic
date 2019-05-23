﻿using MagicalLifeAPI.Server;
using MagicalLifeDedicatedServer.API.Commands;
using MagicalLifeDedicatedServer.Properties;
using System.Collections.Generic;

namespace MagicalLifeDedicatedServer.API
{
    public class InternalCommandModule : ICommandModule
    {
        public List<ICommand> GetCommands()
        {
            return new List<ICommand>()
            {
                new NewGame(),
                new StartGame(),
                new SaveGame()
            };
        }

        public string GetCommandUsageName()
        {
            return "ml";
        }

        /// <summary>
        /// The display name of this command module.
        /// </summary>
        /// <returns></returns>
        public string GetFullName()
        {
            return DedicatedServer.MagicalLife;
        }
    }
}