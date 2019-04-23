using System;
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
	public class SoundChannel
	{
		/// <summary>
		/// FMOD channel object. Use it if you need full FMOD functionality.
		/// </summary>
		public FMOD.Channel Channel => _channel;
		protected FMOD.Channel _channel; // Can't use "out" on properties. 

		/// <summary>
		/// Sound, from which this channel has been created.
		/// </summary>
		public readonly Sound Sound;


		#region Properties.

		/// <summary>
		/// Tells if channel is looping.
		/// </summary>
		public bool Looping
		{
			get
			{
				SetLastResult(_channel.getLoopCount(out int loops));
				return (loops == -1);
			}
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
		public int Loops
		{
			get
			{
				// Do you have some lööps, bröther?
				SetLastResult(_channel.getLoopCount(out int loops));
				return loops;
			}
			set
			{
				if (value == 0)
				{
					Mode = FMOD.MODE.LOOP_OFF;
				}
				else
				{
					Mode = FMOD.MODE.LOOP_NORMAL;
				}

				SetLastResult(_channel.setLoopCount(value));
			}
		}

		/// <summary>
		/// Sound pitch. Affects speed too.
		/// 1 - Normal pitch.
		/// More than 1 - Higher pitch.
		/// Less than 1 - Lower pitch.
		/// </summary>
		public float Pitch
		{
			get
			{
				SetLastResult(_channel.getPitch(out float pitch));
				return pitch;
			}
			set => SetLastResult(_channel.setPitch(value));
		}

		/// <summary>
		/// Sound volume.
		/// 1 - Normal volume.
		/// 0 - Muted.
		/// </summary>
		public float Volume
		{
			get
			{
				SetLastResult(_channel.getVolume(out float volume));
				return volume;
			}
			set => SetLastResult(_channel.setVolume(value));	
		}

		/// <summary>
		/// Low pass filter. Makes sound muffled.
		/// 1 - No filtering.
		/// 0 - Full filtering.
		/// </summary>
		public float LowPass
		{
			get
			{
				SetLastResult(_channel.getLowPassGain(out float lowPassGain));
				return lowPassGain;
			}
			set => SetLastResult(_channel.setLowPassGain(value));
		}

		/// <summary>
		/// Sound mode. Mainly used for 3D sound.
		/// </summary>
		public FMOD.MODE Mode
		{
			get
			{
				SetLastResult(_channel.getMode(out _mode));
				return _mode;
			}
			set
			{
				_mode = value;
				SetLastResult(_channel.setMode(_mode));
			}
		}
		private FMOD.MODE _mode;

		/// <summary>
		/// Tells if sound is playing.
		/// </summary>
		public bool IsPlaying
		{
			get
			{
				SetLastResult(_channel.isPlaying(out bool isPlaying));
				return isPlaying;
			}
		}
		
		/// <summary>
		/// Track position in milliseconds.
		/// </summary>
		public uint TrackPosition
		{
			get
			{
				SetLastResult(_channel.getPosition(out uint position, FMOD.TIMEUNIT.MS));
				return position;
			}
			set
			{
				SetLastResult(_channel.setPosition(value, FMOD.TIMEUNIT.MS));
			}
		}
		

		#endregion Properties.



		public SoundChannel(Sound sound, FMOD.Channel channel)
		{
			Sound = sound;
			_channel = channel;

			Loops = Sound.Loops;
			Volume = Sound.Volume;
			Pitch = Sound.Pitch;
			LowPass = sound.LowPass;
			Mode = sound.Mode;
		}



		
		public void Pause() =>
			SetLastResult(_channel.setPaused(true));

		public void Resume() =>
			SetLastResult(_channel.setPaused(false));

		public void Stop() =>
			SetLastResult(_channel.stop());
			


		/// <summary>
		/// Sets 3D attributes.
		/// </summary>
		/// <param name="position">Sound position.</param>
		/// <param name="velocity">Sound velocity.</param>
		public void Set3DAttributes(Vector2 position, Vector2 velocity)
		{
			var fmodPos = position.ToFmodVector();
			var fmodVelocity = velocity.ToFmodVector();
			SetLastResult(_channel.set3DAttributes(ref fmodPos, ref fmodVelocity));
		}



		public void Set3DMinMaxDistance(float minDistance, float maxDistance) =>
			SetLastResult(_channel.set3DMinMaxDistance(minDistance, maxDistance));

		public Tuple<float, float> Get3DMinMaxDistance()
		{
			float minDistance = 0, 
				maxDistance = 0;
			SetLastResult(_channel.get3DMinMaxDistance(out minDistance, out maxDistance));
			return new Tuple<float, float>(minDistance, maxDistance);
		}
		

		/// <summary>
		/// Sets last result to the Audio Manager.
		/// NOTE: There is very high probability that this code will change, so 
		/// making it separate function will make life a bit easier.
		/// </summary>
		private void SetLastResult(FMOD.RESULT? result)
		{
			if (result != null)
			{
				AudioMgr.LastResult = (FMOD.RESULT)result;
			}
		}

	}
}
