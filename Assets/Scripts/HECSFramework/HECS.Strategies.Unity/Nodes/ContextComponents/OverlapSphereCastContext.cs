using System;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.HECS, Doc.Strategy, "Context for overlap sphere cast node")]
    public sealed class OverlapSphereCastContext : BaseComponent
    {
        public HECSList<Entity> Entities = new HECSList<Entity>(16);
        public Collider[] Colliders = new Collider[16];
        private Vector3 from;

        public void CheckCount(int count)
        {
            if (Colliders.Length < count)
            {
                Entities = new HECSList<Entity>(count);
                Colliders = new Collider[count];
            }
        }

        public void SortByDistanceFrom(Vector3 from)
        {
            this.from = from;
            Entities.Sort(Sort);
        }

        private int Sort(Entity left, Entity right)
        {
            var leftPos = from - left.GetComponent<UnityTransformComponent>().Transform.position;
            var rightPos = from - right.GetComponent<UnityTransformComponent>().Transform.position;

            if (leftPos.sqrMagnitude < rightPos.sqrMagnitude)
                return -1;
            else if (leftPos.sqrMagnitude > rightPos.sqrMagnitude)
                return 1;
            else return 0;
        }
    }
}