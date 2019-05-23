﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MagicalLifeAPI.Filing
{
    /// <summary>
    /// Manages file system operations.
    /// </summary>
    public static class FileSystemManager
    {
        /// <summary>
        /// The path to the current instance folder.
        /// </summary>
        public static string InstanceRootFolder { get; private set; }

        /// <summary>
        /// The path to the save directory.
        /// </summary>
        public static string SaveDirectory { get; private set; }

        /// <summary>
        /// The directory that mods are stored in.
        /// </summary>
        public static string ModDirectory { get; private set; }

        /// <summary>
        /// The directory the main executable is in.
        /// </summary>
        public static string RootDirectory { get; set; }

        public static void Initialize()
        {
            SetupRootFolder();

            DirectoryInfo savePath = Directory.CreateDirectory(FileSystemManager.RootDirectory + Path.DirectorySeparatorChar + "Save");
            SaveDirectory = savePath.FullName;
            DirectoryInfo modPath = Directory.CreateDirectory(FileSystemManager.RootDirectory + Path.DirectorySeparatorChar + "Mods");
            ModDirectory = modPath.FullName;
            MoveCoreMod();
        }

        /// <summary>
        /// Moves the core Magical Life mod into the mods folder if it is discovered.
        /// </summary>
        private static void MoveCoreMod()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            UriBuilder uri = new UriBuilder(currentAssembly.CodeBase);
            string unescaped = Uri.UnescapeDataString(uri.Path);
            DirectoryInfo directory = new DirectoryInfo(Path.GetDirectoryName(unescaped));
            IEnumerable<FileInfo> files = directory.EnumerateFiles("*.dll");

            foreach (FileInfo item in files)
            {
                if (item.Name == "MagicalLifeCoreMod.dll")
                {
                    item.CopyTo(ModDirectory + Path.DirectorySeparatorChar + "MagicalLifeCoreMod.dll", true);
                    break;
                }
            }
        }

        /// <summary>
        /// Sets up the root folder for our program.
        /// </summary>
        private static void SetupRootFolder()
        {
            InstanceRootFolder = Assembly.GetExecutingAssembly().Location;
            InstanceRootFolder = Path.GetDirectoryName(InstanceRootFolder);
            RootDirectory = InstanceRootFolder;
            InstanceRootFolder += Path.DirectorySeparatorChar;
            InstanceRootFolder += "Logging";
            Directory.CreateDirectory(InstanceRootFolder);
            InstanceRootFolder += Path.DirectorySeparatorChar;
            InstanceRootFolder += "Instances";
            Directory.CreateDirectory(InstanceRootFolder);
            InstanceRootFolder += Path.DirectorySeparatorChar;
            InstanceRootFolder += GetIOSafeTime();
            Directory.CreateDirectory(InstanceRootFolder);
        }

        /// <summary>
        /// Returns a IO (file and directory) safe time.
        /// </summary>
        /// <returns></returns>
        public static string GetIOSafeTime()
        {
            return string.Format("{0:[yyyy-MM-dd][hh-mm-ss-tt}]",
            DateTime.Now);
        }

        /// <summary>
        /// Returns the path of all save games.
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllSaveNames()
        {
            string[] ret = Directory.GetDirectories(FileSystemManager.SaveDirectory);

            int length = ret.Length;
            for (int i = 0; i < length; i++)
            {
                DirectoryInfo info = new DirectoryInfo(ret[i]);
                ret[i] = info.Name;
            }

            return ret;
        }
    }
}