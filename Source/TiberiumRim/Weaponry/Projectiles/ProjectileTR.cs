﻿using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace TiberiumRim
{
    public class ProjectileTR : Projectile, IPatchedProjectile
    {
        public TRThingDef TRDef => base.def as TRThingDef;
        public ProjectileProperties_Extended Props => TRDef?.projectileExtended;

        //
        public virtual float ArcHeightFactorPostAdd => 0;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
        }

        public virtual bool PreLaunch(Thing launcher, Vector3 origin, LocalTargetInfo usedTarget, LocalTargetInfo intendedTarget, ProjectileHitFlags hitFlags, bool preventFriendlyFire = false, Thing equipment = null, ThingDef targetCoverDef = null)
        {
            return true;
        }

        public virtual void PostLaunch(ref Vector3 origin, ref Vector3 destination)
        {
        }

        public virtual bool CanHitOverride(Thing thing)
        {
            return base.CanHit(thing);
        }

        public bool PreImpact()
        {
            return true;
        }

        public void PostImpact()
        {
        }


        protected override void Impact(Thing hitThing)
        {
            if (Props != null)
            {
                Props.impactExplosion?.DoExplosion(Position, Map, this);
                Props.impactFilth?.SpawnFilth(Position, Map);
                Props.impactEffecter?.Spawn(Position, Map);
            }
            base.Impact(hitThing);
        }

        public override Graphic Graphic 
        {
            get
            {
                if (base.Graphic is Graphic_Random Random) return Random.SubGraphicFor(this);
                return base.Graphic;
            }
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}