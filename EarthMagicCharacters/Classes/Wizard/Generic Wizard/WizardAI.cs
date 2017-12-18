﻿// <copyright file="WizardAI.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace EarthMagicCharacters.Classes.Wizard.Generic_Wizard
{
    using EarthWithMagicAPI.API.Creature;
    using EarthWithMagicAPI.API.Stuff;
    using EarthWithMagicAPI.API.Util;

    public class WizardAI : IAI
    {
        public void YourTurn(Encounter encounter, ICreature creature)
        {
            creature.UsableSpells[Dice.RollDice(new Die(1, creature.UsableSpells.Count + 1, -1), creature.Name + " is casting a random spell!")].Cast(encounter.Party, encounter.Enemies, creature);
        }
    }
}