using System;

namespace ChaiFoxes.FMODAudio.Demos.DesktopGL
{
	public static class Program
	{
		[STAThread]
		static void Main()
		{
			using (var game = new Game1())
				game.Run();
		}
	}
}
