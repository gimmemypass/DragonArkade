using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Components
{
    [Serializable][Documentation(Doc.FX, Doc.Visual, "This is holder of poolable fx for global entity - Visual Manager")]
    public sealed class VisualFXHolderComponent : BaseComponent, IWorldSingleComponent
    {
        [SerializeField] private FXLink[] fXLinks = default; 

        public bool TryGetReferenceToFX(int id, out AssetReference assetReference)
        {
            foreach (var fx in fXLinks)
            {
                if (fx.fXIdentifier.Id == id)
                {
                    assetReference = fx.AssetReference;
                    return true;
                }
            }

            assetReference = null;
            return false;
        }
    }

    [Serializable]
    public struct FXLink
    {
        public FXIdentifier fXIdentifier;
        public AssetReference AssetReference;
    }
}