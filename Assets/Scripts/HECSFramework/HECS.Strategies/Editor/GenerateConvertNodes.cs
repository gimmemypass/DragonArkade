using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using HECSFramework.Core.Generator;
using HECSFramework.Unity.Editor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Strategies;
using UnityEditor;

public class GenerateConvertNodes : OdinEditorWindow
{
    public const string ConvertNodes = "/ConvertNodes/";

    [MenuItem("HECS Options/Helpers/Generate ConvertNodes")]
    public static void GetGenerateSetNodeForComponentWindow()
    {
        var componentType = typeof(BaseDecisionNode);
        var path = InstallHECS.ScriptPath + InstallHECS.HECSGenerated + ConvertNodes;

        InstallHECS.CheckFolder(path);

        var assembly = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes());
        var nodes = assembly.Where(p => componentType.IsAssignableFrom(p) && !p.IsAbstract && !p.IsInterface).ToList();
        var listToConvert = new List<TypeToMethodsInfo>(64);


        foreach ( var node in nodes ) 
        { 
            var methods = node.GetMethods().Where(x=> x.GetCustomAttribute<GetConvertNodeAttribute>() != null).ToArray();

            if (methods != null && methods.Length > 0)
            {
                listToConvert.Add(new TypeToMethodsInfo { NodeType = node, Methods = new List<MethodInfo>(methods) });
            }
        }

        foreach (var node in listToConvert)
        {
            foreach (var m in node.Methods)
            {
                var method = m;
                var returnValue = StrategyGraphView.FromDotNetTypeToCSharpType(m.ReturnType.Name.ToString());
                var titleCaseReturnValue = returnValue.ToTitleCase();
                var className = $"{node.NodeType.Name}{m.Name}_To_{titleCaseReturnValue}";
                var data = GetConvertNode(node.NodeType, className, method.Name, returnValue, m.ReturnType.Namespace);
                File.WriteAllText(path + $"{className}.cs", data.ToString(), Encoding.UTF8);
            }
        }

        AssetDatabase.SaveAssets();
    }

    private static ISyntax GetConvertNode(Type type, string className, string methodName, string valueType, string namespaceNeeded)
    {
        var node = new TreeSyntaxNode();
        var namespaces = new TreeSyntaxNode();

        node.Add(namespaces);
        namespaces.Add(new UsingSyntax("HECSFramework.Core"));
        namespaces.AddUnique(new UsingSyntax(namespaceNeeded));
        namespaces.AddUnique(new UsingSyntax(type.Namespace));

        node.Add(new NameSpaceSyntax("Strategies"));
        node.Add(new LeftScopeSyntax());
        node.Add(new TabSimpleSyntax(1, $"public sealed class {className}: GenericNode<{valueType}>"));
        
        node.Add(new LeftScopeSyntax(1));
        node.Add(new TabSimpleSyntax(2, $"public override string TitleOfNode {CParse.LeftScope} get; {CParse.RightScope} = {CParse.Quote}{className}{CParse.Quote};"));
        node.Add(new TabSimpleSyntax(2, $"[Connection(ConnectionPointType.In, {CParse.Quote}<{type.Name}> In{CParse.Quote})]"));
        node.Add(new TabSimpleSyntax(2, $"public {type.Name} In;"));

        node.Add(new TabSimpleSyntax(2, $"[Connection(ConnectionPointType.Out, {CParse.Quote}<{valueType}> Out{CParse.Quote})]"));
        node.Add(new TabSimpleSyntax(2, $"public BaseDecisionNode Out;"));

        node.Add(new TabSimpleSyntax(2, "public override void Execute(Entity entity)"));
        node.Add(new LeftScopeSyntax(2));
        node.Add(new RightScopeSyntax(2));

        node.Add(new TabSimpleSyntax(2, $"public override {valueType} Value(Entity entity)"));
        node.Add(new LeftScopeSyntax(2));
        node.Add(new TabSimpleSyntax(3, $"return In.{methodName}(entity);"));
        node.Add(new RightScopeSyntax(2));

        node.Add(new RightScopeSyntax(1));
        node.Add(new RightScopeSyntax());
        return node;
    }
}

public class TypeToMethodsInfo
{
    public Type NodeType;
    public List<MethodInfo> Methods = new List<MethodInfo>(3);
}

