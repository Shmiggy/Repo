namespace SSSG.Utils.Annotations
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class AnnotationHelper
    {
        /// <summary>
        /// Gets a certain attribute placed on certain field of an object.
        /// </summary>
        /// <typeparam name="T">the type of the attribute</typeparam>
        /// <param name="instance">the target object</param>
        /// <param name="fieldName">the name of the field on which the attribute is placed upon</param>
        /// <returns></returns>
        public static T GetAttributeFromField<T>(this object instance, string fieldName) where T : Attribute
        {
            Type attrType = typeof(T);
            FieldInfo field = instance.GetType().GetField(fieldName);
            return (T) field.GetCustomAttributes(attrType, false).First();
        }
    }
}
