using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HECSFramework.Core;
using HECSFramework.Unity;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

#pragma warning disable

namespace Strategies
{
    public class SetFilterWindow : OdinEditorWindow
    {
        [LabelText("Components")]
        [ValueDropdown("DrawComponents")]
        public List<int> indeces = new List<int>();
        
        private FieldInfo fieldInfo;
        private DrawNodeViewGraph drawNode;
        private SerializedObject so;

        private List<(string, int)> components;

        protected override void OnEnable()
        {
            base.OnEnable();
            components = new List<(string, int)>(128);

            var allC = new BluePrintsProvider().Components;

            foreach (var c in allC)
            {
                components.Add((c.Key.Name, IndexGenerator.GetIndexForType(c.Key)));
            }
        }


        internal void DrawFilter(MemberInfo memberInfo, DrawNodeViewGraph drawNode, SerializedObject so)
        {
            fieldInfo = memberInfo as FieldInfo;
            this.drawNode = drawNode;
            this.so = so;
            Filter value = (Filter)fieldInfo.GetValue(drawNode.InnerNode);

            for (int i = 0; i < value.Lenght; i++)
            {
                indeces.Add(value[i]);
            }
        }


        private IEnumerable DrawComponents()
        {
            var list  = new ValueDropdownList<int>();

            foreach (var c in components)
            {
                list.Add(c.Item1, c.Item2);
            }

            return list;
        }

        [Button]
        public void SaveFilter()
        {
            Close();
        }

        protected void OnDisable()
        {
            Filter filter = new Filter();

            indeces = indeces.Distinct().ToList();

            for (int i = 0; i < indeces.Count; i++)
            {
                filter.Add(indeces[i]);
            }

            fieldInfo.SetValue(drawNode.InnerNode, filter);
            EditorUtility.SetDirty(drawNode.InnerNode);
            base.OnDestroy();
        }
    }
}
