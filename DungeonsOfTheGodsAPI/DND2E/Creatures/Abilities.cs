﻿using System.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfTheGodsAPI.DND2E.Creatures
{
    /// <summary>
    /// Holds and manages many attribures of a creature.
    /// </summary>
    public class Abilities
    {
        public Abilities(int strength, int dexterity, int constitution, int wisdom, int intelligence, int charisma)
        {
            this.Strength = new Attribute(strength);
            this.Dexterity = new Attribute(dexterity);
            this.Constitution = new Attribute(constitution);
            this.Wisdom = new Attribute(wisdom);
            this.Intelligence = new Attribute(intelligence);
            this.Charisma = new Attribute(charisma);
        }

        public Abilities(Attribute strength, Attribute dexterity, Attribute constitution, Attribute wisdom, Attribute intelligence, Attribute charisma)
        {
            this.Strength = strength;
            this.Dexterity = dexterity;
            this.Constitution = constitution;
            this.Wisdom = wisdom;
            this.Intelligence = intelligence;
            this.Charisma = charisma;
        }

        /// <summary>
        /// How strong the creature is.
        /// </summary>
        public Attribute Strength { get; }

        /// <summary>
        /// How nimble the creature is.
        /// </summary>
        public Attribute Dexterity { get; }

        /// <summary>
        /// How tough the creature is.
        /// </summary>
        public Attribute Constitution { get; }

        /// <summary>
        /// How wise the creature is.
        /// </summary>
        public Attribute Wisdom { get; }

        /// <summary>
        /// How smart the creature is.
        /// </summary>
        public Attribute Intelligence { get; }

        /// <summary>
        /// How charismatic the creature is.
        /// </summary>
        public Attribute Charisma { get; }
    }
}