using HECSFramework.Core;

namespace Commands
{
    [Documentation(Doc.UI,  "We send this command to ui like bars, with force them to redraw with new values")]
	public struct RedrawBarCommand<T> : ICommand where T : struct
	{
		public T CurrentValue;
		public T MaxValue;
	}
}