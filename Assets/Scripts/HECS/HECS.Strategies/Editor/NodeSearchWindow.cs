using Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
{
    private EditorWindow _window;
    private StrategyGraphView _graphView;

    private Texture2D _indentationIcon;
    private List<Type> nodeTypes = new List<Type>(64);

    private void OnEnable()
    {
        var a = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes());

        foreach (var item in a)
        {
            if (item.IsSubclassOf(typeof(BaseDecisionNode)) && !item.IsAbstract)
                nodeTypes.Add(item);
        }
    }

    public void Configure(EditorWindow window, StrategyGraphView graphView)
    {
        _window = window;
        _graphView = graphView;

        //Transparent 1px indentation icon as a hack
        _indentationIcon = new Texture2D(1, 1);
        _indentationIcon.SetPixel(0, 0, new Color(0, 0, 0, 0));
        _indentationIcon.Apply();
    }

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var tree = new List<SearchTreeEntry>(32);
        //{
        //    new SearchTreeGroupEntry(new GUIContent("Create Node"), 0),
        //    //new SearchTreeGroupEntry(new GUIContent("Dialogue"), 1),
        //    new SearchTreeEntry(new GUIContent("Dialogue Node", _indentationIcon))
        //    {
        //        level = 1, userData = new DrawNodeViewGraph()
        //    },
        //    new SearchTreeEntry(new GUIContent("Comment Block",_indentationIcon))
        //    {
        //        level = 1,
        //        userData = new Group()
        //    }
        //};

        tree.Add(new SearchTreeGroupEntry(new GUIContent("Create Decision Node"), 0));

        foreach (var t in nodeTypes)
        {

            var instance = ScriptableObject.CreateInstance(t);
            var d = t.GetProperty("TitleOfNode");
            var test = d.GetValue(instance);

            if (test == null)
            {
                Debug.LogAssertion("не прописан тайтл у " + t.Name);
                continue;
            }

            tree.Add(new SearchTreeEntry(new GUIContent(test.ToString()))
            {
                level = 1,
                userData = t,
            });
        }

        return tree;
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        //Editor window-based mouse position
        var mousePosition = _window.rootVisualElement.ChangeCoordinatesTo(_window.rootVisualElement.parent,
            context.screenMousePosition - _window.position.position);
        var graphMousePosition = _graphView.contentViewContainer.WorldToLocal(mousePosition);
        _graphView.AddNodeToStrategyGraph(graphMousePosition, SearchTreeEntry.userData as Type);

        return true;
    }
}
