
namespace ChaiFoxes.FMODAudio
{
	public interface IChannel : IChannelControl, IUserData
	{
		// Channel specific control functionality.

		float Frequency { get; set; }
		int Priority { get; set; }

		FmodResult setPosition(uint position, TimeUnit postype);

		FmodResult getPosition(out uint position, TimeUnit postype);

		IChannelGroup ChannelGroup { get; set; }

		int LoopCount { get; set; }

		FmodResult setLoopPoints(uint loopstart, TimeUnit loopstarttype, uint loopend, TimeUnit loopendtype);

		FmodResult getLoopPoints(out uint loopstart, TimeUnit loopstarttype, out uint loopend, TimeUnit loopendtype);


		// Information only functions.
		bool IsVirtual { get; }

		ISound CurrentSound { get; }

		int Index { get; }
	}
}
