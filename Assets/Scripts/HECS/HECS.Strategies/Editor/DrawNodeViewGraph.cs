using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HECSFramework.Core;
using HECSFramework.Core.Helpers;
using HECSFramework.Unity;
using HECSFramework.Unity.Helpers;
using Sirenix.Utilities;
using Strategies;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;
using Direction = UnityEditor.Experimental.GraphView.Direction;
using Toggle = UnityEngine.UIElements.Toggle;

public class DrawNodeViewGraph : Node
{
    public string GUID;
    public string Name;
    public BaseDecisionNode InnerNode;
    public Dictionary<Port, (MemberInfo member, Direction direction, BaseDecisionNode node)> ConnectedPorts = new Dictionary<Port, (MemberInfo member, Direction direction, BaseDecisionNode node)>();

    public void ClearConnections()
    {
        foreach (var connection in ConnectedPorts)
        {
            if (connection.Value.member is FieldInfo fieldInfo)
            {
                fieldInfo.SetValue(InnerNode, null);
            }
        }

        InnerNode.ConnectionContexts.Clear();
    }
}

public struct CopyNode
{
    public Type Type;
    public Vector3 Coord;
    public string Data;
}

public class StrategyGraphView : GraphView, IDisposable
{
    private readonly BaseStrategy strategy;
    private readonly string path;
    private readonly StrategyGraphViewWIndow strategyGraphViewWIndow;
    private List<DrawNodeViewGraph> drawNodes = new List<DrawNodeViewGraph>(16);
    private NodeSearchWindow _searchWindow;
    public static List<CopyNode> copyNodes = new List<CopyNode>();
    private MouseUpEvent mouseData;
    private Vector2 mousePosition;
    private GridBackground grid;

    public StrategyGraphView(BaseStrategy strategy, string path, StrategyGraphViewWIndow strategyGraphViewWIndow)
    {
        styleSheets.Add(Resources.Load<StyleSheet>("StrategiesGraph"));

        SetupZoom(0.01f, 2f);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        this.AddManipulator(new FreehandSelector());

        grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();
        this.strategy = strategy;
        this.path = path;
        this.strategyGraphViewWIndow = strategyGraphViewWIndow;
        AddElement(EntryPoint(strategy));

        foreach (var dn in strategy.nodes)
        {
            if (dn is StartDecision)
                continue;

            AddNodeToDecision(dn);
        }

        ConnectStrategyNodes();
        graphViewChanged += Changes;
        RegisterCallback<MouseMoveEvent>(MouseReact);
        RegisterCallback<KeyDownEvent>(KeyboradEventProcess);

        DelayFrameAll();
    }

    private void KeyboradEventProcess(KeyDownEvent evt)
    {
        if (evt.ctrlKey && evt.shiftKey && evt.keyCode == KeyCode.A)
        {
            SelectAllChilds();
        }

        if (evt.ctrlKey && evt.shiftKey && evt.keyCode == KeyCode.Z)
        {
            SelectAllParents();
        }
    }

    private void SelectAllParents()
    {
        var currentSelection = selection.FirstOrDefault(x => x is DrawNodeViewGraph) as DrawNodeViewGraph;

        if (currentSelection != null)
        {
            foreach (var c in currentSelection.ConnectedPorts)
            {
                if (c.Value.direction == Direction.Input)
                {
                    var needed = drawNodes.FirstOrDefault(x => x.InnerNode == c.Value.node);

                    if (needed != null)
                        AddToSelection(needed);
                    else
                        continue;

                    RecursiveSelection(needed.ConnectedPorts, Direction.Input);
                }
            }
        }
    }

    private void SelectAllChilds()
    {
        var currentSelection = selection.FirstOrDefault(x=> x is DrawNodeViewGraph) as DrawNodeViewGraph;

        if (currentSelection != null)
        {
            foreach (var c in currentSelection.ConnectedPorts)
            {
                if (c.Value.direction == Direction.Output)
                {
                    var needed = drawNodes.FirstOrDefault(x => x.InnerNode == c.Value.node);

                    if (needed != null)
                    {
                        AddToSelection(needed);
                        RecursiveSelection(needed.ConnectedPorts, Direction.Output);
                    }
                }
            }
        }
    }

    private void RecursiveSelection(Dictionary<Port, (MemberInfo member, Direction direction, BaseDecisionNode node)> connections, Direction direction)
    {
        foreach (var c in connections)
        {
            if (c.Value.direction == direction)
            {
                var node = c.Value.node;

                var needed = drawNodes.FirstOrDefault(x => x.InnerNode == node);

                if (needed == null)
                    continue;

                AddToSelection(needed);
                RecursiveSelection(needed.ConnectedPorts, direction);
            }
        }
    }

    private void MouseReact(MouseMoveEvent evt)
    {
        mousePosition = evt.localMousePosition;
    }

    /// this because graph dont calculate data properly yet, and we need w8 for something
    private async void DelayFrameAll()
    {
        await Task.Delay(80);
        FrameAll();
    }

    protected void DuplicateNode()
    {
        var newSelection = new List<ISelectable>();

        foreach (var node in selection)
        {
            if (node is DrawNodeViewGraph drawNode)
            {
                var toJSON = JsonUtility.ToJson(drawNode.InnerNode);
                var newNode = AddNodeToStrategyGraph(drawNode.GetPosition().center + new Vector2(2, 0), drawNode.InnerNode.GetType());
                JsonUtility.FromJsonOverwrite(toJSON, newNode.node);
                newSelection.Add(newNode.drawNode);
                newNode.drawNode.ClearConnections();
            }
        }

        ClearSelection();

        foreach (var n in newSelection)
            AddToSelection(n);
    }

    public override void AddToSelection(ISelectable selectable)
    {
        base.AddToSelection(selectable);

        if (selectable is DrawNodeViewGraph drawNode)
        {
            EditorGUIUtility.PingObject(drawNode.InnerNode);
            Selection.activeObject = drawNode.InnerNode;
        }
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        base.BuildContextualMenu(evt);
        evt.menu.InsertAction(1, "Duplicate", (x) => DuplicateNode());
        evt.menu.InsertAction(2, "Copy", (x) => CopyNodes());
        evt.menu.InsertAction(3, "Paste Nodes", (x) => PasteNode());
    }

    private void CopyNodes()
    {
        copyNodes.Clear();

        var mousePosition = GetMousePosition();

        foreach (var n in selection)
        {
            if (n is DrawNodeViewGraph draw)
            {
                copyNodes.Add(new CopyNode
                {
                    Coord = draw.InnerNode.coords - mousePosition,
                    Data = JsonUtility.ToJson(draw.InnerNode),
                    Type = draw.InnerNode.GetType(),
                });
            }
        }
    }

    private Vector2 GetMousePosition()
    {
        var mousePosition = grid.ChangeCoordinatesTo(contentViewContainer, this.mousePosition);

        return mousePosition;
    }

    private void PasteNode()
    {
        ClearSelection();
        var toSelection = new List<ISelectable>();
        var mousePos = GetMousePosition();

        foreach (var n in copyNodes)
        {
            var newNode = AddNodeToStrategyGraph((Vector3)mousePos + n.Coord, n.Type);
            JsonUtility.FromJsonOverwrite(n.Data, newNode.node);
            toSelection.Add(newNode.drawNode);
            newNode.drawNode.ClearConnections();
        }

        foreach (var s in toSelection)
            AddToSelection(s);
    }

    /// <summary>
    /// here we react on drag and drop connections and set values to nodes
    /// </summary>
    /// <param name="graphViewChange"></param>
    /// <returns></returns>
    private GraphViewChange Changes(GraphViewChange graphViewChange)
    {

        //moving and update coords of node
        if (graphViewChange.movedElements != null)
        {
            foreach (var moved in graphViewChange.movedElements)
            {
                if (moved is DrawNodeViewGraph drawNode)
                    drawNode.InnerNode.coords = drawNode.GetPosition().position;
            }
        }

        //create connection
        if (graphViewChange.edgesToCreate != null)
        {
            foreach (var e in graphViewChange.edgesToCreate)
            {
                var input = NeededNode(e.input, Direction.Input);
                var output = NeededNode(e.output, Direction.Output);

                try
                {
                    var inputType = (input.ConnectedPorts[e.input].member as FieldInfo).FieldType;
                    var outType = (output.ConnectedPorts[e.output].member as FieldInfo).FieldType;

                    var check = output.InnerNode.GetType().InheritsFrom(inputType);

                    if (IsValidConnect(((FieldInfo)input.ConnectedPorts[e.input].member), ((FieldInfo)output.ConnectedPorts[e.output].member)))
                    {
                        ((FieldInfo)output.ConnectedPorts[e.output].member).SetValue(output.InnerNode, input.InnerNode);
                        ((FieldInfo)input.ConnectedPorts[e.input].member).SetValue(input.InnerNode, output.InnerNode);

                        output.InnerNode.ConnectionContexts.AddOrRemoveElement(new ConnectionContext { Out = output.ConnectedPorts[e.output].member.Name, In = input.ConnectedPorts[e.input].member.Name }, true);
                    }
                    else if (check && inputType != typeof(BaseDecisionNode))
                    {
                        ((FieldInfo)output.ConnectedPorts[e.output].member).SetValue(output.InnerNode, input.InnerNode);
                        ((FieldInfo)input.ConnectedPorts[e.input].member).SetValue(input.InnerNode, output.InnerNode);

                        output.InnerNode.ConnectionContexts.AddOrRemoveElement(new ConnectionContext { Out = output.ConnectedPorts[e.output].member.Name, In = input.ConnectedPorts[e.input].member.Name }, true);
                    }
                    else
                    {
                        Debug.LogWarning("nodes not match");
                        graphViewChange.edgesToCreate.Remove(e);
                        return default;
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogWarning("nodes not match " + ex.Message);
                    graphViewChange.edgesToCreate.Remove(e);
                    return default;
                }
            }
        }

        //clean connection
        if (graphViewChange.elementsToRemove != null)
        {
            foreach (var remove in graphViewChange.elementsToRemove)
            {
                if (remove is Edge edge)
                {
                    foreach (var dn in drawNodes)
                    {
                        if (dn.ConnectedPorts.TryGetValue(edge.output, out var info))
                        {
                            var nameOut = ((FieldInfo)info.member).Name;
                            var neededNode = drawNodes.FirstOrDefault(x => x.InnerNode == info.node);

                            if (neededNode != null)
                            {
                                if (neededNode.ConnectedPorts.TryGetValue(edge.input, out var info2))
                                {
                                    var nameInput = info2.member.Name;
                                    dn.InnerNode.ConnectionContexts.Remove(new ConnectionContext { In = nameInput, Out = nameOut });
                                    ((FieldInfo)info2.member).SetValue(info.node, null);
                                }
                            }

                            ((FieldInfo)info.member).SetValue(dn.InnerNode, null);
                        }
                    }
                }

                if (remove is DrawNodeViewGraph node)
                {
                    strategy.nodes.AddOrRemoveElement(node.InnerNode, false);
                    ClearNodes();
                }
            }
        }

        return graphViewChange;
    }

    private bool IsValidConnect(FieldInfo input, FieldInfo output)
    {
        var attr = output.GetAttribute<ConnectionAttribute>();
        var comment = attr.NameOfField.ToLower();

        if (input.FieldType.IsGenericType)
        {
            foreach (var g in input.FieldType.GetGenericArguments())
            {
                var name = FromDotNetTypeToCSharpType(g.Name);

                if (comment.Contains(name.ToLower()))
                    return true;
            }

            return false;
        }

        if (comment.Contains("<") && comment.Contains(">"))
            return false;

        return true;
    }

    public static string FromDotNetTypeToCSharpType(string dotNetTypeName, bool isNull = false)
    {
        string cstype = "";
        string nullable = isNull ? "?" : "";
        string prefix = "System.";
        string typeName = dotNetTypeName.StartsWith(prefix) ? dotNetTypeName.Remove(0, prefix.Length) : dotNetTypeName;

        switch (typeName)
        {
            case "Boolean": cstype = "bool"; break;
            case "Byte": cstype = "byte"; break;
            case "SByte": cstype = "sbyte"; break;
            case "Char": cstype = "char"; break;
            case "Decimal": cstype = "decimal"; break;
            case "Double": cstype = "double"; break;
            case "Single": cstype = "float"; break;
            case "Int32": cstype = "int"; break;
            case "UInt32": cstype = "uint"; break;
            case "Int64": cstype = "long"; break;
            case "UInt64": cstype = "ulong"; break;
            case "Object": cstype = "object"; break;
            case "Int16": cstype = "short"; break;
            case "UInt16": cstype = "ushort"; break;
            case "String": cstype = "string"; break;

            default: cstype = typeName; break; // do nothing
        }
        return $"{cstype}{nullable}";

    }


    private DrawNodeViewGraph NeededNode(Port port, Direction direction)
    {
        foreach (var dn in drawNodes)
        {
            foreach (var portInfo in dn.ConnectedPorts)
            {
                if (portInfo.Key == port && portInfo.Value.direction == direction)
                    return dn;
            }
        }

        return null;
    }

    private void ConnectStrategyNodes()
    {
        foreach (var dn in drawNodes)
        {
            foreach (var portinfo in dn.ConnectedPorts)
            {
                if (portinfo.Value.direction == Direction.Input)
                    continue;

                if (portinfo.Value.node == null)
                    continue;

                var viewNode = drawNodes.FirstOrDefault(x => x.InnerNode == portinfo.Value.node);
                var connect = dn.InnerNode.ConnectionContexts.FirstOrDefault(x => x.Out == portinfo.Value.member.Name);
                
                if (string.IsNullOrEmpty(connect.In) || string.IsNullOrEmpty(connect.Out))
                {
                    LinkNodesTogether(portinfo.Key, viewNode.ConnectedPorts.FirstOrDefault(x=> x.Value.direction == Direction.Input).Key);
                }
                else
                {
                    var connectToPort = viewNode.ConnectedPorts.FirstOrDefault(x => x.Value.direction == Direction.Input && x.Value.member.Name == connect.In);

                    if (connectToPort.Key == null)
                    {
                        LinkNodesTogether(portinfo.Key, viewNode.ConnectedPorts.FirstOrDefault(x => x.Value.direction == Direction.Input).Key);
                    }
                    else
                    {
                        LinkNodesTogether(portinfo.Key, connectToPort.Key);
                    }
                }
            }
        }
    }

    private void LinkNodesTogether(Port outputSocket, Port inputSocket)
    {
        var tempEdge = new Edge()
        {
            output = outputSocket,
            input = inputSocket
        };

        tempEdge?.input.Connect(tempEdge);
        tempEdge?.output.Connect(tempEdge);

        tempEdge?.styleSheets.Add(Resources.Load<StyleSheet>("StrategiesGraph"));
        this.Add(tempEdge);
    }

    private DrawNodeViewGraph EntryPoint(BaseStrategy strategy)
    {
        var node = new DrawNodeViewGraph();
        var startNode = strategy.nodes.FirstOrDefault(x => x is StartDecision);

        if (startNode == null)
        {
            OnClickAddNode(Vector2.zero, typeof(StartDecision));
            startNode = strategy.nodes.FirstOrDefault(x => x is StartDecision);
        }

        node.title = startNode.TitleOfNode;
        node.GUID = Guid.NewGuid().ToString();
        node.InnerNode = startNode;

        node.SetPosition(new Rect(startNode.coords, new Vector2(150, 50)));
        GeneratePorts(node);
        drawNodes.Add(node);
        return node;
    }

    private DrawNodeViewGraph AddNodeToDecision(BaseDecisionNode node)
    {
        var drawNode = new DrawNodeViewGraph();

        drawNode.title = node.TitleOfNode;
        drawNode.GUID = Guid.NewGuid().ToString();
        drawNode.InnerNode = node;
        drawNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        drawNode.SetPosition(new Rect(node.coords, new Vector2(150, 50)));
        GeneratePorts(drawNode);
        GenerateCustomField(drawNode);

        AddElement(drawNode);
        drawNodes.Add(drawNode);
        return drawNode;
    }

    private void GenerateCustomField(DrawNodeViewGraph drawNode)
    {
        var so = new SerializedObject(drawNode.InnerNode);

        if (drawNode.InnerNode != null)
        {
            if (drawNode.InnerNode.GetType().GetCustomAttributes().Any(x => x is DrawInNodeAttribute))
            {
                drawNode.contentContainer.Add(CreateEditorFromNodeData(drawNode.InnerNode));
                drawNode.RefreshPorts();
                drawNode.RefreshExpandedState();
            }
            else

                foreach (var m in drawNode.InnerNode.GetType().GetMembers())
                {
                    foreach (var a in m.GetCustomAttributes())
                    {
                        if (a is ComponentMaskDropDownAttribute)
                        {
                            var field = m as FieldInfo;
                            var componentsList = new TypesProvider().MapIndexes.OrderBy(x => x.Value.ComponentName);

                            var newList = new List<string>(512);

                            foreach (var c in componentsList)
                            {
                                newList.Add(c.Value.ComponentName);
                            }

                            var currentValue = (int)field.GetValue(drawNode.InnerNode);

                            if (currentValue == 0 && componentsList != null && componentsList.Count() > 0)
                            {
                                currentValue = componentsList.First().Key;
                                field.SetValue(drawNode.InnerNode, currentValue);
                            }

                            var lookForCurrentStringName = componentsList.FirstOrDefault(x => x.Key == currentValue);

                            var defaultIndex = lookForCurrentStringName.Key == 0 ? 0 : newList.IndexOf(lookForCurrentStringName.Value.ComponentName);

                            var dropDown = new DropdownField("Choose component", newList, defaultIndex);

                            var button = new Button(() => ReactOnComponentSearchClick(dropDown)) { text = "Search" };
                            dropDown.Add(button);
                            dropDown.RegisterValueChangedCallback((evt) => ComponentsDropDownReact(evt, field, drawNode.InnerNode));
                            drawNode.contentContainer.Add(dropDown);
                        }

                        if (a is DrawEntitiesFilterAttribute)
                        {
                            var field = m as FieldInfo;
                            var label = new Label($"{field.Name}");

                            var filter = (Filter)field.GetValue(drawNode.InnerNode);

                            drawNode.contentContainer.Add(label);

                            for (int i = 0; i < filter.Lenght; i++)
                            {
                                if (TypesMap.GetComponentInfo(filter[i], out var componentInfo))
                                {
                                    var textField = new Label($"{componentInfo.ComponentName}");
                                    textField.style.scale = new StyleScale { value = new Scale { value = Vector3.one * 0.8f } };
                                    drawNode.contentContainer.Add(textField);
                                }
                            }

                            DrawFilter(m, drawNode, so);
                        }

                        if (a is AnimParameterDropDownAttribute)
                        {
                            DrawAnimParameterDropDown(m, drawNode);
                        }

                        if (a is AbilityIDDropDownAttribute)
                        {
                            DrawAbilityDropDown(m, drawNode);
                        }

                        if (a is DropDownIdentifierAttribute dropDownIdentifierAttribute)
                        {
                            DrawIdentifierDropDown(m, drawNode, dropDownIdentifierAttribute.IdentifierType);
                        }

                        if (a is SerializeField || a is ExposeFieldAttribute)
                        {
                            var field = m as FieldInfo;

                            if (field.FieldType == typeof(int))
                            {
                                var intField = new IntegerField(m.Name + ":");
                                intField.value = (int)(m as FieldInfo).GetValue(drawNode.InnerNode);

                                intField.RegisterValueChangedCallback((evt) => IntChangeReact(evt, m as FieldInfo, drawNode.InnerNode));
                                drawNode.contentContainer.Add(intField);
                            }
                            else if (field.FieldType.BaseType == typeof(Enum))
                            {
                                var enumField = new EnumField(m.Name + ":");
                                var property = so.FindProperty(m.Name);
                                enumField.value = (Enum)Enum.ToObject(field.FieldType, field.GetValue(drawNode.InnerNode));
                                enumField.Init((Enum)Enum.ToObject(field.FieldType, field.GetValue(drawNode.InnerNode)));

                                enumField.RegisterValueChangedCallback((evt) => EnumChangeReact(evt, field, drawNode.InnerNode));
                                drawNode.contentContainer.Add(enumField);
                            }
                            else if (field.FieldType == typeof(float))
                            {
                                var floatField = new FloatField(m.Name + ":");
                                floatField.value = (float)field.GetValue(drawNode.InnerNode);
                                floatField.RegisterValueChangedCallback((evt) => FloatFieldReact(evt, field, drawNode.InnerNode));
                                floatField.style.minWidth = 20;
                                drawNode.contentContainer.Add(floatField);
                            }
                            else if (field.FieldType == typeof(string))
                            {
                                var floatField = new TextField(m.Name + ":");
                                floatField.value = (string)field.GetValue(drawNode.InnerNode);
                                floatField.RegisterValueChangedCallback((evt) => StringFieldReact(evt, field, drawNode.InnerNode));
                                drawNode.contentContainer.Add(floatField);
                            }
                            else if (field.FieldType == typeof(Vector3))
                            {
                                var floatField = new Vector3Field(m.Name + ":");
                                floatField.value = (Vector3)field.GetValue(drawNode.InnerNode);
                                floatField.RegisterValueChangedCallback((evt) => UpdateField(evt, field, drawNode.InnerNode));
                                drawNode.contentContainer.Add(floatField);
                            }
                            else if (field.FieldType == typeof(bool))
                            {
                                var boolField = new Toggle(m.Name + ":");
                                boolField.value = (bool)field.GetValue(drawNode.InnerNode);
                                boolField.RegisterValueChangedCallback((evt) => BoolFieldReact(evt, field, drawNode.InnerNode));
                                drawNode.contentContainer.Add(boolField);
                            }
                            else
                            {
                                var objectField = new ObjectField(m.Name + ":") { objectType = field.FieldType };
                                var value = (m as FieldInfo).GetValue(drawNode.InnerNode) as UnityEngine.Object;

                                objectField.value = value;
                                objectField.RegisterValueChangedCallback((evt) => SetEntityContainer(evt, m as FieldInfo, drawNode.InnerNode));
                                drawNode.contentContainer.Add(objectField);
                            }

                            drawNode.RefreshPorts();
                            drawNode.RefreshExpandedState();
                        }
                    }
                }
        }
    }

    private void DrawFilter(MemberInfo m, DrawNodeViewGraph drawNode, SerializedObject so)
    {
        var button = new Button(() => ReactOnFilterClick(m, drawNode, so)) { text = m.Name };
        drawNode.Add(button);
    }

    private void ReactOnFilterClick(MemberInfo memberInfo, DrawNodeViewGraph drawNode, SerializedObject so)
    {
        var window = EditorWindow.GetWindow<SetFilterWindow>();
        window.DrawFilter(memberInfo, drawNode, so);
    }

    private void DrawAnimParameterDropDown(MemberInfo m, DrawNodeViewGraph drawNode)
    {
        var field = m as FieldInfo;
        var abilitiesList = AnimParametersMap.AnimParameters.OrderBy(x => x.Key);

        var newList = new List<string>(512);

        foreach (var c in abilitiesList)
        {
            newList.Add(c.Key);
        }

        var currentValue = (int)field.GetValue(drawNode.InnerNode);

        if (currentValue == 0 && abilitiesList != null && abilitiesList.Count() > 0)
        {
            currentValue = abilitiesList.First().Value;
            field.SetValue(drawNode.InnerNode, currentValue);
        }

        var lookForCurrentStringName = abilitiesList.FirstOrDefault(x => x.Value == currentValue);

        var defaultIndex = lookForCurrentStringName.Value == 0 ? 0 : newList.IndexOf(lookForCurrentStringName.Key);

        var dropDown = new DropdownField("Select parameter", newList, defaultIndex);

        var button = new Button(() => ReactOnComponentSearchClick(dropDown)) { text = "Search" };
        dropDown.Add(button);
        dropDown.RegisterValueChangedCallback((evt) => AnimParameterDropDownReact(evt, field, drawNode.InnerNode));
        drawNode.contentContainer.Add(dropDown);
    }

    private void DrawIdentifierDropDown(MemberInfo m, DrawNodeViewGraph drawNode, string identifierType)
    {
        var field = m as FieldInfo;
        var neededType = Type.GetType(identifierType);

        //if (neededType == null)
        //{
        //    throw new Exception(drawNode.name + " looks for missed type " + identifierType);
        //}

        var abilitiesList = new SOProvider<IdentifierContainer>().GetCollection().ToList();

        foreach (var a in abilitiesList.ToArray())
        {
            if (a.GetType().Name != identifierType)
                abilitiesList.Remove(a);
        }

        var newList = new List<string>(512);

        foreach (var c in abilitiesList)
        {
            newList.Add(c.name);
        }

        var currentValue = (int)field.GetValue(drawNode.InnerNode);

        if (currentValue == 0 && abilitiesList != null && abilitiesList.Count() > 0)
        {
            currentValue = abilitiesList.First().Id;
            field.SetValue(drawNode.InnerNode, currentValue);
        }

        var lookForCurrentStringName = abilitiesList.FirstOrDefault(x => x.Id == currentValue);

        var defaultIndex = lookForCurrentStringName == null ? 0 : newList.IndexOf(lookForCurrentStringName.name);

        var dropDown = new DropdownField("Select identifier", newList, defaultIndex);

        var button = new Button(() => ReactOnComponentSearchClick(dropDown)) { text = "Search" };
        dropDown.Add(button);
        dropDown.RegisterValueChangedCallback((evt) => IdentifierDropDownReact(evt, field, drawNode.InnerNode));
        drawNode.contentContainer.Add(dropDown);
    }

    private void DrawAbilityDropDown(MemberInfo m, DrawNodeViewGraph drawNode)
    {
        var field = m as FieldInfo;
        var abilitiesList = AbilitiesMap.AbilitiesToIdentifiersMap.OrderBy(x => x.Key);

        var newList = new List<string>(512);

        foreach (var c in abilitiesList)
        {
            newList.Add(c.Key);
        }

        var currentValue = (int)field.GetValue(drawNode.InnerNode);

        if (currentValue == 0 && abilitiesList != null && abilitiesList.Count() > 0)
        {
            currentValue = abilitiesList.First().Value;
            field.SetValue(drawNode.InnerNode, currentValue);
        }

        var lookForCurrentStringName = abilitiesList.FirstOrDefault(x => x.Value == currentValue);

        var defaultIndex = lookForCurrentStringName.Value == 0 ? 0 : newList.IndexOf(lookForCurrentStringName.Key);

        var dropDown = new DropdownField("Select ability", newList, defaultIndex);

        var button = new Button(() => ReactOnComponentSearchClick(dropDown)) { text = "Search" };
        dropDown.Add(button);
        dropDown.RegisterValueChangedCallback((evt) => AbilityDropDownReact(evt, field, drawNode.InnerNode));
        drawNode.contentContainer.Add(dropDown);
    }

    private void IdentifierDropDownReact(ChangeEvent<string> evt, FieldInfo field, BaseDecisionNode innerNode)
    {
        var identifier = IndexGenerator.GenerateIndex(evt.newValue);
        field.SetValue(innerNode, identifier);
    }

    private void AbilityDropDownReact(ChangeEvent<string> evt, FieldInfo field, BaseDecisionNode innerNode)
    {
        var abilityIndex = AbilitiesMap.AbilitiesToIdentifiersMap[evt.newValue];
        field.SetValue(innerNode, abilityIndex);
    }

    private void AnimParameterDropDownReact(ChangeEvent<string> evt, FieldInfo field, BaseDecisionNode innerNode)
    {
        var abilityIndex = AnimParametersMap.AnimParameters[evt.newValue];
        field.SetValue(innerNode, abilityIndex);
    }


    private void ReactOnComponentSearchClick(DropdownField dropDown)
    {
        var winow = EditorWindow.GetWindow<SearchComponentsWindow>();
        winow.Init(dropDown);
    }

    private void ComponentsDropDownReact(ChangeEvent<string> evt, FieldInfo field, BaseDecisionNode innerNode)
    {
        var mask = new TypesProvider().MapIndexes[IndexGenerator.GenerateIndex(evt.newValue)];
        field.SetValue(innerNode, mask.ComponentsMask.TypeHashCode);
    }

    private VisualElement CreateEditorFromNodeData<T>(T obj) where T : UnityEngine.Object
    {
        SerializedObject soEditor = new UnityEditor.SerializedObject(obj);
        var container = new VisualElement();

        var it = soEditor.GetIterator();
        if (!it.NextVisible(true))
            return container;

        //Descends through serialized property children & allows us to edit them.
        do
        {
            var typeofProperty = it.serializedObject.targetObject.GetType();
            var neededType = typeofProperty.GetField(it.propertyPath, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (neededType != null)
            {
                if (neededType.GetCustomAttributes().Any(x => x is IgnoreDrawAttribute))
                    continue;
            }

            var propertyField = new PropertyField(it.Copy())
            { name = "PropertyField:" + it.propertyPath };

            //Bind the property so we can edit the values.
            propertyField.Bind(soEditor);

            //This ignores the label name field, it's ugly.
            if (it.propertyPath == "m_Script" && soEditor.targetObject != null)
            {
                propertyField.SetEnabled(false);
                propertyField.visible = false;
            }

            container.Add(propertyField);
        }
        while (it.NextVisible(false));

        return container;
    }

    private void UpdateField<T>(ChangeEvent<T> evt, FieldInfo field, BaseDecisionNode innerNode)
    {
        field.SetValue(innerNode, evt.newValue);
    }

    private void StringFieldReact(ChangeEvent<string> evt, FieldInfo field, BaseDecisionNode innerNode)
    {
        field.SetValue(innerNode, evt.newValue);
    }

    private void SetEntityContainer(ChangeEvent<UnityEngine.Object> evt, FieldInfo field, BaseDecisionNode baseDecisionNode)
    {
        field.SetValue(baseDecisionNode, evt.newValue);
    }

    private void BoolFieldReact(ChangeEvent<bool> evt, FieldInfo fieldInfo, BaseDecisionNode baseDecisionNode)
    {
        fieldInfo.SetValue(baseDecisionNode, evt.newValue);
    }

    private void IntChangeReact(ChangeEvent<int> evt, FieldInfo fieldInfo, BaseDecisionNode baseDecisionNode)
    {
        fieldInfo.SetValue(baseDecisionNode, evt.newValue);
    }

    private void FloatFieldReact(ChangeEvent<float> evt, FieldInfo fieldInfo, BaseDecisionNode baseDecisionNode)
    {
        fieldInfo.SetValue(baseDecisionNode, evt.newValue);
    }

    private void EnumChangeReact(ChangeEvent<Enum> evt, FieldInfo fieldInfo, BaseDecisionNode baseDecisionNode)
    {
        fieldInfo.SetValue(baseDecisionNode, evt.newValue);
    }

    private void ChangeStrategy(ChangeEvent<UnityEngine.Object> evt, FieldInfo field, BaseDecisionNode baseDecisionNode)
    {
        field.SetValue(baseDecisionNode, evt.newValue);
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();
        var startPortView = startPort;

        ports.ForEach((port) =>
        {
            var portView = port;
            if (startPortView != portView && startPortView.node != portView.node)
                compatiblePorts.Add(port);
        });

        return compatiblePorts;
    }

    private void GeneratePorts(DrawNodeViewGraph drawNode)
    {
        var members = drawNode.InnerNode.GetType().GetMembers();

        foreach (var m in members)
        {
            var atrs = m.GetCustomAttributes();

            foreach (var a in atrs)
            {
                if (a is ConnectionAttribute connection)
                {
                    var nextNode = GetNodeFromField(m, drawNode.InnerNode);

                    switch (connection.ConnectionPointType)
                    {
                        case ConnectionPointType.In:
                            var port = GeneratePort(drawNode, Direction.Input, connection.NameOfField, Port.Capacity.Single);
                            drawNode.ConnectedPorts.Add(port, (m, Direction.Input, nextNode));
                            break;

                        case ConnectionPointType.Out:
                            var port2 = GeneratePort(drawNode, Direction.Output, connection.NameOfField, Port.Capacity.Single);
                            drawNode.ConnectedPorts.Add(port2, (m, Direction.Output, nextNode));
                            break;
                    }
                }
            }
        }
    }

    private BaseDecisionNode GetNodeFromField(MemberInfo memberInfo, BaseDecisionNode baseDecisionNode)
    {
        if (memberInfo is FieldInfo field)
            return field.GetValue(baseDecisionNode) as BaseDecisionNode;
        else if (memberInfo is PropertyInfo property)
            return property.GetValue(baseDecisionNode) as BaseDecisionNode;

        return null;
    }

    private Port GeneratePort(DrawNodeViewGraph node, Direction direction, string portName, Port.Capacity capacity = Port.Capacity.Single)
    {
        var port = node.InstantiatePort(Orientation.Horizontal, direction, capacity, typeof(BaseDecisionNode));
        port.portName = portName;
        node.outputContainer.Add(port);

        node.RefreshExpandedState();
        node.RefreshPorts();
        return port;
    }

    public void AddSearchWindow(StrategyGraphViewWIndow editorWindow)
    {
        _searchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
        _searchWindow.Configure(editorWindow, this);
        nodeCreationRequest = context =>
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition, requestedWidth: 800), _searchWindow);
    }

    public (BaseDecisionNode node, DrawNodeViewGraph drawNode) AddNodeToStrategyGraph(Vector2 position, Type type)
    {
        var newNode = OnClickAddNode(position, type);
        var drawNode = AddNodeToDecision(newNode);
        return (newNode, drawNode);
    }

    private BaseDecisionNode OnClickAddNode(Vector2 mousePosition, Type type)
    {
        if (strategy.nodes == null)
            strategy.nodes = new List<BaseDecisionNode>(10);

        var asset = ScriptableObject.CreateInstance(type);
        var parent = AssetDatabase.LoadAssetAtPath<BaseStrategy>(path);
        asset.name = type.ToString();
        AssetDatabase.AddObjectToAsset(asset, parent);
        strategy.nodes.Add(asset as BaseDecisionNode);
        AssetDatabase.SaveAssets();
        (asset as BaseDecisionNode).coords = mousePosition;

        return asset as BaseDecisionNode;
    }

    private void ClearNodes()
    {
        var path = AssetDatabase.GetAssetPath(strategy);
        var allSo = AssetDatabase.LoadAllAssetRepresentationsAtPath(path);

        foreach (var go in allSo)
        {
            if (!strategy.nodes.Contains(go))
            {
                if (go == null)
                    continue;

                AssetDatabase.RemoveObjectFromAsset(go);
                MonoBehaviour.DestroyImmediate(go);
            }
        }

        AssetDatabase.SaveAssets();
    }


    public void Dispose()
    {
        graphViewChanged -= Changes;
    }
}
