using Components;
using HECSFramework.Core;
using HECSFramework.Unity;
using Helpers;
using UnityEngine;

namespace Strategies
{
    [Documentation(Doc.Strategy, Doc.UniversalNodes, Doc.HECS, "this node gather spherecast result to hecs list")]
    public class GetEntitiesOverlapSphereCast : GenericNode<HECSList<Entity>>
    {
        [Connection(ConnectionPointType.In, "<Filter> optional filter")]
        public GenericNode<FilterNode> Filter;

        [Connection(ConnectionPointType.In, "<int> Target count")]
        public GenericNode<int> TargetsCount;

        [Connection(ConnectionPointType.In, "<Vector3> Point of cast")]
        public GenericNode<Vector3> PointOfCast;

        [Connection(ConnectionPointType.In, "<float> Radius of cast")]
        public GenericNode<float> RadiusOfCast;

        public override string TitleOfNode { get; } = "GetEntitiesOverlapSphereCast";

        [Connection(ConnectionPointType.Out, "HECSList<Entity> Out")]
        public BaseDecisionNode Out;


        public override void Execute(Entity entity)
        {
        }

        public override HECSList<Entity> Value(Entity entity)
        {
            var context = entity.GetOrAddComponent<OverlapSphereCastContext>();
            context.Entities.ClearFast();
            context.CheckCount(TargetsCount.Value(entity));

            var count = Physics.OverlapSphereNonAlloc(PointOfCast.Value(entity), RadiusOfCast.Value(entity), context.Colliders);

            for (int i = 0; i < count; i++)
            {
                if (context.Colliders[i].TryGetActorFromCollision(out var actor))
                {
                    if (actor.IsAlive())
                    {
                        if (Filter != null)
                        {
                            var filter = Filter.Value(entity);

                            if (actor.Entity.ContainsMask(filter.Include) && !actor.Entity.ContainsMask(filter.Exclude))
                            {
                                context.Entities.Add(actor.Entity);
                            }
                        }
                        else
                        {
                            context.Entities.Add(actor.Entity);
                        }
                    }
                }
            }

            return context.Entities;
        }
    }
}