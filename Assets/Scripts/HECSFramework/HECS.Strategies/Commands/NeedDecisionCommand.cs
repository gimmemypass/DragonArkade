using HECSFramework.Core;
using Strategies;
using Systems;

namespace Commands
{
	[Documentation(Doc.AI, Doc.Strategy, "this command we send from strategy node " + nameof(CompleteFinalNode) + "and wait new decision result (start new strategy for example)" + nameof(AINPCSystem))]
	public struct NeedDecisionCommand : ICommand
	{
	}
}