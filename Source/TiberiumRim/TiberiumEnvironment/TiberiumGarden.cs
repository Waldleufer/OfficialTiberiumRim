﻿using System.Collections.Generic;
using Verse;

namespace TiberiumRim
{
    public class TiberiumGarden
    {
        private Map map;
        private CellArea cells;
        private TiberiumBlossom blossomParent;
        private List<TiberiumPlant> tiberiumFlora = new List<TiberiumPlant>();

        public TiberiumGarden(Map map)
        {
            this.map = map;
            cells = new CellArea(map);
        }

        public TiberiumGarden(Map map, List<IntVec3> cells)
        {
            this.map = map;
            this.cells = new CellArea(map);
            this.cells.AddRange(cells);
        }

        public void GardenTick()
        {

        }

        public void AddCell(IntVec3 cell)
        {
            cells.Add(cell);
        }

        public void RegisterFlora(TiberiumPlant plant)
        {
            tiberiumFlora.Add(plant);
        }

        public void DeregisterFlora(TiberiumPlant plant)
        {
            tiberiumFlora.Remove(plant);
        }

        public void GrowFlora(float pct)
        {
            tiberiumFlora.ForEach(f => f.Growth += pct);
        }
    }
}
