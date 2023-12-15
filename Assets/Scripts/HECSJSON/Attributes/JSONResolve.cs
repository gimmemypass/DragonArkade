using System;
using HECSFramework.Core;

namespace HECSFramework.Serialize
{
    [Documentation(Doc.HECS, Doc.JSONSerialization, "we mark any objects with this attributes, and we should mark fields by field attribute")]
    public class JSONHECSSerializeAttribute : Attribute
    {
    }

    [Documentation(Doc.JSONSerialization, Doc.HECS, "if we need resolve complex field by another resolver, we should mark field by this attribute")]
    public class JSONHECSFieldByResolver : Attribute
    {
        public Type ResolverType;

        public JSONHECSFieldByResolver(Type resolverType)
        {
            ResolverType = resolverType;
        }
    } 
}
