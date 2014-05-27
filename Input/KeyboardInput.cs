namespace SSSG.Input
{
    using Microsoft.Xna.Framework.Input;
    using SSSG.Utils;
    using SSSG.Utils.Patterns;

    public class KeyboardInput : IKeyboardInput
    {
        private KeyboardState? prevKeyboardState;
        private KeyboardState? currKeyboardState;

        public KeyboardInput()
        {
            prevKeyboardState = null;
            currKeyboardState = null;
        }

        private void updateKeyboardState(KeyboardState keyboardState)
        {
            prevKeyboardState = currKeyboardState;
            currKeyboardState = keyboardState;
        }

        #region IKeyboardInput Members

        public event KeyboardHandler KeyPressed;
        public event KeyboardHandler KeyReleased;

        #endregion

        #region IObserver Members

        public void Update(ISubject subject, object payload)
        {
            updateKeyboardState(Keyboard.GetState());

            Keys[] keys = currKeyboardState.Value.GetPressedKeys();

            foreach ( Keys key in keys )
            {
                if ( prevKeyboardState.HasValue && prevKeyboardState.Value.IsKeyDown(key) )
                {
                    continue;
                }
                OnKeyPressed(key, keys);
            }

            if ( prevKeyboardState.HasValue )
            {
                Keys[] oldKeys = prevKeyboardState.Value.GetPressedKeys();
                foreach( Keys key in oldKeys)
                {
                    if (currKeyboardState.Value.IsKeyUp(key))
                    {
                        OnKeyReleased(key, keys);
                    }
                }
            }
        }

        #endregion

        protected void OnKeyPressed(Keys key, Keys[] currentPressedKeys)
        {
            KeyboardHandler handler = KeyPressed;
            if ( handler != null )
            {
                KeyboardEventArgs args = new KeyboardEventArgs(key);
                args.CurrentPressedKeys = currentPressedKeys;
                handler.Invoke(this, args);
            }
        }

        protected void OnKeyReleased(Keys key, Keys[] currentPressedKeys)
        {
            KeyboardHandler handler = KeyReleased;
            if ( handler != null )
            {
                KeyboardEventArgs args = new KeyboardEventArgs(key);
                args.CurrentPressedKeys = currentPressedKeys;
                handler.Invoke(this, args);
            }
        }
    }
}
