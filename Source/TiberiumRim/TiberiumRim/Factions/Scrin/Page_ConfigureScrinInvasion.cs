﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace TiberiumRim
{
    public class Page_ConfigureScrinInvasion : Page
    {
        public override string PageTitle
        {
            get
            {
                return "TR_Scrin_SetupInvasion".Translate();
            }
        }

        public override void PreOpen()
        {
            Find.GameInitData.startingPawnCount = 1;
            var req = new PawnGenerationRequest(PawnKindDef.Named("ScrinDrone"));
            Pawn pawn = PawnGenerator.GeneratePawn(req);
            Log.Message("Pawn: " + pawn);
            //Find.GameInitData.startingAndOptionalPawns.Add(pawn);
            base.PreOpen();
        }

        public override void DoWindowContents(Rect rect)
        {
            base.DrawPageTitle(rect);
            rect.yMin += 45f;
            base.DoBottomButtons(rect, "Start".Translate(), null, null, true, false);
        }

        protected override bool CanDoNext()
        {
            if (!base.CanDoNext())
                return false;

            return true;
        }

        protected override void DoNext()
        {
            base.DoNext();
        }
    }
}
