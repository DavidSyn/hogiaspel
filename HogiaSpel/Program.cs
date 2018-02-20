using System;

namespace HogiaSpel
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Engine())
                game.Run();
        }
    }
#endif
}
