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

		/// <summary>
		/// If true, allows sound to be positioned in 3D space.
		/// </summary>
		public bool Is3D = false;

		/// <summary>
		/// Sound's position in 3D space. Can be used only id 3D positioning is enabled.
		/// </summary>
		public Vector3 Position3D = Vector3.Zero;
		
		/// <summary>
		/// Sound's velocity in 3D space. Can be used only id 3D positioning is enabled.
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


		public void Unload() =>
			_FMODSound.release();
		

	}
}
