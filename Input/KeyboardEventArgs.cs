namespace SSSG.Input
{
    using Microsoft.Xna.Framework.Input;

    public class KeyboardEventArgs : System.EventArgs
    {
        private Keys[] currentPressedKeys;
        private Keys sourceKey;

        public Keys SourceKey
        {
            get { return sourceKey; }
            private set { sourceKey = value; }
        }

        public Keys[] CurrentPressedKeys
        {
            get { return currentPressedKeys ?? new Keys[0]; }
            set { currentPressedKeys = value; }
        }

        public KeyboardEventArgs(Keys sourceKey)
        {
            SourceKey = sourceKey;
        }
    }
}
