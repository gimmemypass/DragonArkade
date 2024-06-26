using HECSFramework.Core;

namespace Components
{
    public partial class ViewReferenceComponent : BaseComponent, IAfterLife
    {
        [Field(0)]
        public string AssetReferenceGuid { get; set; } = string.Empty;
    }
}