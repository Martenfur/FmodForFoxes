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
		private static FMODMode _mode;

		private static bool _initialized = false;

		public static bool UsesStudio => 
			_mode == FMODMode.CoreAndStudio;

		public static void Init(
			FMODMode mode,
			string rootDir,
			int maxChannels = 256,
			uint dspBufferLength = 4,
			int dspBufferCount = 32,
			FMOD.INITFLAGS coreInitFlags = FMOD.INITFLAGS.CHANNEL_LOWPASS | FMOD.INITFLAGS.CHANNEL_DISTANCEFILTER,
			FMOD.Studio.INITFLAGS studioInitFlags = FMOD.Studio.INITFLAGS.NORMAL
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

				FMOD.Studio.System.create(out StudioSystem.Native);
				
				StudioSystem.Native.getCoreSystem(out CoreSystem.Native);
				
				// This also will init core system. 
				StudioSystem.Native.initialize(maxChannels, studioInitFlags, coreInitFlags, (IntPtr)0);
			}
			else
			{
				FMOD.Factory.System_Create(out CoreSystem.Native);

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
