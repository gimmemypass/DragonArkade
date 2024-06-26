using Components;
using HECSFramework.Core;
using Helpers;
using UnityEngine;

namespace Strategies
{
    [Documentation(Doc.Strategy, Doc.UniversalNodes, Doc.HECS, "this node gather spherecast result to hecs list")]
    public class GetEntitiesBySphereCast : GenericNode<HECSList<Entity>>
    {
        [Connection(ConnectionPointType.In, "<Filter> optional filter")]
        public GenericNode<FilterNode> Filter;

        [Connection(ConnectionPointType.In, "<int> Target count")]
        public GenericNode<int> TargetsCount;

        [Connection(ConnectionPointType.In, "<Vector3> Direction")]
        public GenericNode<Vector3> Direction;

        [Connection(ConnectionPointType.In, "<Vector3> Point of cast")]
        public GenericNode<Vector3> PointOfCast;

        [Connection(ConnectionPointType.In, "<float> Radius of cast")]
        public GenericNode<float> RadiusOfCast;


        [Connection(ConnectionPointType.Out, "HECSList<Entity> Out")]
        public BaseDecisionNode Out;
        public override string TitleOfNode { get; } = "GetEntitiesBySphereCast";


        public override void Execute(Entity entity)
        {
        }

        public override HECSList<Entity> Value(Entity entity)
        {
            var context = entity.GetOrAddComponent<SphereCastContext>();
            context.Entities.ClearFast();
            context.CheckCount(TargetsCount.Value(entity));
            
            var count = Physics.SphereCastNonAlloc(PointOfCast.Value(entity), RadiusOfCast.Value(entity), Direction.Value(entity), context.RaycastHits);

            for (int i = 0; i < count; i++)
            {
                if (context.RaycastHits[i].collider.TryGetActorFromCollision(out var actor))
                {
                    if (actor.Entity.IsAlive())
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