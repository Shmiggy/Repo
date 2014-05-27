namespace SSSG.Input
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using SSSG.Utils;
    using SSSG.Utils.Patterns;
    using System;

    public class MouseInput : IMouseInput
    {
        private MouseState? prevMouseState;
        private MouseState? currMouseState;

        public MouseInput()
        {
            prevMouseState = null;
            currMouseState = null;
        }

        private void updateMouseState(MouseState mouseState)
        {
            prevMouseState = currMouseState;
            currMouseState = mouseState;
        }

        #region IMouseInput Members

        public event MouseMovedHandler MouseMoved;
        public event MouseHandler LeftButtonDown;

        #endregion

        #region IObserver Members

        public void Update(ISubject subject, object payload)
        {
            updateMouseState(Mouse.GetState());

            if ( prevMouseState.HasValue )
            {
                if ( currMouseState.Value.X != prevMouseState.Value.X || currMouseState.Value.Y != prevMouseState.Value.Y )
                {
                    OnMouseMoved();
                }
            }

            if ( currMouseState.Value.LeftButton == ButtonState.Pressed )
            {
                if ( !prevMouseState.HasValue || prevMouseState.Value.LeftButton == ButtonState.Released )
                {
                    OnLeftButtonDown();
                }
            }

        }

        #endregion

        protected void OnMouseMoved()
        {
            MouseMovedHandler handler = MouseMoved;

            if ( handler != null )
            {
                Point previousPosition = new Point { X = prevMouseState.Value.X, Y = prevMouseState.Value.Y };
                Point currentPosition = new Point { X = currMouseState.Value.X, Y = currMouseState.Value.Y };

                handler.Invoke(this, new MouseMovedEventArgs(currentPosition, previousPosition));
            }
        }

        protected void OnLeftButtonDown()
        {
            MouseHandler handler = LeftButtonDown;

            if ( handler != null )
            {
                handler.Invoke(this, new MouseEventArgs(currMouseState.Value.X, currMouseState.Value.Y));
            }
        }

    }
}
