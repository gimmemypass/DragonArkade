using HECSFramework.Core;
using Strategies;
using UnityEngine;

[Documentation(Doc.UniversalNodes, Doc.HECS, "this node return unity time delta time" )]
public class GetDeltaTimeNode : GenericNode<float>
{
    public override string TitleOfNode { get; } = "GetDeltaTimeNode";

    [Connection(ConnectionPointType.Out, "<float>")]
    public BaseDecisionNode BaseDecisionNode;

    public override void Execute(Entity entity)
    {
    }

    public override float Value(Entity entity)
    {
        return Time.deltaTime;
    }
}
