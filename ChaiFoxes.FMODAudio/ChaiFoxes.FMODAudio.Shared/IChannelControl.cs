using Microsoft.Xna.Framework;

namespace ChaiFoxes.FMODAudio
{
	public interface IChannelControl : I3DControl
	{
		// General control functionality for Channels and ChannelGroups.
		void Stop();

		bool Paused { get; set; }
		float Volume { get; set; }
		bool VolumeRamp { get; set; }
		float Audibility { get; }
		float Pitch { get; set; }
		bool Mute { get; set; }
		ReverbProperties ReverbProperties { get; set; }
		float LowpassGain { get; set; }
		FMOD.MODE Mode { get; set; }
		bool IsPlaying { get; }

		// 3D functionality.

		Vector3 ConeOrientation3D { get; set; }

		Occlusion3D Occlusion3D { get; set; }
		
		float Spread3D { get; set; }
		float Level3D { get; set; }
		float DopplerLevel3D { get; set; }

		DistanceFilter3D DistanceFilter3D { get; set; }
	}
}
