﻿// <copyright file="ItemRegistry.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace EarthWithMagicAPI.API.Registry
{
    using EarthWithMagicAPI.API.Interfaces.Items;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public static class ItemRegistry
    {
        public static List<IItem> Items = new List<IItem>();

        static ItemRegistry()
        {
            Assembly itemAssembly = Assembly.Load(new AssemblyName("EarthMagicItems"));
            Type interfaceType = typeof(IItem);

            foreach (Type item in itemAssembly.GetTypes())
            {
                if (interfaceType.IsAssignableFrom(item) && !item.GetTypeInfo().IsAbstract)
                {
                    foreach (ConstructorInfo constructor in item.GetTypeInfo().DeclaredConstructors)
                    {
                        if (constructor.GetParameters().Length == 0)
                        {
                            IItem someItem = (IItem)itemAssembly.CreateInstance(item.FullName, false);
                            Items.Add(someItem);
                        }
                    }
                }
            }
        }
    }
}