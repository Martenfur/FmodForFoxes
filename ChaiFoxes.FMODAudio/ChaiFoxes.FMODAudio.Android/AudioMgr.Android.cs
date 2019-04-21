namespace ChaiFoxes.FMODAudio
{
	/// <summary>
	/// Android-specific part of an audio manager.
	/// </summary>
	public static partial class AudioMgr
	{
		/// <summary>
		/// Loads Android version of FMOD library.
		/// </summary>
		public static void LoadNativeLibrary()
		{
			Java.Lang.JavaSystem.LoadLibrary("fmod");
		}
	}
}