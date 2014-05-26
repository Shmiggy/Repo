using System;

namespace SSSG
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            Game.Instance.Run();
        }
    }
#endif
}

