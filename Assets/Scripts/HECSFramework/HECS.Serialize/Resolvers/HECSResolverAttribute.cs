using System;
using HECSFramework.Core;


/// <summary>
/// we mark class or struct with this attribute and mark fields with field attr
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class HECSResolverAttribute : Attribute
{
    public readonly Type ResolverType;
    public readonly Type ResolverProvider;

    public HECSResolverAttribute()
    {
    }

    public HECSResolverAttribute(Type resolverType)
    {
        ResolverType = resolverType;
    }

    public HECSResolverAttribute(Type resolverType, Type resolverProvider) : this(resolverType)
    {
        ResolverProvider = resolverProvider;
    }
}

[Documentation(Doc.HECS, Doc.Attributes, "We should add this attribute when we need include private field to serialization, and this class should be partial for that ")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public class PartialSerializeFieldAttribute : Attribute
{
    private int Order;
    private string FieldName;
    private string AdditionalResolver;

    public PartialSerializeFieldAttribute(int order, string fieldName, string additionalResolver = null)
    {
        this.Order = order;
        this.FieldName = fieldName;
        this.AdditionalResolver = additionalResolver;
    }
}

//we use if we want manualy make custom resolver and u should provide type of resolver
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class HECSManualResolverAttribute : Attribute
{
    /// <summary>
    /// here we need to mention of type we should be resolved
    /// </summary>
    public Type ResolvedType;

    public HECSManualResolverAttribute(Type resolvedType)
    {
        ResolvedType = resolvedType;
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class HECSDefaultResolverAttribute : Attribute
{
}