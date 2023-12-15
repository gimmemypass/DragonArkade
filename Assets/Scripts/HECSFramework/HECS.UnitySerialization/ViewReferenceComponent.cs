using HECSFramework.Core;

namespace Components
{
    public partial class ViewReferenceComponent : BaseComponent, IBeforeSerializationComponent, IAfterSerializationComponent
    {
        public void AfterSync()
        {
            this.ViewReference = new ActorViewReference(AssetReferenceGuid);
        }

        public void BeforeSync()
        {
            this.AssetReferenceGuid = ViewReference.AssetGUID;
        }
    }
}