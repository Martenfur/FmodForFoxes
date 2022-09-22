
namespace FmodForFoxes
{
	public interface INativeFmodLibrary
	{
		/// <summary>
		/// Initializes native FMOD library for a given platform.
		/// </summary>
		void Init(FmodInitMode mode, bool loggingEnabled = false);
	}
}
