using System;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.HECS, Doc.Strategy, "Context for sphere cast node")]
    public sealed class SphereCastContext : BaseComponent
    {
        public HECSList<Entity> Entities = new HECSList<Entity>(16);
        public RaycastHit[] RaycastHits = new RaycastHit[16];

        public void CheckCount(int count) 
        { 
            if (RaycastHits.Length < count)
            {
                Entities = new HECSList<Entity>(count);
                RaycastHits = new RaycastHit[count];
            }
        }
    }
}