using HECSFramework.Core;

namespace Commands
{
    [Documentation(Doc.Damage, Doc.FX, Doc.Abilities, "This send all info for visualize damage")]
	public struct DamageForVisualFXCommand : IGlobalCommand
	{
		public Entity Victim;
		public float StartDamage;
		public float DamageAfterResist;
		public int DamageType;
	}
}