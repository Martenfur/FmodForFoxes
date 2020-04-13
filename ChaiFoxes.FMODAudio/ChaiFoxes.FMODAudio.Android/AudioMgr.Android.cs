namespace ChaiFoxes.FMODAudio
{
	/// <summary>
	/// Android-specific part of the audio manager.
	/// </summary>
	public static partial class AudioMgr
	{
		/// <summary>
		/// Loads Android version of FMOD library.
		/// </summary>
		public static void LoadNativeLibrary(bool loadStudio = true)
		{
			Java.Lang.JavaSystem.LoadLibrary("fmod");
            if(loadStudio)
            {
                Java.Lang.JavaSystem.LoadLibrary("fmodstudio");
            }
		}
	}
}