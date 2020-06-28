namespace ChaiFoxes.FMODAudio
{
	/// <summary>
	/// Android-specific part of the audio manager.
	/// </summary>
	public static class NativeLibraryLoader
	{
		/// <summary>
		/// Loads Android version of FMOD library.
		/// </summary>
		public static void LoadNativeLibrary(string libName) =>
			Java.Lang.JavaSystem.LoadLibrary(libName);
	}
}