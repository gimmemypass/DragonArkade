using HECSFramework.Core;

namespace Commands
{
    [Documentation(Doc.Abilities,  "With this command we do dmg to other entities ")]
	public struct DamageCommand<T> : ICommand where T : struct
	{
		public Entity DamageDealer;
		public int DmgType;
		public T DamageValue;
	}
}