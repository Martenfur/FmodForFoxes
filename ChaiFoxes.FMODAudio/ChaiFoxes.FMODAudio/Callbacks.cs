using System;

namespace ChaiFoxes.FMODAudio
{
	public delegate FmodResult ChannelControlCallback(
		IntPtr channelcontrol,
		ChannelControlType controltype,
		ChannelControlCallbackType callbacktype,
		IntPtr commanddata1,
		IntPtr commanddata2
	);
}
