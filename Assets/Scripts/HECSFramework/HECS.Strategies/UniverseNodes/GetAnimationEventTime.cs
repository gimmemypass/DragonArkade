using System;
using Components;
using HECSFramework.Core;
using HECSFramework.Unity;
using Strategies;
using UnityEngine;

[Documentation(Doc.Strategy, Doc.Animation, Doc.Abilities, "this strategy node where we set animation event, for this event we take checkout")]
public sealed class GetAnimationEventTime : GenericNode<float>
{
    public override string TitleOfNode { get; } = "GetAnimationEventTime";

    [DropDownIdentifier("AnimationEventIdentifier")]
    public int AnimationEventID;

    [Connection(ConnectionPointType.In, "<float> Animation speed multiplier")]
    public GenericNode<float> Multiplier;

    [Connection(ConnectionPointType.Out, "<float Out>")]
    public BaseDecisionNode Out;
    
    public override float Value(Entity entity)
    {
        if (entity.TryGetComponent(out AnimationCheckOutsHolderComponent animationCheckOutsHolder))
        {
            if (animationCheckOutsHolder.TryGetCheckoutInfo(AnimationEventID, out var info))
            {
                var multiplier = Multiplier == null ? 1 : Multiplier.Value(entity);
                multiplier = multiplier == 0 ? 1 : multiplier;
                return info.Timing/ multiplier;
            }
        }
            
        throw new Exception("we dont have checkout component on " + entity.ID);
    }

    public override void Execute(Entity entity)
    {
    }
}