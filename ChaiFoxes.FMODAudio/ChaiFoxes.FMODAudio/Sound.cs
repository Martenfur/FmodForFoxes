using Microsoft.Xna.Framework;

// DO NOT include FMOD namespace in ANY of your classes.
// Use FMOD.SomeClass instead.
// FMOD classes seriously interfere with System namespace.

namespace ChaiFoxes.FMODAudio
{
	public class Sound
	{
		protected FMOD.Sound _FMODSound {get; private set;}

		
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

		public Sound(FMOD.Sound sound)
		{
			_FMODSound = sound;
		}


		public SoundChannel Play(bool paused = false) =>
			Play(ChannelGroup, paused);
		

		public SoundChannel Play(FMOD.ChannelGroup group, bool paused = false)
		{
			AudioMgr.FMODSystem.playSound(_FMODSound, group, paused, out FMOD.Channel fmodChannel);
			var channel = new SoundChannel(this, fmodChannel);			
			return channel;
		}
		
		public SoundChannel PlayAt(Vector2 position, Vector2 velocity, bool paused = false) =>
			PlayAt(ChannelGroup, position, velocity, paused);
		
		
		public SoundChannel PlayAt(
			FMOD.ChannelGroup group, 
			Vector2 position,
			Vector2 velocity,
			bool paused = false
		)
		{
			AudioMgr.FMODSystem.playSound(_FMODSound, group, paused, out FMOD.Channel fmodChannel);
			var channel = new SoundChannel(this, fmodChannel);
			channel.Mode = FMOD.MODE._3D;
			channel.Set3DAttributes(position, velocity);

			return channel;
		}


		public void Unload() =>
			_FMODSound.release();
		

	}
}
