﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace TiberiumRim
{
    public class PlaceWorker_Suppression : PlaceWorker
    {
        public override void DrawGhost(ThingDef def, IntVec3 center, Rot4 rot, Color ghostCol, Thing thing = null)
        {
            var props = def.GetCompProperties<CompProperties_Suppression>();
            if(props == null)
                return;
            Map map = Find.CurrentMap;
            Predicate<IntVec3> pred = cell => !cell.Roofed(map) && GenSight.LineOfSight(center, cell, map);
            GenDraw.DrawFieldEdges(TRUtils.SectorCells(center, map, props.radius, props.angle, rot.AsAngle,false, pred).ToList(), Color.cyan);
            var otherCells = map.GetComponent<MapComponent_Suppression>().ActiveCells.ToList();
            GenDraw.DrawFieldEdges(otherCells, Color.gray);

        }
    }
}
