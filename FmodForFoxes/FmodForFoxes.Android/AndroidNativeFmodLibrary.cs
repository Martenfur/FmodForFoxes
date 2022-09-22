
namespace FmodForFoxes
{
	/// <summary>
	/// Android-specific part of the audio manager.
	/// </summary>
	public class AndroidNativeFmodLibrary : INativeFmodLibrary
	{
		public void Init(FmodInitMode mode, bool loggingEnabled = false)
		{
			Java.Lang.JavaSystem.LoadLibrary(SelectDefaultLibraryName("fmod", loggingEnabled));
			if (mode == FmodInitMode.CoreAndStudio)
			{ 
				Java.Lang.JavaSystem.LoadLibrary(SelectDefaultLibraryName("fmodstudio", loggingEnabled));
			}
		}


		private string SelectDefaultLibraryName(string libName, bool loggingEnabled = false) =>
			loggingEnabled ? $"{libName}L" : $"{libName}";
	}
}
