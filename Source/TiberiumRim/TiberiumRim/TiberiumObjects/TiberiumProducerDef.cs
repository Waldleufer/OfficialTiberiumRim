﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace TiberiumRim
{
    public class TiberiumProducerDef : TRThingDef
    {
        //Tiberium Properties
        public TiberiumFieldRuleset tiberiumFieldRules;

        //public List<PotentialEvolution> evolutions;
        public SporeProperties spore;
        public SpawnProperties spawner;
        public float daysToMature = 0f;
        public bool canBeGroundZero = false;
        public bool leaveTiberium = true;
        public bool forResearch = true;

    }

    public class SpawnProperties
    {
        public TiberiumSpawnMode spawnMode = TiberiumSpawnMode.Direct;
        public IntRange spawnInterval = new IntRange(2500, 5000);
        public IntRange explosionRange = new IntRange(10, 100);
        public FloatRange spreadRange = new FloatRange(-1, -1);
        public IntVec3 sporeOffset = new IntVec3(0, 0, 0);
        public float minDaysToSpread = 0f;
        public float sporeExplosionRadius = 20f;
        public float growRadius = 5f;
    }

    public class PlantGroupProperties
    {
        public IntRange sizeRange = new IntRange(5,10);
        public int minFieldSize = 1000;

    }
}
