using System;

namespace FmodForFoxes.Samples.WindowsDX
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Game1(new DesktopNativeLibrary()))
                game.Run();
        }
    }
#endif
}
