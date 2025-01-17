﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using UnityEngine;

namespace TiberiumRim
{ 
    public enum NetworkMode
    {
        Alpha,
        Beta,
        Gamma,
        Delta,
        Epsilon
    }

    public class TiberiumNetwork
    {
        public MapComponent_TNWManager Manager;
        public StructureSet NetworkSet = new StructureSet();

        public int NetworkID = -1;
        public NetworkMode NetworkMode = NetworkMode.Alpha;
        public bool networkDirty = false;

        public CompTNW_TNC NetworkController => Manager.MainController;
        public bool IsWorking => NetworkController?.CompPower?.PowerOn ?? false;

        public float TotalNetworkValue => NetworkSet.FullList.Sum(c => c.Container.TotalStorage);
        public float TotalSiloNetworkValue => NetworkSet.Silos.Any() ? NetworkSet.Silos.Sum(c => c.Container.TotalStorage) : 0;

        public IEnumerable<TiberiumValueType> AvailableTypes => NetworkSet.FullList.SelectMany(t => t.Container.AllStoredTypes).Distinct();

        public Dictionary<TiberiumValueType, float> TypeValues
        {
            get
            {
                Dictionary<TiberiumValueType, float> dictionary = new Dictionary<TiberiumValueType, float>();
                foreach (var comp in NetworkSet.FullList)
                {
                    foreach (var typeValue in comp.Container.AllStoredTypeValues)
                    {
                        if (!dictionary.ContainsKey(typeValue.Key))
                        {
                            dictionary.Add(typeValue.Key, 0);
                        }
                        dictionary[typeValue.Key] += typeValue.Value;
                    }
                }
                return dictionary;
            }
        }

        public Color GeneralColor
        {
            get
            {
                Color color = new Color(0, 0, 0, 0);
                if (!NetworkSet.Silos.NullOrEmpty())
                {
                    int count = NetworkSet.Silos.Count;
                    for (int i = 0; i < count; i++)
                    {
                        color += NetworkSet.Silos[i].Container.Color;
                    }
                    color /= count;
                }
                return color;
            }
        }

        public TiberiumNetwork(MapComponent_TNWManager Manager)
        {
            this.Manager = Manager;
        }

        public TiberiumNetwork(CompTNW root, MapComponent_TNWManager Manager)
        {
            this.Manager = Manager;
            Manager.MakeNewNetwork(root, this);
            Manager.RegisterNetwork(this);
        }

        public void Tick()
        {
        }

        public bool ValidFor(TNWMode mode, out string reason)
        {
            reason = string.Empty;
            switch (mode)
            {
                case TNWMode.Consumer:
                    reason = "TR_ConsumerLack";
                    return NetworkSet.FullList.Any(x => x.NetworkMode == TNWMode.Storage || x.NetworkMode == TNWMode.Producer);
                case TNWMode.Producer:
                    reason = "TR_ProducerLack";
                    return NetworkSet.FullList.Any(x => x.NetworkMode == TNWMode.Storage || x.NetworkMode == TNWMode.Consumer);
            }
            return true;
        }


        public float NetworkValueFor(TiberiumValueType valueType)
        {
            float value = 0;
            foreach (var silo in NetworkSet.Silos)
            {
                value += silo.Container.ValueForType(valueType);
            }
            return value;
        }

        public float NetworkValueFor(List<TiberiumValueType> types)
        {
            float value = 0;
            foreach(var silo in NetworkSet.Silos)
            {
                value += silo.Container.ValueForTypes(types);
            }
            return value;
        }

        public List<IntVec3> NetworkCells()
        {
            List<IntVec3> cells = new List<IntVec3>();
            foreach(CompTNW comp in NetworkSet.FullList)
            {
                cells.AddRange(comp.InnerConnectionCells);
            }
            return cells;
        }

        public void AddStructure(CompTNW tnwb)
        {
            NetworkSet.AddNewStructure(tnwb, tnwb.parent.Position);
        }

        public void MakeDirty()
        {
            networkDirty = true;
        }

        public void NotifyPotentialSplit(CompTNW from)
        {
            from.Network = null;
            TiberiumNetwork newNet = null;
            foreach (CompTNW root in from.StructureSet.FullList)
            {
                if (root.Network != newNet)
                {
                    newNet = root.Network = new TiberiumNetwork(root, Manager);
                }
            }
        }

        public string GreekLetter
        {
            get
            {
                switch (NetworkMode)
                {
                    case NetworkMode.Alpha:
                        return "α";
                    case NetworkMode.Beta:
                        return "β";
                    case NetworkMode.Gamma:
                        return "γ";
                    case NetworkMode.Delta:
                        return "δ";
                    case NetworkMode.Epsilon:
                        return "ε";
                }
                return "";
            }
        }
    }
}
