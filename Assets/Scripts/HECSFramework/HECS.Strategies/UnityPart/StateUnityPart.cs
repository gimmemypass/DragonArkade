using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Strategies
{
    public partial class State : BaseStrategy
    {
#if UNITY_EDITOR

        private void OnEnable()
        {
        }

        protected override void OnOpenActions()
        {
            base.OnOpenActions();
            CheckNeeded();
        }


        private void CheckNeeded()
        {
            AddNode<StartDecision>(new Vector2(443, 253));
            AddNode<UpdateStateNode>(new Vector2(443, 364));
            AddNode<ExitStateNode>(new Vector2(1274, 280));
        }

        private void AddNode<T>(Vector2 coord) where T : ScriptableObject
        {
            if (nodes.Any(x => x is T)) return;

            var createInstance = ScriptableObject.CreateInstance<T>();
            createInstance.name = typeof(T).Name;
            nodes.Add(createInstance as BaseDecisionNode);

            if (coord != Vector2.zero && createInstance is BaseDecisionNode decisionNode)
            {
                decisionNode.coords = coord;
            }

            var path = AssetDatabase.GetAssetPath(this);
            AssetDatabase.AddObjectToAsset(createInstance, path);
            AssetDatabase.SaveAssets();
        }
#endif
    }
}
