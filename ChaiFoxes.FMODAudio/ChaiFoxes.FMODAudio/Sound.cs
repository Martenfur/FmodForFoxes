using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace ChaiFoxes.FMODAudio
{
	public class Sound
	{
		protected FMOD.Sound _FMODSound {get; private set;}

		/// <summary>
		/// Amount of loops. 
		/// > 0 - Specific count.
		/// 0 - No loops.
		/// -1 - Infinite loops.
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

		public int Loops;

		/// <summary>
		/// Sound pitch. Affects speed too.
		/// 1 - Normal pitch.
		/// </summary>
		public float Pitch;
		
		public float Volume;
		
		/// <summary>
		/// Low pass filter. Makes sound muffled.
		/// 1 - No filtering.
		/// 0 - Full filtering.
		/// </summary>
		public float LowPass;

		/// <summary>
		/// Sound mode. Mainly used for 3D sound.
		/// </summary>
		public FMOD.MODE Mode;

		public FMOD.ChannelGroup ChannelGroup;

		public Sound(FMOD.Sound sound)
		{
			_FMODSound = sound;
		}



		public SoundChannel Play(FMOD.ChannelGroup group, bool paused = false)
		{
			AudioMgr.FMODSystem.playSound(_FMODSound, group, paused, out FMOD.Channel fmodChannel);
			var channel = new SoundChannel(this, fmodChannel);			
			return channel;
		}
		
		
		public SoundChannel PlaySoundAt(
			FMOD.ChannelGroup group, 
			bool paused = false, 
			Vector2 position = default(Vector2), 
			Vector2 velocity = default(Vector2)
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
