namespace SSSG.Input
{
    public class MouseEventArgs : System.EventArgs
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public MouseEventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
