using Microsoft.Xna.Framework;

// DO NOT include FMOD namespace in ANY of your classes.
// Use FMOD.SomeClass instead.
// FMOD classes seriously interfere with System namespace.

namespace ChaiFoxes.FMODAudio
{
	/// <summary>
	/// FMOD sound channel wrapper. Takes horrible FMOD wrapper and makes it look pretty.
	/// Basically, a playing sound instance.
	/// </summary>
	public interface ISoundChannel
	{
		/// <summary>
		/// Sound, from which this channel has been created.
		/// </summary>
		ISound Sound { get; }


		/// <summary>
		/// Tells if channel is looping.
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
		/// Sound mode. Mainly used for 3D sound.
		/// </summary>
		SoundMode Mode { get; set; }


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

		/// <summary>
		/// Tells if sound is playing.
		/// </summary>
		bool IsPlaying { get; }

		/// <summary>
		/// Track position in milliseconds.
		/// </summary>
		uint TrackPosition { get; set; }

		
		void Pause();

		void Resume();

		void Stop();
	}
}
