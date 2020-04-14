using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;

namespace ChaiFoxes.FMODAudio
{
	/// <summary>
	/// Sound class. Can play sound with given attributes.
	/// </summary>
	public interface ISound
	{
		/// <summary>
		/// Tells if sound is looping.
		/// </summary>
		bool Looping { get; set; }

		/// <summary>
		/// Amount of loops. 
		/// > 0 - Specific count.
		/// 0 - No loops.
		/// -1 - Infinite loops.
		/// </summary>
		int Loops { get; set; }
		
		/// <summary>
		/// Sound pitch. Affects speed too.
		/// 1 - Normal pitch.
		/// More than 1 - Higher pitch.
		/// Less than 1 - Lower pitch.
		/// </summary>
		float Pitch { get; set; }

		/// <summary>
		/// Sound volume.
		/// 1 - Normal volume.
		/// 0 - Muted.
		/// </summary>
		float Volume { get; set; }

		/// <summary>
		/// Low pass filter. Makes sound muffled.
		/// 1 - No filtering.
		/// 0 - Full filtering.
		/// </summary>
		float LowPass { get; set; }

		/// <summary>
		/// Sound mode.
		/// </summary>
		SoundMode Mode { get; set; }

		/// <summary>
		/// Sound's default channel group.
		/// </summary>
		FMOD.ChannelGroup ChannelGroup { get; set; }

		/// <summary>
		/// If true, allows sound to be positioned in 3D space.
		/// </summary>
		bool Is3D { get; set; }

		/// <summary>
		/// Sound's position in 3D space. Can be used only if 3D positioning is enabled.
		/// </summary>
		Vector3 Position3D { get; set; }
		
		/// <summary>
		/// Sound's velocity in 3D space. Can be used only if 3D positioning is enabled.
		/// </summary>
		Vector3 Velocity3D { get; set; }

		/// <summary>
		/// Distance from the source where attenuation begins.
		/// </summary>
		float MinDistance3D { get; set; }
		
		/// <summary>
		/// Distance from the source where attenuation ends.
		/// </summary>
		float MaxDistance3D { get; set; }
		
		ISoundChannel Play(bool paused = false);
		
		ISoundChannel Play(FMOD.ChannelGroup group, bool paused = false);
		
		/// <summary>
		/// Unloads the sound and frees its handles.
		/// </summary>
		void Unload();
	}
}
