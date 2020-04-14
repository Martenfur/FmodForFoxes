
namespace ChaiFoxes.FMODAudio
{
	public enum ChannelControlCallbackType : int
	{
		/// <summary>
		/// Called when a sound ends.
		/// </summary>
		End,
		/// <summary>
		/// Called when a voice is swapped out or swapped in.
		/// </summary>
		VirtualVouce,
		/// <summary>
		/// Called when a syncpoint is encountered.  Can be from wav file markers.
		/// </summary>
		SyncPoint,
		/// <summary>
		/// Called when the channel has its geometry occlusion value calculated.  Can be used to clamp or change the value.
		/// </summary>
		Occlusion,
		/// <summary>
		/// Maximum number of callback types supported.
		/// </summary>
		Max,
	}
}
