using Components;
using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.HECS, Doc.UniversalNodes, Doc.Animation, "this node return diff from event to end of animation clip")]
    public sealed class GetTimeFromEventToEndOfClip : GenericNode<float>
    {
        public override string TitleOfNode { get; } = "GetTimeFromEventToEndOfClip";

        [DropDownIdentifier("AnimationEventIdentifier")]
        public int AnimationEventID;

        [Connection(ConnectionPointType.Out, "<float> Out")]
        public BaseDecisionNode Out;

        public override void Execute(Entity entity)
        {
        }

        public override float Value(Entity entity)
        {
            if (entity.TryGetComponent(out AnimationCheckOutsHolderComponent animationCheckOutsHolder))
            {
                if (animationCheckOutsHolder.TryGetCheckoutInfo(AnimationEventID, out var info))
                {
                    return info.ClipLenght - info.Timing;
                }
            }

            HECSDebug.LogError($"{entity.ID} doesnt have AnimationCheckoutHolder");
            return 0;
        }
    }
}