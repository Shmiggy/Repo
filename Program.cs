namespace SSSG
{
    using SSSG.Input;

#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            DeepSpaceShooterGame.Instance.MouseInput = new MouseInput();
            DeepSpaceShooterGame.Instance.KeyboardInput = new KeyboardInput();

            DeepSpaceShooterGame.Instance.Run();
        }
    }
#endif
}

