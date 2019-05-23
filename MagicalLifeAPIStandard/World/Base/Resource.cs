﻿using MagicalLifeAPI.Components;
using MagicalLifeAPI.Components.Resource;
using MagicalLifeAPI.DataTypes.Attribute;
using MagicalLifeAPI.GUI;
using ProtoBuf;

namespace MagicalLifeAPI.World.Base
{
    /// <summary>
    /// A base class for all resources.
    /// Resources in tiles are things such as stone and minerals.
    /// </summary>
    [ProtoContract]
    public abstract class Resource : HasComponents
    {
        /// <summary>
        /// The display name of the resource.
        /// </summary>
        [ProtoMember(1)]
        public string DisplayName { get; private set; }

        /// <summary>
        /// How much of the resources is left.
        /// </summary>
        [ProtoMember(2)]
        public int Durability { get; set; }

        [ProtoMember(3)]
        public Attribute32 MaxDurability { get; private set; }

        protected Resource(string name, int durability, ComponentHarvestable harvestBehavior)
            : base(true)
        {
            this.AddComponent(new ComponentHasTexture(false));
            this.AddComponent(harvestBehavior);
            this.DisplayName = name;
            this.Durability = durability;
            this.MaxDurability = new Attribute32(this.Durability);
        }

        protected Resource()
        {
        }
    }
}