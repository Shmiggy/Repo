namespace SSSG.Utils.Annotations
{
    using System;

    public class StringValue : Attribute
    {
        private string value;

        public StringValue(string value)
        {
            Value = value;
        }

        public string Value
        {
            get { return this.value; }
            private set { this.value = value; }
        }
    }
}
