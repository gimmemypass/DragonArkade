using System;
using Components;
using HECSFramework.Core;
using Strategies;

public sealed class GetFloatAnimParameterValueFrom : GenericNode<float>
{
    public override string TitleOfNode { get; } = "Get Float AnimParameter";

    [Connection(ConnectionPointType.Out, "<float> out")]
    public BaseDecisionNode Connect;

    [AnimParameterDropDown]
    public int AnimIdentifer;

    [NonSerialized]
    private HECSMask animatorStateComponentMask = HMasks.GetMask<AnimatorStateComponent>();

    public override void Execute(Entity entity)
    {
    }

    public override float Value(Entity entity)
    {
        if (entity.TryGetComponent(out AnimatorStateComponent animatorStateComponent))
        {
            if (animatorStateComponent.State.TryGetFloat(AnimIdentifer, out var parameter))
            {
                return parameter.Value;
            }
        }

        //todo if we dont have parameter on state, we think its should be 1, but better will alrdy have it from animator helper for example
        return 1;
    }
}
