using Commands;
using HECSFramework.Core;
using Strategies;

public class StopStrategyNode : FinalDecision
{
    public override string TitleOfNode { get; } = "Stop strategy";

    protected override void Run(Entity entity)
    {
        entity.Command(new ForceStopAICommand());
    }
}