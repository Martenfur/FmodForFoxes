using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using ChaiFoxes.FMODAudio.Studio;

// DO NOT include FMOD namespace in ANY of your classes.
// Use FMOD.SomeClass instead.
// FMOD classes seriously interfere with System namespace.

namespace ChaiFoxes.FMODAudio
{
	/// <summary>
	/// Audio manager. Contains main audiosystem parameters.
	/// </summary>
	public static class AudioSystem
	{
		/// <summary>
		/// Low level FMOD sound system.
		/// </summary>
		public static FMOD.System FMODSystem;

		/// <summary>
		/// Returns true if initialized with Studio, false for the core FMOD system.
		/// </summary>
		private static bool _studioLoaded;

		/// <summary>
		/// Initializes FMOD Core with default parameters.
		/// Loading this way disables Studio functionality.
		/// 
		/// If you want to exclusively use the default wrapper, call
		/// LoadNativeLibraries() instead.
		/// </summary>
		public static void Init(string rootDir) =>
				Init(rootDir, 256, 4, 32, FMOD.INITFLAGS.CHANNEL_LOWPASS | FMOD.INITFLAGS.CHANNEL_DISTANCEFILTER);

		/// <summary>
		/// Initializes FMOD Core with custom parameters.
		/// Loading this way disables Studio functionality.
		/// 
		/// If you want to exclusively use the default wrapper, call
		/// LoadNativeLibrary("fmod") instead.
		/// </summary>
		public static void Init(
				string rootDir,
				uint dspBufferLength,
				int dspBufferCount,
				int maxChannels,
				FMOD.INITFLAGS initFlags
		)
		{
			FileLoader.RootDirectory = rootDir;
			NativeLibraryLoader.LoadNativeLibrary("fmod");

			FMOD.Factory.System_Create(out FMOD.System system);
			FMODSystem = system;

			// Too high values will cause sound lag.
			FMODSystem.setDSPBufferSize(dspBufferLength, dspBufferCount);

			FMODSystem.init(maxChannels, initFlags, (IntPtr)0);
			_studioLoaded = false;
		}

		public static void Update() =>
			FMODSystem.update();

		public static void Unload() =>
			FMODSystem.release();

		/// <summary>
		/// Creates new channel group with given name.
		/// </summary>
		public static FMOD.ChannelGroup CreateChannelGroup(string name)
		{
			FMODSystem.createChannelGroup(name, out FMOD.ChannelGroup channelGroup);
			return channelGroup;
		}

		/// <summary>
		/// Loads sound from file.
		/// Use this function to load short sound effects.
		/// </summary>
		public static Sound LoadSound(string path)
		{
			var buffer = FileLoader.LoadFileAsBuffer(path);

			var info = new FMOD.CREATESOUNDEXINFO();
			info.length = (uint)buffer.Length;
			info.cbsize = Marshal.SizeOf(info);

			FMODSystem.createSound(
					buffer,
					FMOD.MODE.OPENMEMORY | FMOD.MODE.CREATESAMPLE,
					ref info,
					out FMOD.Sound newSound
			);

			return new Sound(newSound);
		}

		/// <summary>
		/// Loads streamed sound stream from file.
		/// Use this function to load music and long ambience tracks.
		/// </summary>
		public static Sound LoadStreamedSound(string path)
		{
			var buffer = FileLoader.LoadFileAsBuffer(path);

			// Internal FMOD pointer points to this memory, so we don't want it to go anywhere.
			var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

			var info = new FMOD.CREATESOUNDEXINFO();
			info.length = (uint)buffer.Length;
			info.cbsize = Marshal.SizeOf(info);

			FMODSystem.createStream(
					buffer,
					FMOD.MODE.OPENMEMORY | FMOD.MODE.CREATESTREAM,
					ref info,
					out FMOD.Sound newSound
			);

			return new Sound(newSound, buffer, handle);
		}


	}
}