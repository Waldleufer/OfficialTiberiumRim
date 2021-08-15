﻿using Verse;

namespace TiberiumRim
{
    public abstract class RoomComponent
    {
        private RoomTracker parent;

        public RoomTracker Parent => parent;
        public Map Map => Parent.Map;
        public Room Room => Parent.Room;

        public virtual void Create(RoomTracker parent)
        {
            this.parent = parent;
        }

        public virtual void Disband(RoomTracker parent, Map map) { }
        public virtual void Notify_Reused() { }
        public virtual void Notify_RoofClosed() { }
        public virtual void Notify_RoofOpened() { }

        public virtual void PreApply() { }

        public virtual void FinalizeApply() { }


        public virtual void Notify_RoofChanged() { }

        public virtual void CompTick() { }

        public virtual void OnGUI() { }

        public virtual void Draw() { }
    }
}