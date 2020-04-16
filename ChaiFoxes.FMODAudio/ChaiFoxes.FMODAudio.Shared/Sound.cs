using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;

// DO NOT include FMOD namespace in ANY of your classes.
// Use FMOD.SomeClass instead.
// FMOD classes seriously interfere with System namespace.

namespace ChaiFoxes.FMODAudio
{
	/// <summary>
	/// Sound class. Can play sound with given attributes.
	/// </summary>
	public class Sound
	{
		public FMOD.Sound FMODSound {get; private set;}
		
		/// <summary>
		/// Tells if sound is looping.
		/// </summary>
		public bool Looping
		{
			get => (Loops == -1);
			set 
			{
				if (value)
				{
					Loops = -1;
				}
				else
				{	
					Loops = 0;
				}
			}
		}

		/// <summary>
		/// Amount of loops. 
		/// > 0 - Specific count.
		/// 0 - No loops.
		/// -1 - Infinite loops.
		/// </summary>
		public int Loops = 0;
		
		/// <summary>
		/// Sound pitch. Affects speed too.
		/// 1 - Normal pitch.
		/// More than 1 - Higher pitch.
		/// Less than 1 - Lower pitch.
		/// </summary>
		public float Pitch = 1;
		
		/// <summary>
		/// Sound volume.
		/// 1 - Normal volume.
		/// 0 - Muted.
		/// </summary>
		public float Volume = 1;
		
		/// <summary>
		/// Low pass filter. Makes sound muffled.
		/// 1 - No filtering.
		/// 0 - Full filtering.
		/// </summary>
		public float LowPass = 1;

		/// <summary>
		/// Sound mode.
		/// </summary>
		public FMOD.MODE Mode = FMOD.MODE.DEFAULT;

		/// <summary>
		/// Sound's default channel group.
		/// </summary>
		public FMOD.ChannelGroup ChannelGroup;

		/// <summary>
		/// If true, allows sound to be positioned in 3D space.
		/// </summary>
		public bool Is3D = false;

		/// <summary>
		/// Sound's position in 3D space. Can be used only if 3D positioning is enabled.
		/// </summary>
		public Vector3 Position3D = Vector3.Zero;
		
		/// <summary>
		/// Sound's velocity in 3D space. Can be used only if 3D positioning is enabled.
		/// </summary>
		public Vector3 Velocity3D = Vector3.Zero;
		
		/// <summary>
		/// Distance from the source where attenuation begins.
		/// </summary>
		public float MinDistance3D;
		
		/// <summary>
		/// Distance from the source where attenuation ends.
		/// </summary>
		public float MaxDistance3D;

		/// <summary>
		/// Sound buffer. Used for streamed sounds, which point to this memory.
		/// In other words, we need to just reference it somewhere to prevent
		/// garbage collector from collecting it.
		/// This memory is also pinned, so GC won't move it anywhere.
		/// 
		/// If any unexpected crashes emerge, this is the first suspect.
		/// </summary>
		private byte[] _buffer;

		/// <summary>
		/// Buffer's handle.
		/// </summary>
		private GCHandle _bufferHandle;

		public Sound(FMOD.Sound sound, byte[] buffer, GCHandle bufferHandle)
		{
			FMODSound = sound;
			_buffer = buffer;
			_bufferHandle = bufferHandle;
		}

		public Sound(FMOD.Sound sound)
		{
			FMODSound = sound;
			_buffer = null;
		}
		
		public SoundChannel Play(bool paused = false) =>
			Play(ChannelGroup, paused);

		public SoundChannel Play(FMOD.ChannelGroup group, bool paused = false)
		{
			CoreSystem.Native.playSound(FMODSound, group, paused, out FMOD.Channel fmodChannel);
			return new SoundChannel(this, fmodChannel);	
		}

		/// <summary>
		/// Unloads the sound and frees its handles.
		/// </summary>
		public void Unload()
		{
			FMODSound.release();
			if (_buffer != null)
			{
				_bufferHandle.Free();
			}
		}
	}
}
