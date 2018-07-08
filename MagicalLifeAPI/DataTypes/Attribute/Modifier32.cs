﻿using MagicalLifeAPI.Entities.Util.Modifier_Remove_Conditions;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicalLifeAPI.Entity.Util
{
    /// <summary>
    /// Used to store a modifier, and some other information for internal use.
    /// </summary>
    [ProtoContract]
    public struct Modifier32
    {
        [ProtoMember(1)]
        public Int32 Value;

        [ProtoMember(2)]
        public IModifierRemoveCondition RemoveCondition;

        [ProtoMember(3)]
        public string Explanation;

        public Modifier32(Int32 value, IModifierRemoveCondition removeCondition, string explanation)
        {
            this.Value = value;
            this.RemoveCondition = removeCondition;
            this.Explanation = explanation;
        }
    }
}
