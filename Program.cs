using System;

namespace SSSG
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            using (DesignPattern game = new DesignPattern())
            {
                game.Run();
            }
        }
    }
#endif
}

