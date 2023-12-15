using System;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class FieldAttribute : Attribute
{
    public readonly int Queue;
    public readonly Type CustomResolverType;
    public readonly Type CustomResolverProvider;

    public FieldAttribute(int queue)
    {
        Queue = queue;
    }

    public FieldAttribute(int queue, Type customResolverType) : this(queue)
    {
        CustomResolverType = customResolverType;
    }

    public FieldAttribute(int queue, Type customResolverType, Type customResolverProvider) 
    {
        Queue = queue;
        CustomResolverType = customResolverType;
        CustomResolverProvider = customResolverProvider;
    }
}