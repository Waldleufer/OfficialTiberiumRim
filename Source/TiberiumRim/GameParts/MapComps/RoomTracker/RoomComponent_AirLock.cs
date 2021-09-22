﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace TiberiumRim
{
    public class RoomComponent_AirLock : RoomComponent
    {
        private RoomComponent_Atmospheric atmosphericCompInt;
        private List<Building> AirVents = new List<Building>();
        private List<Building_AirLock> AirLockDoors = new List<Building_AirLock>();

        private bool HasAirLockRole = false;
        
        public RoomComponent_Atmospheric Atmospheric => atmosphericCompInt ??= Parent.GetRoomComp<RoomComponent_Atmospheric>();

        public bool IsActive => HasAirLockRole && AirVents.Concat(AirLockDoors).All(c => c.IsPoweredOn());
        public bool IsClean => Atmospheric.ActualValue <= 0;

        public bool AllDoorsClosed => AirLockDoors.All(d => !d.Open);

        public override void Create(RoomTracker parent)
        {
            base.Create(parent);
        }
        public override void Notify_Reused()
        {
            base.Notify_Reused();
        }

        public override void Disband(RoomTracker parent, Map map)
        {
            foreach (var buildingAirLock in AirLockDoors)
            {
                buildingAirLock.SetAirlock(null);
            }
            base.Disband(parent, map);
        }

        public override void FinalizeApply()
        {
            base.FinalizeApply();
            SetData();
        }

        public override void Notify_ThingSpawned(Thing thing)
        {
            TryAddComponent(thing);
        }

        public override void Notify_ThingDespawned(Thing thing)
        {
            TryRemoveComponent(thing);
        }

        private void SetData()
        {
            HasAirLockRole = Room.Districts.All(r => r.Room.Role == TiberiumDefOf.TR_AirLock);
        }

        private void TryAddComponent(Thing thing)
        {
            if (thing is Building_AirLock airLock)
            {
                AirLockDoors.Add(airLock);
                airLock.SetAirlock(this);
                return;
            }
            var comp = thing.TryGetComp<Comp_ANS_AirVent>();
            if (comp != null)
            {
                AirVents.Add(thing as Building);
            }
        }

        private void TryRemoveComponent(Thing thing)
        {
            AirLockDoors.Remove(thing as Building_AirLock);
            AirVents.Remove(thing as Building);
        }

        public override void Draw()
        {
            if (UI.MouseCell().GetRoom(Map) == this.Room)
            {
                GenDraw.DrawFieldEdges(AirVents.Select(t => t.Position).ToList(), Color.green);
            }

        }
    }
}