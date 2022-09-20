using ChaiFoxes.FMODAudio.Studio;
using System;

namespace ChaiFoxes.FMODAudio
{
	public enum FMODMode
	{ 
		Core,
		CoreAndStudio
	}

	public static class FMODManager
	{
		/// <summary>
		/// This is the FMOD version which was tested on this
		/// version of the library. Other versions may work, 
		/// but this is not guaranteed.
		/// Visit https://fmod.com/download
		/// </summary>
		public const string RecommendedNativeLibraryVersion = "2.00.08";

		private static FMODMode _mode;

		internal static bool _initialized { get; private set; } = false;

		public static bool UsesStudio => 
			_mode == FMODMode.CoreAndStudio;

		/// <summary>
		/// Initializes systems and loads the native libraries. Can only be called once. 
		/// </summary>
		/// <param name="preInitAction">Executes before initialization, but after the native instance creation.</param>
		public static void Init(
			FMODMode mode,
			string rootDir,
			int maxChannels = 256,
			uint dspBufferLength = 4,
			int dspBufferCount = 32,
			FMOD.INITFLAGS coreInitFlags = FMOD.INITFLAGS.CHANNEL_LOWPASS | FMOD.INITFLAGS.CHANNEL_DISTANCEFILTER,
			FMOD.Studio.INITFLAGS studioInitFlags = FMOD.Studio.INITFLAGS.NORMAL,
			Action preInitAction = null
		)
		{
			if (_initialized)
			{
				throw new Exception("Manager is already initialized!");	
			}
			_initialized = true;
			_mode = mode;

			FileLoader.RootDirectory = rootDir;
			NativeLibraryLoader.LoadNativeLibrary("fmod");

			if (UsesStudio)
			{ 
				NativeLibraryLoader.LoadNativeLibrary("fmodstudio");

				// There is a requirement when using C# to call into the Core API before calling FMOD.Studio.System.create.
				// This is due to a limitation with dependency loading in the C# runtime.
				// Any call into the Core API would satisfy this criteria.
				// FMOD.Memory.GetStats as an innocuous and relatively low overhead way to meet this requirement.
				// Not doing this results in Linux core system not working correctly.		
				FMOD.Memory.GetStats(out var currentallocated, out var maxallocated);

				FMOD.Studio.System.create(out StudioSystem.Native);
				StudioSystem.Native.getCoreSystem(out CoreSystem.Native);
				
				preInitAction?.Invoke();

				// This also will init core system. 
				StudioSystem.Native.initialize(maxChannels, studioInitFlags, coreInitFlags, (IntPtr)0);
			}
			else
			{
				FMOD.Factory.System_Create(out CoreSystem.Native);

				preInitAction?.Invoke();

				CoreSystem.Native.init(maxChannels, coreInitFlags, (IntPtr)0);
			}

			// Too high values will cause sound lag.
			CoreSystem.Native.setDSPBufferSize(dspBufferLength, dspBufferCount);

		}

		public static void Update()
		{ 
			CheckInitialized();
			if (UsesStudio)
			{ 
				// Studio update updates core system internally.
				// 2020 design awards winner material right here.
				StudioSystem.Native.update();
			}
			else
			{
				CoreSystem.Native.update();	
			}
		}

		public static void Unload()
		{
			CheckInitialized();
			if (UsesStudio)
			{
				StudioSystem.Native.release();
			}
			else
			{
				CoreSystem.Native.release();
			}
		}

		private static void CheckInitialized()
		{ 
			if (!_initialized)
			{ 
				throw new Exception("You need to call Init() before calling this method!");
			}
		}
	}
}
