using HECSFramework.Core;
using Strategies;

[Documentation(Doc.UniversalNodes, Doc.Strategy, Doc.HECS, "this node contains and provide float")]
public sealed class FloatNode : GenericNode<float>
{
    public override string TitleOfNode { get; } = "FloatNode";

    [Connection(ConnectionPointType.Out, "<float> Out")]
    public BaseDecisionNode Out;

    [ExposeField]
    public float SetValue = 1;

    public override void Execute(Entity entity)
    {
    }

    public override float Value(Entity entity)
    {
        return SetValue;
    }
}
