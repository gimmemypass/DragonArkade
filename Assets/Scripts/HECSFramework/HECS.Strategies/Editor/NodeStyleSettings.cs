using UnityEditor;
using UnityEngine;

public class NodeStyleSettings
{
    public GUIStyle nodeStyle;
    public GUIStyle selectedNodeStyle;
    public GUIStyle inPointStyle;
    public GUIStyle outPointStyle;
    public GUIStyle WindowNodeStyle;
    public GUIStyle topperStyle;
    public GUIStyle closeButtonStyle;

    public Texture closeIcon;
    public Texture inputIcon;
    public Texture linklIcon;
    public Texture outIcon;

    public NodeStyleSettings()
    {
        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        nodeStyle.border = new RectOffset(1, 1, 1, 1);

        selectedNodeStyle = new GUIStyle();
        selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
        selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

        inPointStyle = new GUIStyle();
        inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        inPointStyle.border = new RectOffset(4, 4, 12, 12);

        outPointStyle = new GUIStyle();
        outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        outPointStyle.border = new RectOffset(4, 4, 12, 12);


        WindowNodeStyle = new GUIStyle();
        WindowNodeStyle.fontSize = 16;
        WindowNodeStyle.alignment =  TextAnchor.UpperLeft;
        WindowNodeStyle.richText = true;

        closeIcon = AssetDatabase.LoadAssetAtPath<Texture>("Packages/com.unity.render-pipelines.core/Editor/LookDev/Icons/Remove.png");
        inputIcon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Configs/Icons/InputNode.png");
        linklIcon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Configs/Icons/LinkNode.png");
        outIcon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Configs/Icons/OutNode.png");

        closeButtonStyle = new GUIStyle();

        topperStyle = new GUIStyle();
        topperStyle.padding = new RectOffset(1, 1, 1, 1);
        topperStyle.margin = new RectOffset(1, 1, 1, 1);
        topperStyle.border = new RectOffset(5, 5, 5, 5);
    }
}
