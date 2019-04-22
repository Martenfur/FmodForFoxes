using System;
using Microsoft.Xna.Framework;

// DO NOT include FMOD namespace in ANY of your classes.
// Use FMOD.SomeClass instead.
// FMOD classes seriously interfere with System namespace.

namespace ChaiFoxes.FMODAudio
{
	/// <summary>
	/// FMOD sound wrapper. Takes horrible FMOD wrapper and makes it look pretty.
	/// Basically, a sound instance.
	/// 
	/// NOTE: You can set sound parameters only AFTER you've started playing sound.
	/// Otherwise they will have no effect!
	/// 
	/// NOTE: My wrappers don't provide full FMOD functionality. For example,
	/// DSPs and advanced 3D stuff are largely left untouched. I may extend my audio
	/// classes to add new features. For now, you have to use FMOD classes directly.
	/// </summary>
	public class SoundChannel
	{
		/// <summary>
		/// FMOD channel object.
		/// 
		/// NOTE: It is not recommended to use it directly. 
		/// If you'll really need some of its features, 
		/// just implement them in this class.
		/// 
		/// NOTE: ALWAYS check for null!!!
		/// </summary>
		public FMOD.Channel Channel 
		{
			get => _channel;
			private set => _channel = value;
		}
		private FMOD.Channel _channel; // Can't use "out" on properties. 

		public Sound Sound;

		#region Properties.

		/// <summary>
		/// Amount of loops. 
		/// > 0 - Specific count.
		/// 0 - No loops.
		/// -1 - Infinite loops.
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
					SetLastResult(_channel.setLoopCount(-1));
				}
				else
				{	
					SetLastResult(_channel.setLoopCount(0));
				}
			}
		}

		public int Loops
		{
			get
			{
				SetLastResult(_channel.getLoopCount(out int loops));
				return loops;
			}
			set => SetLastResult(_channel.setLoopCount(value));
		}

		/// <summary>
		/// Sound pitch. Affects speed too.
		/// 1 - Normal pitch.
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
		/// Sound mode. Mainly used for 3D sound.
		/// </summary>
		public FMOD.MODE Mode
		{
			get
			{
				SetLastResult(_channel.getMode(out FMOD.MODE mode));
				return mode;
			}
			set => SetLastResult(_channel.setMode(value));
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

		

		public uint GetTrackPosition(FMOD.TIMEUNIT timeUnit = FMOD.TIMEUNIT.MS)
		{
			SetLastResult(_channel.getPosition(out uint position, timeUnit));
			return position;
		}

		public void SetTrackPosition(uint position, FMOD.TIMEUNIT timeUnit = FMOD.TIMEUNIT.MS) =>
			SetLastResult(_channel.setPosition(position, timeUnit));
		


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
