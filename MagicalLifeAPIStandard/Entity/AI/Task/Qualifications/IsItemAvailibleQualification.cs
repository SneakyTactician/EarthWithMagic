﻿using MagicalLifeAPI.DataTypes;
using MagicalLifeAPI.Registry.ItemRegistry;
using ProtoBuf;

namespace MagicalLifeAPI.Entity.AI.Task.Qualifications
{
    /// <summary>
    /// Determines whether an item of the specified type is unreserved in the world.
    /// </summary>
    [ProtoContract]
    public class IsItemAvailibleQualification : Qualification
    {
        [ProtoMember(1)]
        protected int ItemID { get; set; }

        [ProtoMember(2)]
        protected int Dimension { get; set; }

        protected Point2D SearchLocation { get; set; } = new Point2D(0, 0);

        public IsItemAvailibleQualification(int itemID, int dimension)
        {
            this.ItemID = itemID;
            this.Dimension = dimension;
        }

        public override bool ArePreconditionsMet()
        {
            return ItemFinder.IsItemAvailible(this.ItemID, this.Dimension, this.SearchLocation);
        }

        public override bool IsQualified(Living l)
        {
            return true;
        }
    }
}