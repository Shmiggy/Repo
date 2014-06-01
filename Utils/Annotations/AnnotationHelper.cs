namespace SSSG.Utils.Annotations
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class AnnotationHelper
    {
        public static T GetAttributeFromField<T>(this object instance, string fieldName) where T : Attribute
        {
            Type attrType = typeof(T);
            FieldInfo field = instance.GetType().GetField(fieldName);
            return (T) field.GetCustomAttributes(attrType, false).First();
        }
    }
}
