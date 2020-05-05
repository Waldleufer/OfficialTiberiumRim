﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace TiberiumRim
{
    public class TiberiumFloraMapInfo
    {
        public Map map;
        private readonly TiberiumFloraGrid floraGrid;

        public List<TiberiumGarden> Gardens;
        public List<TiberiumPond> Ponds;

        public TiberiumFloraMapInfo(Map map)
        {
            this.map = map;
            floraGrid = new TiberiumFloraGrid(map);
            //Init();
        }

        public void Init()
        {
            LongEventHandler.QueueLongEvent(delegate ()
            {
                FloodFiller filler = map.floodFiller;
                foreach (IntVec3 cell in map.AllCells)
                {
                    if (ShouldGrowFloraAt(cell)) continue;
                    TerrainDef terrain = cell.GetTerrain(map);
                    if (IsGarden(terrain))
                    {
                        TiberiumGarden garden = new TiberiumGarden(floraGrid);
                        filler.FloodFill(cell, ((IntVec3 c) => c.GetTerrain(map) == terrain), delegate (IntVec3 cell) {
                            floraGrid.SetGrow(cell, true);
                            garden.AddCell(cell);
                        });
                        Gardens.Add(garden);
                    }
                }
            }, "SettingFloraBools", false, null);
        }

        public void FloraTick()
        {

        }

        public void RegisterTiberiumPlant(TiberiumPlant plant)
        {
            floraGrid.SetFlora(plant.Position, true);
            Notify_PlantSpawned(plant);
        }

        public void DeregisterTiberiumPlant(TiberiumPlant plant)
        {
            floraGrid.SetFlora(plant.Position, false);
        }

        //Bools
        public bool HasFloraAt(IntVec3 c)
        {
            return floraGrid.floraBools[c];
        }

        public bool ShouldGrowFloraAt(IntVec3 c)
        {
            return floraGrid.growBools[c] && !floraGrid.floraBools[c];
        }

        private bool IsGarden(TerrainDef def)
        {
            return def.IsMoss() || (def.IsSoil() && (def.fertility >= 1.2f));
        }

        private bool IsPond(TerrainDef def)
        {
            return def.IsWater && !def.IsRiver;
        }

        public void Notify_PlantSpawned(TiberiumPlant plant)
        {
            floraGrid.Notify_PlantSpawned(plant);
        }

    }
}
