using System;

namespace Strategies
{
    public class ComponentMaskDropDownAttribute : Attribute
    {

    }

    public class DrawEntitiesFilterAttribute : Attribute { }

    public class DropDownIdentifierAttribute : Attribute
    {
        public string IdentifierType;

        public DropDownIdentifierAttribute(string identifierType)
        {
            IdentifierType = identifierType;
        }
    }
}