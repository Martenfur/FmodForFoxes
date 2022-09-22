using System;

namespace FmodForFoxes.Samples.DesktopGL
{
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
			using(var game = new Game1(new DesktopNativeFmodLibrary()))
				game.Run();
		}
	}
}
