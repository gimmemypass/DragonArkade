using System;
using System.Collections.Generic;
using HECSFramework.Core.Generator;
using HECSFramework.Unity.Editor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Strategies;
using UnityEditor;

namespace HECSFramework.Unity
{
    public class CreateNodeWindow : OdinEditorWindow
    {
        private static string ConstructedNodes = "/ConstructedNodes/";

        public string NodeName;
        public NodeType NodeType;

        [ShowIf("NodeType", NodeType.GenericNode)]
        public string GenericType;

        public List<CreateFieldInfo> createFieldInfos = new List<CreateFieldInfo>();

        [MenuItem("HECS Options/Helpers/Create Strategies Node")]
        public static void ShowCreateNodeWindow()
        {
            GetWindow<CreateNodeWindow>();
        }

        [Button]
        private void GenerateNode()
        {
            if (string.IsNullOrEmpty(NodeName))
                return;

            ISyntax treeSyntaxNode = default;
            ISyntax fieldsInput = default;
            ISyntax fieldsOut = default;
            ISyntax body = default;

            switch (NodeType)
            {
                case NodeType.Dilemma:
                    GetDillema(NodeName, out treeSyntaxNode, out fieldsInput, out fieldsOut, out body);
                    break;
                case NodeType.InterDecision:
                    GetInterDecision(NodeName, out treeSyntaxNode, out fieldsInput, out fieldsOut, out body);
                    break;
                case NodeType.GenericNode:
                    GetGenericNode(NodeName, out treeSyntaxNode, out fieldsInput, out fieldsOut, out body);
                    break;
            }

            foreach (var f in createFieldInfos)
            {
                switch (f.ConnectionPointType)
                {
                    case ConnectionPointType.In:
                        fieldsInput.Tree.Add(GetInputField(f));
                        break;
                    case ConnectionPointType.Out:
                        fieldsOut.Tree.Add(GetOutField(f));
                        break;
                }
            }

            InstallHECS.CheckFolder(InstallHECS.ScriptPath + InstallHECS.HECSGenerated + ConstructedNodes);
            InstallHECS.SaveToFile(treeSyntaxNode.ToString(), InstallHECS.ScriptPath + InstallHECS.HECSGenerated + ConstructedNodes + $"{NodeName}.cs");
        }

        private ISyntax GetInputField(CreateFieldInfo createFieldInfo)
        {
            var tree = new TreeSyntaxNode();
            tree.Add(new TabSimpleSyntax(2, $"[Connection(ConnectionPointType.In, {CParse.Quote}<{createFieldInfo.Type}> {createFieldInfo.Name}{CParse.Quote})]"));
            tree.Add(new TabSimpleSyntax(2, $"public GenericNode<{createFieldInfo.Type}> {createFieldInfo.Name.Replace(" ", "")};"));
            return tree;
        }

        private ISyntax GetOutField(CreateFieldInfo createFieldInfo)
        {
            var tree = new TreeSyntaxNode();
            tree.Add(new TabSimpleSyntax(2, $"[Connection(ConnectionPointType.Out, {CParse.Quote}<{createFieldInfo.Type}> {createFieldInfo.Name}{CParse.Quote})]"));
            tree.Add(new TabSimpleSyntax(2, $"public BaseDecisionNode {createFieldInfo.Name.Replace(" ", "")};"));
            return tree;
        }

        private void GetDillema(string name, out ISyntax node, out ISyntax fieldsInput, out ISyntax fieldsOut, out ISyntax body)
        {
            node = new TreeSyntaxNode();
            fieldsInput = new TreeSyntaxNode();
            fieldsOut = new TreeSyntaxNode();
            body = new TreeSyntaxNode();

            node.Tree.Add(new UsingSyntax("HECSFramework.Core", 1));
            node.Tree.Add(new NameSpaceSyntax("Strategies"));
            node.Tree.Add(new LeftScopeSyntax());//{
            node.Tree.Add(new TabSimpleSyntax(1, $"[Documentation(Doc.Strategy, {CParse.Quote}{CParse.Quote})]"));
            node.Tree.Add(new TabSimpleSyntax(1, $"public class {name} : DilemmaDecision"));
            node.Tree.Add(new LeftScopeSyntax(1));//{{
            node.Tree.Add(fieldsInput);
            node.Tree.Add(fieldsOut);
            node.Tree.Add(new TabSimpleSyntax(2, $"public override string TitleOfNode {{ get; }} = {CParse.Quote}{name}{CParse.Quote};"));
            node.Tree.Add(body);
            node.Tree.Add(new RightScopeSyntax(1));//}}
            node.Tree.Add(new RightScopeSyntax());//}

            body.AddUnique(new TabSimpleSyntax(2, "protected override void Run(Entity entity)"));
            body.Tree.Add(new LeftScopeSyntax(2)); //{{{

            body.Tree.Add(new TabSimpleSyntax(3, "if (true)"));
            body.Tree.Add(new LeftScopeSyntax(3)); //{{{{
            body.Tree.Add(new TabSimpleSyntax(4, "Positive.Execute(entity);"));
            body.Tree.Add(new TabSimpleSyntax(4, "return;"));
            body.Tree.Add(new RightScopeSyntax(3)); //}}}}

            body.Tree.Add(new TabSimpleSyntax(3, "else"));
            body.Tree.Add(new LeftScopeSyntax(3)); //{{{{
            body.Tree.Add(new TabSimpleSyntax(4, "Negative.Execute(entity);"));
            body.Tree.Add(new TabSimpleSyntax(4, "return;"));
            body.Tree.Add(new RightScopeSyntax(3)); //}}}}

            body.Tree.Add(new RightScopeSyntax(2)); //}}}

        }

        private void GetGenericNode(string name, out ISyntax node, out ISyntax fieldsInput, out ISyntax fieldsOut, out ISyntax body)
        {
            node = new TreeSyntaxNode();
            fieldsInput = new TreeSyntaxNode();
            fieldsOut = new TreeSyntaxNode();
            body = new TreeSyntaxNode();

            node.Tree.Add(new UsingSyntax("HECSFramework.Core", 1));
            node.Tree.Add(new NameSpaceSyntax("Strategies"));
            node.Tree.Add(new LeftScopeSyntax());//{
            node.Tree.Add(new TabSimpleSyntax(1, $"[Documentation(Doc.Strategy, {CParse.Quote}{CParse.Quote})]"));
            node.Tree.Add(new TabSimpleSyntax(1, $"public class {name} : GenericNode<{GenericType}>"));
            node.Tree.Add(new LeftScopeSyntax(1));//{{
            node.Tree.Add(fieldsInput);
            node.Tree.Add(fieldsOut);
            node.Tree.Add(new TabSimpleSyntax(2, $"public override string TitleOfNode {{ get; }} = {CParse.Quote}{name}{CParse.Quote};"));
            node.Tree.Add(body);
            node.Tree.Add(new RightScopeSyntax(1));//}}
            node.Tree.Add(new RightScopeSyntax());//}

            body.AddUnique(new TabSimpleSyntax(2, "public override void Execute(Entity entity)"));
            body.Tree.Add(new LeftScopeSyntax(2)); //{{{
            body.Tree.Add(new RightScopeSyntax(2)); //}}}

            body.AddUnique(new TabSimpleSyntax(2, $"public override {GenericType} Value(Entity entity)"));
            body.Tree.Add(new LeftScopeSyntax(2)); //{{{
            body.Tree.Add(new RightScopeSyntax(2)); //}}}

            fieldsOut.Tree.Add(GetOutField(new CreateFieldInfo { Name = "Out", ConnectionPointType = ConnectionPointType.Out, Type = $"{GenericType}" }));
        }

        private void GetInterDecision(string name, out ISyntax node, out ISyntax fieldsInput, out ISyntax fieldsOut, out ISyntax body)
        {
            node = new TreeSyntaxNode();
            fieldsInput = new TreeSyntaxNode();
            fieldsOut = new TreeSyntaxNode();
            body = new TreeSyntaxNode();

            node.Tree.Add(new UsingSyntax("HECSFramework.Core", 1));
            node.Tree.Add(new NameSpaceSyntax("Strategies"));
            node.Tree.Add(new LeftScopeSyntax());//{
            node.Tree.Add(new TabSimpleSyntax(1, $"[Documentation(Doc.Strategy, {CParse.Quote}{CParse.Quote})]"));
            node.Tree.Add(new TabSimpleSyntax(1, $"public class {name} : InterDecision"));
            node.Tree.Add(new LeftScopeSyntax(1));//{{
            node.Tree.Add(fieldsInput);
            node.Tree.Add(fieldsOut);
            node.Tree.Add(new TabSimpleSyntax(2, $"public override string TitleOfNode {{ get; }} = {CParse.Quote}{name}{CParse.Quote};"));
            node.Tree.Add(body);
            node.Tree.Add(new RightScopeSyntax(1));//}}
            node.Tree.Add(new RightScopeSyntax());//}

            body.AddUnique(new TabSimpleSyntax(2, "protected override void Run(Entity entity)"));
            body.Tree.Add(new LeftScopeSyntax(2)); //{{{
            body.Tree.Add(new TabSimpleSyntax(3, " Next.Execute(entity);"));
            body.Tree.Add(new RightScopeSyntax(2)); //}}}
        }
    }
}

[Serializable]
public struct CreateFieldInfo
{
    public string Name;

    [ShowInInspector]
    public string Type;

    public ConnectionPointType ConnectionPointType;
}

public enum NodeType
{
    Dilemma,
    InterDecision,
    GenericNode,
}