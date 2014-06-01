namespace SSSG
{

#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            DeepSpaceShooterGame.Instance.Run();
        }
    }
#endif
}

