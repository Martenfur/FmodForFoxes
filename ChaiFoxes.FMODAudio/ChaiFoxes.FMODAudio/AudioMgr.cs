using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

namespace ChaiFoxes.FMODAudio
{

	/// <summary>
	/// Audio manager. Controls main audiosystem parameters.
	/// </summary>
	public static partial class AudioMgr
	{
		public static FMOD.System FMODSystem;
		public static FMOD.RESULT LastResult {get; internal set;}
		
		public static int ListenerCount 
		{
			get 
			{
				LastResult = FMODSystem.get3DNumListeners(out int listeners);
				return listeners;
			}
			set => LastResult = FMODSystem.set3DNumListeners(value);
		}

		/// <summary>
		/// Root directory for sounds and music.
		/// </summary>
		private static string _rootDir;
		
		/// <summary>
		/// Initializes FMOD with default parameters. 
		/// 
		/// If you want to use only the default wrapper, call
		/// LoadNativeLibrary() instead.
		/// </summary>
		/// <param name="rootDir"></param>
		public static void Init(string rootDir)
		{
			_rootDir = rootDir;
			LoadNativeLibrary();		
			
			FMOD.Factory.System_Create(out FMOD.System system);
			FMODSystem = system;

			FMODSystem.setDSPBufferSize(256, 4);
			FMODSystem.init(32, FMOD.INITFLAGS.CHANNEL_LOWPASS | FMOD.INITFLAGS.CHANNEL_DISTANCEFILTER, (IntPtr)0);
		}
		

		public static FMOD.ChannelGroup CreateChannelGroup(string name)
		{
			FMODSystem.createChannelGroup(name, out FMOD.ChannelGroup group);
			return group;
		}


		public static void Update() =>
			FMODSystem.update();
		
		public static void Unload() =>
			FMODSystem.release();
		
		
		/// <summary>
		/// Loads sound from file.
		/// Use this function to load short sound effects.
		/// </summary>
		public static Sound LoadSound(string name, FMOD.MODE mode = FMOD.MODE.DEFAULT)
		{
			// TODO: Fix paths, when will be porting FMOD to other platforms.
			LastResult = FMODSystem.createSound(
				_rootDir + name, 
				mode, 
				out FMOD.Sound newSound
			);
			
			return new Sound(FMODSystem, newSound, new FMOD.Channel());
		}
				
		/// <summary>
		/// Loads sound stream from file.
		/// Use this function to load music and lond ambience tracks.
		/// </summary>
		public static Sound LoadStreamedSound(string name, FMOD.MODE mode = FMOD.MODE.DEFAULT)
		{
			var stream = TitleContainer.OpenStream(_rootDir + name);
			
			byte[] buffer = new byte[16*1024];
			byte[] bufferRes;
			using (MemoryStream ms = new MemoryStream())
			{
				int read;
        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            ms.Write(buffer, 0, read);
        }
        bufferRes =  ms.ToArray();
			}
			
			var info = new FMOD.CREATESOUNDEXINFO();
			info.length = (uint)bufferRes.Length;
			info.cbsize = Marshal.SizeOf(info);

			LastResult = FMODSystem.createStream(
				bufferRes, 
				FMOD.MODE.OPENMEMORY | FMOD.MODE.CREATESAMPLE, 
				ref info,
				out FMOD.Sound newSound
			);

			return new Sound(FMODSystem, newSound, new FMOD.Channel());
			
		}

		/// <summary>
		/// Plays given sound and returns separate instance of it.
		/// </summary>
		public static Sound PlaySound(Sound sound, FMOD.ChannelGroup group, bool paused = false)
		{
			LastResult = FMODSystem.playSound(sound.FMODSound, group, paused, out FMOD.Channel channel);
			return new Sound(FMODSystem, sound.FMODSound, channel);
		}
		
		/// <summary>
		/// Plays given sound and returns separate instance of it.
		/// </summary>
		public static Sound PlaySoundAt(
			Sound sound, 
			FMOD.ChannelGroup group, 
			bool paused = false, 
			Vector2 position = default(Vector2), 
			Vector2 velocity = default(Vector2)
		)
		{
			LastResult = FMODSystem.playSound(sound.FMODSound, group, paused, out FMOD.Channel channel);
			var newSound = new Sound(FMODSystem, sound.FMODSound, channel);
			newSound.Mode = FMOD.MODE._3D;
			newSound.Set3DAttributes(position, velocity);

			return newSound;
		}



		public static void SetListenerPosition(Vector2 position, int listenerId = 0)
		{
			var fmodPos = position.ToFmodVector();
			var fmodZeroVec = Vector3.Zero.ToFmodVector();

			// Apparently, you cannot just pass zero vector and call it a day.
			var fmodForward = Vector2.UnitY.ToFmodVector();
			var fmodUp = Vector3.UnitZ.ToFmodVector();

			LastResult = FMODSystem.set3DListenerAttributes(listenerId, ref fmodPos, ref fmodZeroVec, ref fmodForward, ref fmodUp);
		}

		public static void SetListenerAttributes(Vector2 position, Vector2 velocity, Vector2 forward, int listenerId = 0)
		{
			var fmodPos = position.ToFmodVector();
			var fmodVelocity = velocity.ToFmodVector();
			var fmodForward = forward.ToFmodVector();
			var fmodZeroVec = Vector3.Zero.ToFmodVector();
			var fmodUp = Vector3.UnitZ.ToFmodVector();

			LastResult = FMODSystem.set3DListenerAttributes(listenerId, ref fmodPos, ref fmodVelocity, ref fmodForward, ref fmodUp);		
		}


	}
}
