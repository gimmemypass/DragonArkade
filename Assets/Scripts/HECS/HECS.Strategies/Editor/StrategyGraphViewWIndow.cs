using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Strategies;

[InitializeOnLoad]
public class StrategyGraphViewWIndow : EditorWindow
{
    private StrategyGraphView graphView;
    private BaseStrategy strategy;
    private string assetPath;
    private NodeSearchWindow _searchWindow;

    static StrategyGraphViewWIndow()
    {
        BaseStrategy.GetWindow += ShowWindowReact;
    }

    private static void ShowWindowReact(BaseStrategy strategy, string path)
    {
        var window = CreateWindow<StrategyGraphViewWIndow>();
        window.titleContent = new GUIContent(strategy.name);
        window.OnInit(strategy, path);
    }

    internal void OnInit(BaseStrategy strategy, string path)
    {
        this.strategy = strategy;
        assetPath = path;

        graphView = new StrategyGraphView(strategy, path, this);
        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);
        CreateMiniMap();
        graphView.AddSearchWindow(this);
    }

    private void CreateMiniMap()
    {
        var minimap = new MiniMap() { anchored = true };
        minimap.SetPosition(new Rect(10, 10, 200, 140));

        graphView.Add(minimap);
    }

    private void OnDisable()
    {
        if (!strategy)
            return;

        if (graphView != null)
        {
            rootVisualElement.Remove(graphView);
            graphView.Dispose();
        }

        EditorUtility.SetDirty(strategy);
        AssetDatabase.SaveAssets();
    }
}