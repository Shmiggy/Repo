namespace SSSG.Input
{
    using Microsoft.Xna.Framework;

    public class MouseMovedEventArgs : System.EventArgs
    {
        private Point previousPosition;
        private Point currentPosition;

        public MouseMovedEventArgs(Point currentPosition, Point previousPosition)
        {
            CurrentPosition = currentPosition;
            PreviousPosition = previousPosition;
        }

        public Point PreviousPosition
        {
            get { return previousPosition; }
            private set { previousPosition = value; }
        }

        public Point CurrentPosition
        {
            get { return currentPosition; }
            private set { currentPosition = value; }
        }

    }
}