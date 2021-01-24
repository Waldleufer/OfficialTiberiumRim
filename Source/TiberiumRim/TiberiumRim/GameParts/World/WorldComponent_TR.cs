﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld.Planet;
using Verse;

namespace TiberiumRim
{
    public class WorldComponent_TR : WorldComponent
    {
        //Discovery
        public DiscoveryTable DiscoveryTable;

        //Infos
        public TiberiumWorldInfo TiberiumInfo;
        public GroundZeroInfo GroundZeroInfo;
        public SuperWeaponInfo SuperWeaponInfo;
        public SatelliteInfo SatelliteInfo;

        //
        public GameSettingsInfo GameSettings;

        //Incident Locks
        public bool AllowTRInit => GroundZeroInfo.HasGroundZero;
        public bool AllowNewMeteorites => TiberiumDefOf.MineralAnalysis.IsFinished;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Deep.Look(ref DiscoveryTable, "DiscoveryTable");
            Scribe_Deep.Look(ref TiberiumInfo, "TiberiumInfo", world);
            Scribe_Deep.Look(ref GroundZeroInfo, "GroundZeroInfo", world);
            Scribe_Deep.Look(ref SuperWeaponInfo, "SuperWeaponInfo", world);
            Scribe_Deep.Look(ref SatelliteInfo, "SatelliteInfo", world);
            Scribe_Deep.Look(ref GameSettings, "GameSettings", world);
        }

        public WorldComponent_TR(World world) : base(world)
        {
            DiscoveryTable = new DiscoveryTable();
            TiberiumInfo = new TiberiumWorldInfo(world);
            GroundZeroInfo = new GroundZeroInfo(world);
            SuperWeaponInfo = new SuperWeaponInfo(world);
            SatelliteInfo = new SatelliteInfo(world);
            GameSettings = new GameSettingsInfo(world);
        }

        public void Notify_TiberiumArrival(Map map)
        {
            TiberiumInfo.SpawnTiberiumTile(map.Tile);
        }
    }
}
