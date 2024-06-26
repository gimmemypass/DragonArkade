using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HECSFramework.Core;
using HECSFramework.Core.Generator;
using HECSFramework.Unity.Editor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

public class GenerateSetNodeForComponentWindow : OdinEditorWindow
{
    [OnValueChanged(nameof(ClearField))]
    [ValueDropdown(nameof(GetComponents))]
    public Type Component;

    [HideIf("@Component == null")]
    [ValueDropdown(nameof(GetFields))]
    public string Field;

    private const string StrategyNodes = "/StrategyNodes/";

    [MenuItem("HECS Options/Helpers/Generate SetNode For Component")]
    public static void GetGenerateSetNodeForComponentWindow()
    {
        GetWindow<GenerateSetNodeForComponentWindow>();
    }

    private IEnumerable<Type> GetComponents()
    {
        var components = new TypesProvider().TypeToComponentIndex.Keys.ToList();
        return components;
    }

    private void ClearField()
    {
        Field = string.Empty;
    }

    private IEnumerable<string> GetFields()
    {
        if (Component == null)
            return default;

        var fields = Component.GetFields(BindingFlags.Public | BindingFlags.Instance);

        var list = new List<string>(16);

        foreach (var f in fields)
        {
            list.Add(f.Name);
        }

        return list;
    }

    [Button]
    [HideIf("@string.IsNullOrEmpty(Field)")]
    private void GenerateNode()
    {
        var field = Component.GetField(Field);

        var tree = new TreeSyntaxNode();
        var usings = new TreeSyntaxNode();

        tree.Add(usings);
        tree.Add(new UsingSyntax("HECSFramework.Core"));
        tree.Add(new UsingSyntax("Components"));
        tree.AddUnique(new UsingSyntax(field.FieldType.Namespace));

        tree.Add(new NameSpaceSyntax("Strategies"));
        tree.Add(new LeftScopeSyntax());

        tree.Add(new TabSimpleSyntax(1, $"public sealed class {Component.Name}Set{Field} : InterDecision"));
        tree.Add(new LeftScopeSyntax(1));
        tree.Add(new TabSimpleSyntax(2, $"public override string TitleOfNode {CParse.LeftScope} get; {CParse.RightScope} = {CParse.Quote}{Component.Name}Set{Field}{CParse.Quote};"));

        tree.Add(new TabSimpleSyntax(2, $"[Connection(ConnectionPointType.In, {CParse.Quote}<{field.FieldType.Name}> Set {field.Name}{CParse.Quote})]"));
        tree.Add(new TabSimpleSyntax(2, $"public GenericNode<{field.FieldType.Name}> Set{field.Name};"));

        tree.Add(new TabSimpleSyntax(2, "protected override void Run(Entity entity)"));
        tree.Add(new LeftScopeSyntax(2));
        tree.Add(new TabSimpleSyntax(3, $"entity.GetOrAddComponent<{Component.Name}>().{field.Name} = Set{field.Name}.Value(entity);"));
        tree.Add(new TabSimpleSyntax(3, $"Next.Execute(entity);"));
        tree.Add(new RightScopeSyntax(2));

        tree.Add(new RightScopeSyntax(1));
        tree.Add(new RightScopeSyntax());

        InstallHECS.CheckFolder(InstallHECS.ScriptPath + InstallHECS.HECSGenerated + StrategyNodes);
        InstallHECS.SaveToFile(tree.ToString(), InstallHECS.ScriptPath + InstallHECS.HECSGenerated + StrategyNodes + $"{Component.Name}Set{Field}.cs");
        AssetDatabase.SaveAssets();
        Close();
    }
}