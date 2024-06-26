using HECSFramework.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HECSFramework.Core.Generator
{
    public partial class CodeGenerator
    {
        public List<Type> needResolver = new List<Type>();
        public List<Type> containersSolve = new List<Type>();
        public List<Type> commands = new List<Type>();
        public const string Resolver = "Resolver";
        public const string Cs = ".cs";

        private string ResolverContainer = typeof(ResolverDataContainer).Name;

        #region Resolvers
        public List<(string name, string content)> GetSerializationResolvers()
        {
            var list = new List<(string, string)>();

            foreach (var c in componentTypes)
            {
                var attr2 = c.GetCustomAttribute(typeof(HECSResolverAttribute));

                if (attr2 != null)
                {
                    containersSolve.Add(c);
                    continue;
                }

                containersSolve.Add(c);
                needResolver.Add(c);
            }


            foreach (var c in needResolver)
            {
                list.Add((c.Name + Resolver + Cs, GetResolver(c).ToString()));
            }

            return list;
        }

        private ISyntax GetResolver(Type c)
        {
            var tree = new TreeSyntaxNode();
            var usings = new TreeSyntaxNode();
            var fields = new TreeSyntaxNode();
            var constructor = new TreeSyntaxNode();
            var defaultConstructor = new TreeSyntaxNode();
            var outFunc = new TreeSyntaxNode();
            var out2EntityFunc = new TreeSyntaxNode();
            tree.Add(usings);

            tree.Add(new NameSpaceSyntax("HECSFramework.Core"));
            tree.Add(new LeftScopeSyntax());
            tree.Add(new TabSimpleSyntax(1, "[MessagePackObject, Serializable]"));
            tree.Add(new TabSimpleSyntax(1, $"public struct {c.Name + Resolver} : IResolver<{c.Name}>, IData"));
            tree.Add(new LeftScopeSyntax(1));
            tree.Add(fields);
            tree.Add(new ParagraphSyntax());
            tree.Add(new TabSimpleSyntax(2, $"public {c.Name + Resolver} In(ref {c.Name} {c.Name.ToLower()})"));
            tree.Add(new LeftScopeSyntax(2));
            tree.Add(constructor);
            tree.Add(new RightScopeSyntax(2));
            //tree.Add(new ParagraphSyntax());
            //tree.Add(defaultConstructor);
            //tree.Add(new ParagraphSyntax());
            tree.Add(new TabSimpleSyntax(2, $"public void Out(ref {typeof(Entity).Name} entity)"));
            tree.Add(new LeftScopeSyntax(2));
            tree.Add(GetOutToEntityVoidBody(c));
            tree.Add(new RightScopeSyntax(2));

            tree.Add(new TabSimpleSyntax(2, $"public void Out(ref {c.Name} {c.Name.ToLower()})"));
            tree.Add(new LeftScopeSyntax(2));
            tree.Add(outFunc);
            tree.Add(new RightScopeSyntax(2));
            tree.Add(new RightScopeSyntax(1));
            tree.Add(new RightScopeSyntax());

            usings.Add(new UsingSyntax("Components"));
            usings.Add(new UsingSyntax("System"));

            var typeFields = c.GetFields(BindingFlags.Public | BindingFlags.Instance);
            var typeProperties = c.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            int count = 0;

            List<(string type, string name)> fieldsForConstructor = new List<(string type, string name)>();

            if (typeof(IBeforeSerializationComponent).IsAssignableFrom(c))
            {
                constructor.Add(new TabSimpleSyntax(3, $"{c.Name.ToLower()}.BeforeSync();"));
            }

            foreach (var f in typeFields)
            {
                var attr = f.GetCustomAttribute<FieldAttribute>();

                if (attr != null)
                {
                    fields.Add(new TabSimpleSyntax(2, $"[Key({attr.Queue})]"));

                    if (f.FieldType.IsGenericType && f.FieldType.Name.Contains("List"))
                    {
                        var type = f.FieldType.GetGenericArguments()[0]; // use this...
                        fields.Add(new TabSimpleSyntax(2, $"public List<{type.Name}> {f.Name};"));
                        usings.Add(new UsingSyntax("System.Collections.Generic"));
                    }
                    else
                    {
                        fields.Add(new TabSimpleSyntax(2, $"public {AdaptPrimitives(f.FieldType.Name)} {f.Name};"));
                        
                        if (!string.IsNullOrEmpty(f.FieldType.Namespace?.ToString()))
                        {
                            if (usings.Tree.FirstOrDefault(x => x.ToString() == new UsingSyntax(f.FieldType.Namespace).ToString()) == null)
                                usings.Add(new UsingSyntax(f.FieldType.Namespace));
                        }
                    }

                    constructor.Add(new TabSimpleSyntax(3, $"this.{f.Name} = {c.Name.ToLower()}.{f.Name};"));
                    outFunc.Add(new TabSimpleSyntax(3, $"{c.Name.ToLower()}.{f.Name} = this.{f.Name};"));
                    count++;
                }
            }

            var react = typeof(ReactiveValue<>);

            foreach (var property in typeProperties)
            {
                var attr = property.GetCustomAttribute<FieldAttribute>();

                if (attr != null)
                {
                    if (property.PropertyType.Name.Contains("ReactiveValue"))
                    {
                        var generics = property.PropertyType.GenericTypeArguments;

                        fields.Add(new TabSimpleSyntax(2, $"[Key({attr.Queue})]"));
                        fields.Add(new TabSimpleSyntax(2, $"public {generics[0].Name} {property.Name};"));
                        fieldsForConstructor.Add((generics[0].Name, property.Name));


                        constructor.Add(new TabSimpleSyntax(3, $"this.{property.Name} = {c.Name.ToLower()}.{property.Name}.CurrentValue;"));
                        outFunc.Add(new TabSimpleSyntax(3, $"{c.Name.ToLower()}.{property.Name}.CurrentValue = this.{property.Name};"));

                        count++;
                        continue;
                    }

                    if (property.CanWrite)
                    {
                        var setTest = property.SetMethod;

                        fields.Add(new TabSimpleSyntax(2, $"[Key({attr.Queue})]"));
                        fields.Add(new TabSimpleSyntax(2, $"public {AdaptPrimitives(property.PropertyType.Name)} {property.Name};"));
                        fieldsForConstructor.Add((property.PropertyType.Name, property.Name));

                        constructor.Add(new TabSimpleSyntax(3, $"this.{property.Name} = {c.Name.ToLower()}.{property.Name};"));
                        outFunc.Add(new TabSimpleSyntax(3, $"{c.Name.ToLower()}.{property.Name} = this.{property.Name};"));

                        if (!string.IsNullOrEmpty(property.PropertyType.Namespace?.ToString()))
                        {
                            if (usings.Tree.FirstOrDefault(x => x.ToString() == new UsingSyntax(property.PropertyType.Namespace).ToString()) == null)
                                usings.Add(new UsingSyntax(property.PropertyType.Namespace));
                        }
                    }

                    count++;
                }
            }

            if (typeof(IAfterSerializationComponent).IsAssignableFrom(c))
            {
                outFunc.Add(new TabSimpleSyntax(3, $"{c.Name.ToLower()}.AfterSync();"));
            }

            //defaultConstructor.Add(DefaultConstructor(c, fieldsForConstructor, fields, constructor));
            constructor.Add(new TabSimpleSyntax(3, "return this;"));

            usings.Add(new UsingSyntax("MessagePack", 1));

            return tree;
        }

        private string AdaptPrimitives(string source)
        {
            switch (source)
            {
                case "Single":
                    return "float";
                case "Int32":
                    return "int";
                case "Boolean":
                    return "bool";    
                case "Int64":
                    return "long"; 
                case "Byte":
                    return "byte";  
                case "Double":
                    return "double";
                case "String":
                    return "string";
                default:
                    return source;
            }
        }

        private ISyntax GetOutToEntityVoidBody(Type c)
        {
            var tree = new TreeSyntaxNode();
            tree.Add(new TabSimpleSyntax(3, $"var local = entity.Get{c.Name}();"));
            tree.Add(new TabSimpleSyntax(3, $"Out(ref local);"));
            return tree;
        }

        private ISyntax DefaultConstructor(Type type, List<(string type, string name)> data, ISyntax fields, ISyntax constructor)
        {
            var tree = new TreeSyntaxNode();
            var arguments = new TreeSyntaxNode();

            var defaultConstructor = new TreeSyntaxNode();
            var defaultconstructorSignature = new TreeSyntaxNode();

            tree.Add(new TabSimpleSyntax(2, $"[SerializationConstructor]"));
            tree.Add(defaultconstructorSignature);
            tree.Add(new LeftScopeSyntax(2));
            tree.Add(defaultConstructor);
            tree.Add(new RightScopeSyntax(2));

            if (data.Count == 0)
            {
                fields.Tree.Add(IsTagBool());
                constructor.Tree.Add(new TabSimpleSyntax(3, "IsTag = false;"));
                defaultConstructor.Tree.Add(new TabSimpleSyntax(3, "IsTag = false;"));
                arguments.Add(new SimpleSyntax("bool isTag"));

                defaultconstructorSignature.Add(new TabSimpleSyntax(2, $"public {type.Name + Resolver}({arguments})"));
                return tree;
            }

            for (int i = 0; i < data.Count; i++)
            {
                (string type, string name) d = data[i];
                var needComma = i < data.Count - 1 ? CParse.Comma : "";

                arguments.Add(new SimpleSyntax($"{d.type} {d.name}{needComma}"));
                defaultConstructor.Add(new TabSimpleSyntax(3, $"this.{d.name} = {d.name};"));
            }

            defaultconstructorSignature.Add(new TabSimpleSyntax(2, $"public {type.Name + Resolver}({arguments})"));
            return tree;
        }

        private ISyntax IsTagBool()
        {
            var tree = new TreeSyntaxNode();
            tree.Add(new TabSimpleSyntax(2, "[Key(0)]"));
            tree.Add(new TabSimpleSyntax(2, "public bool IsTag;"));
            return tree;
        }

        #endregion

        #region  ResolversMap
        public string GetResolverMap()
        {
            var tree = new TreeSyntaxNode();

            tree.Add(new UsingSyntax("Components"));
            tree.Add(new UsingSyntax("HECSFramework.Core"));
            tree.Add(new UsingSyntax("MessagePack", 1));
            tree.Add(GetUnionResolvers());
            tree.Add(new ParagraphSyntax());
            tree.Add(new NameSpaceSyntax("HECSFramework.Core"));
            tree.Add(new LeftScopeSyntax());
            tree.Add(new TabSimpleSyntax(1, "public partial class ResolversMap"));
            tree.Add(new LeftScopeSyntax(1));
            tree.Add(ResolverMapConstructor());
            tree.Add(LoadDataFromContainerSwitch());
            tree.Add(GetContainerForComponentFuncProvider());
            tree.Add(ProcessComponents());
            tree.Add(GetComponentFromContainerFuncRealisation());
            tree.Add(ProcessResolverContainerRealisation());
            tree.Add(new RightScopeSyntax(1));
            tree.Add(new RightScopeSyntax());
            return tree.ToString();
        }

        private ISyntax GetUnionResolvers()
        {
            var tree = new TreeSyntaxNode();
            var unionPart = new TreeSyntaxNode();
            tree.Add(unionPart);
            tree.Add(new TabSimpleSyntax(0, "public partial interface IData { }"));

            for (int i = 0; i < containersSolve.Count; i++)
            {
                unionPart.Add(new TabSimpleSyntax(0, $"[Union({i}, typeof({containersSolve[i].Name}Resolver))]"));
            }

            return tree;
        }

        private ISyntax ProcessResolverContainerRealisation()
        {
            var tree = new TreeSyntaxNode();
            var caseBody = new TreeSyntaxNode();

            tree.Add(new ParagraphSyntax());
            tree.Add(new TabSimpleSyntax(2, "private void ProcessResolverContainerRealisation(ref ResolverDataContainer dataContainerForResolving, ref Entity entity)"));
            tree.Add(new LeftScopeSyntax(2));
            tree.Add(new TabSimpleSyntax(3, "switch (dataContainerForResolving.TypeHashCode)"));
            tree.Add(new LeftScopeSyntax(3));
            tree.Add(caseBody);
            tree.Add(new RightScopeSyntax(3));
            tree.Add(new RightScopeSyntax(2));

            foreach (var container in containersSolve)
            {
                caseBody.Add(new TabSimpleSyntax(4, $"case {IndexGenerator.GetIndexForType(container)}:"));
                caseBody.Add(new TabSimpleSyntax(5, $"var {container.Name}{Resolver.ToLower()} = MessagePackSerializer.Deserialize<{container.Name}{Resolver}>(dataContainerForResolving.Data);"));
                caseBody.Add(new TabSimpleSyntax(5, $"var {container.Name}component = ({container.Name})entity.Get{container.Name}();"));
                caseBody.Add(new TabSimpleSyntax(5, $"{container.Name}{Resolver.ToLower()}.Out(ref {container.Name}component);"));
                caseBody.Add(new TabSimpleSyntax(5, $"break;"));
            }

            return tree;
        }

        private ISyntax GetComponentFromContainerFuncRealisation()
        {
            var tree = new TreeSyntaxNode();
            var caseBody = new TreeSyntaxNode();

            tree.Add(new ParagraphSyntax());
            tree.Add(new TabSimpleSyntax(2, "private IComponent GetComponentFromContainerFuncRealisation(ResolverDataContainer resolverDataContainer)"));
            tree.Add(new LeftScopeSyntax(2));
            tree.Add(new TabSimpleSyntax(3, "switch (resolverDataContainer.TypeHashCode)"));
            tree.Add(new LeftScopeSyntax(3));
            tree.Add(caseBody);
            tree.Add(new RightScopeSyntax(3));
            tree.Add(new TabSimpleSyntax(4, "return default;"));
            tree.Add(new RightScopeSyntax(2));

            foreach (var container in containersSolve)
            {
                caseBody.Add(new TabSimpleSyntax(4, $"case {IndexGenerator.GetIndexForType(container)}:"));
                caseBody.Add(new TabSimpleSyntax(5, $"var {container.Name}new = new {container.Name}();"));
                caseBody.Add(new TabSimpleSyntax(5, $"var {container.Name}data = MessagePackSerializer.Deserialize<{container.Name}{Resolver}>(resolverDataContainer.Data);"));
                caseBody.Add(new TabSimpleSyntax(5, $"{container.Name}data.Out(ref {container.Name}new);"));
                caseBody.Add(new TabSimpleSyntax(5, $"return {container.Name}new;"));
            }

            return tree;
        }

        private ISyntax ProcessComponents()
        {
            var tree = new TreeSyntaxNode();
            var caseBody = new TreeSyntaxNode();

            tree.Add(new ParagraphSyntax());
            tree.Add(new TabSimpleSyntax(2, $"private void ProcessComponents(ref {ResolverContainer} dataContainerForResolving, int worldIndex)"));
            tree.Add(new LeftScopeSyntax(2));
            tree.Add(new TabSimpleSyntax(3, "switch (dataContainerForResolving.TypeHashCode)"));
            tree.Add(new LeftScopeSyntax(3));
            tree.Add(caseBody);
            tree.Add(new RightScopeSyntax(3));
            tree.Add(new RightScopeSyntax(2));

            foreach (var container in containersSolve)
            {
                caseBody.Add(new TabSimpleSyntax(4, $"case {IndexGenerator.GetIndexForType(container)}:"));
                caseBody.Add(new TabSimpleSyntax(5, $"var {container.Name}{Resolver.ToLower()} = MessagePackSerializer.Deserialize<{container.Name}{Resolver}>(dataContainerForResolving.Data);"));
                caseBody.Add(new TabSimpleSyntax(5, $"if (EntityManager.TryGetEntityByID(dataContainerForResolving.EntityGuid, out var entityOf{container.Name}))"));
                caseBody.Add(new LeftScopeSyntax(5));
                caseBody.Add(new TabSimpleSyntax(6, $"var {container.Name}component = ({container.Name})entityOf{container.Name}.Get{container.Name}();"));
                caseBody.Add(new TabSimpleSyntax(6, $"{container.Name}{Resolver.ToLower()}.Out(ref {container.Name}component);"));
                caseBody.Add(new RightScopeSyntax(5));
                caseBody.Add(new TabSimpleSyntax(5, $"break;"));
            }

            return tree;
        }

        private ISyntax GetContainerForComponentFuncProvider()
        {
            var tree = new TreeSyntaxNode();
            var caseBody = new TreeSyntaxNode();

            tree.Add(new ParagraphSyntax());
            tree.Add(new TabSimpleSyntax(2, $"private {ResolverContainer} GetContainerForComponentFuncProvider<T>(T component) where T: IComponent"));
            tree.Add(new LeftScopeSyntax(2));
            tree.Add(new TabSimpleSyntax(3, "switch (component.GetTypeHashCode)"));
            tree.Add(new LeftScopeSyntax(3));
            tree.Add(caseBody);
            tree.Add(new RightScopeSyntax(3));
            tree.Add(new TabSimpleSyntax(3, "return default;"));
            tree.Add(new RightScopeSyntax(2));

            foreach (var container in containersSolve)
            {
                var lowerContainerName = (container.Name + Resolver).ToLower();
                caseBody.Add(new TabSimpleSyntax(4, $"case {IndexGenerator.GetIndexForType(container)}:"));
                caseBody.Add(new TabSimpleSyntax(5, $"var {lowerContainerName} = component as {container.Name};"));
                caseBody.Add(new TabSimpleSyntax(5, $"var {container.Name}Data = new {container.Name + Resolver}().In(ref {lowerContainerName});"));
                caseBody.Add(new TabSimpleSyntax(5, $"return PackComponentToContainer(component, {container.Name}Data);"));
            }

            return tree;
        }

        private ISyntax LoadDataFromContainerSwitch()
        {
            var tree = new TreeSyntaxNode();
            tree.Add(new ParagraphSyntax());
            tree.Add(new TabSimpleSyntax(2, $"partial void LoadDataFromContainerSwitch({typeof(ResolverDataContainer).Name} dataContainerForResolving, int worldIndex)"));
            tree.Add(new LeftScopeSyntax(2));
            tree.Add(new TabSimpleSyntax(3, "switch (dataContainerForResolving.Type)"));
            tree.Add(new LeftScopeSyntax(3));
            tree.Add(new TabSimpleSyntax(4, "case 0:"));
            tree.Add(new TabSimpleSyntax(5, "ProcessComponents(ref dataContainerForResolving, worldIndex);"));
            tree.Add(new TabSimpleSyntax(5, "break;"));
            tree.Add(new RightScopeSyntax(3));
            tree.Add(new RightScopeSyntax(2));
            return tree;
        }

        private ISyntax ResolverMapConstructor()
        {
            var tree = new TreeSyntaxNode();

            tree.Add(new TabSimpleSyntax(2, "public ResolversMap()"));
            tree.Add(new LeftScopeSyntax(2));
            tree.Add(new TabSimpleSyntax(3, "GetComponentContainerFunc = GetContainerForComponentFuncProvider;"));
            tree.Add(new TabSimpleSyntax(3, "ProcessResolverContainer = ProcessResolverContainerRealisation;"));
            tree.Add(new TabSimpleSyntax(3, "GetComponentFromContainer = GetComponentFromContainerFuncRealisation;"));
            tree.Add(new TabSimpleSyntax(3, "InitPartialCommandResolvers();"));
            tree.Add(new RightScopeSyntax(2));

            return tree;
        }
        #endregion
    }

}