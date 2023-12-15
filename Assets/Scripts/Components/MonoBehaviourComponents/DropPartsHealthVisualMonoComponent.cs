using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

namespace Components.MonoBehaviourComponents
{
    public class DropPartsHealthVisualMonoComponent : MonoBehaviour
    {
        public List<GameObject> Parts;

#if UNITY_EDITOR
        [Button]
        public void CollectChildren()
        {
            Parts.Clear();
            for (int i = transform.childCount - 1; i >= 0 ; i--)
            {
                Parts.Add(transform.GetChild(i).gameObject); 
            }
        }
#endif
    }
}