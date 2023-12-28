using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace HECSFramework.Unity.Editor
{
    public static class CenterChildrenHelper
    {
        [MenuItem("Tools/CenterChildren")]
        public static void CenterChildren()
        {
            var select = Selection.activeGameObject;
            var centerPos = Vector3.zero;
            for (int i = 0; i < select.transform.childCount; i++)
            {
                centerPos += select.transform.GetChild(i).transform.localPosition;
            }
            centerPos /= select.transform.childCount;
            
            for (int i = 0; i < select.transform.childCount; i++)
            {
                select.transform.GetChild(i).transform.localPosition -= centerPos;
            }
        } 

    }
}