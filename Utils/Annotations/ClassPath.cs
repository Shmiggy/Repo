namespace SSSG.Utils.Annotations
{
    using System;

    public class ClassPath : Attribute
    {
        private string value; // should contain the fully qualified name of the class

        /// <summary>
        /// Initializes an intance of ClassPath class.
        /// </summary>
        /// <param name="value">the fully qualified name of the class</param>
        public ClassPath(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the fully qualified name of the class.
        /// </summary>
        public string Value
        {
            get { return this.value; }
            private set { this.value = value; }
        }
    }
}
