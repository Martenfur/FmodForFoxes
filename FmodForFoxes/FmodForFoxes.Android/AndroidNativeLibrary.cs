
namespace FmodForFoxes
{
	/// <summary>
	/// Android-specific part of the audio manager.
	/// </summary>
	public class AndroidNativeLibrary : INativeLibrary
	{
		public void Init(FMODMode mode, bool loggingEnabled = false)
		{
			Java.Lang.JavaSystem.LoadLibrary(SelectDefaultLibraryName("fmod", loggingEnabled));
			if (mode == FMODMode.CoreAndStudio)
			{ 
				Java.Lang.JavaSystem.LoadLibrary(SelectDefaultLibraryName("fmodstudio", loggingEnabled));
			}
		}


		private string SelectDefaultLibraryName(string libName, bool loggingEnabled = false) =>
			loggingEnabled ? $"{libName}L" : $"{libName}";
	}
}
