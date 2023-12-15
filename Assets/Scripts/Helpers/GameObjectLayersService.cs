using UnityEngine;

namespace HECSFramework.Unity.Helpers
{
    public static class GameObjectLayersService
    {
        public static void SetLayerRecursive(this GameObject obj,int layer)
        {
            obj.layer = layer;
            foreach (Transform child in obj.transform)
            {
                SetLayerRecursive(child.gameObject, layer);
            }
        }
        public static bool CheckLayerRecursive(this GameObject obj,int layer)
        {
            if (obj.layer != layer)
                return false;
            foreach (Transform child in obj.transform)
            {
                var checkLayerRecursive = CheckLayerRecursive(child.gameObject, layer);
                if (!checkLayerRecursive) return false;
            }

            return true;
        }
    }
}