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
	public static class AudioMgr
	{
		/// <summary>
		/// FMOD studio sound system.
		/// </summary>
		public static FMOD.Studio.System FMODStudioSystem;

		/// <summary>
		/// Low level FMOD sound system.
		/// </summary>
		public static FMOD.System FMODSystem;

		/// <summary>
		/// Root directory for banks, music, sounds, etc.
		/// </summary>
		public static string _rootDir;

		/// <summary>
		/// Returns true if initialized with Studio, false for the core FMOD system.
		/// </summary>
		private static bool _studioLoaded;

		/// <summary>
		/// Initializes FMOD Studio with default parameters.
		/// 
		/// If you want to exclusively use the default wrapper, call
		/// LoadNativeLibraries() instead.
		/// </summary>
		public static void InitStudio(string rootDir) =>
				InitStudio(rootDir, 256, FMOD.Studio.INITFLAGS.NORMAL, FMOD.INITFLAGS.NORMAL);

		// TODO: Split Studio into its own namespace. Don't touch Core

		/// <summary>
		/// Initializes FMOD Studio with custom parameters.
		/// 
		/// If you want to exclusively use the default wrapper, call
		/// LoadNativeLibraries() instead.
		/// </summary>
		public static void InitStudio(
				string rootDir,
				int maxChannels,
				FMOD.Studio.INITFLAGS studioInitFlags,
				FMOD.INITFLAGS initFlags
		)
		{
			_rootDir = rootDir;
			LoadNativeLibrary();

			FMOD.Studio.System.create(out FMOD.Studio.System system);
			FMODStudioSystem = system;

			FMODStudioSystem.getCoreSystem(out FMOD.System coreSystem);
			FMODSystem = coreSystem;

			FMODStudioSystem.initialize(maxChannels, studioInitFlags, initFlags, (IntPtr)0);
			_studioLoaded = true;
		}

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
		/// LoadNativeLibraries() instead.
		/// </summary>
		public static void Init(
				string rootDir,
				uint dspBufferLength,
				int dspBufferCount,
				int maxChannels,
				FMOD.INITFLAGS initFlags
		)
		{
			_rootDir = rootDir;
			LoadNativeLibrary(false);

			FMOD.Factory.System_Create(out FMOD.System system);
			FMODSystem = system;

			// Too high values will cause sound lag.
			FMODSystem.setDSPBufferSize(dspBufferLength, dspBufferCount);

			FMODSystem.init(maxChannels, initFlags, (IntPtr)0);
			_studioLoaded = false;
		}

		public static void Update()
		{
			if (_studioLoaded)
			{
				FMODStudioSystem.update();
			}
			else
			{
				FMODSystem.update();
			}
		}

		public static void Unload()
		{
			if (_studioLoaded)
			{
				FMODStudioSystem.release();
			}
			else
			{
				FMODSystem.release();
			}
		}

		/// <summary>
		/// STUDIO:
		/// Loads bank from file with the default flag.
		/// </summary>
		public static Bank LoadBank(string name) =>
				LoadBank(name, FMOD.Studio.LOAD_BANK_FLAGS.NORMAL);

		/// <summary>
		/// STUDIO:
		/// Loads bank from file with custom flags.
		/// </summary>
		public static Bank LoadBank(string name, FMOD.Studio.LOAD_BANK_FLAGS flags)
		{
			FMODStudioSystem.loadBankFile(
					Path.Combine(_rootDir, name),
					flags,
					out FMOD.Studio.Bank bank);

			bank.getPath(out string path);

			return new Bank(bank);
		}

		/// <summary>
		/// STUDIO:
		/// Retrieves an event via internal path, i.e. "event:/UI/Cancel", or ID string, i.e. "{2a3e48e6-94fc-4363-9468-33d2dd4d7b00}".
		/// </summary>
		public static EventDescription GetEvent(string path)
		{
			FMODStudioSystem.getEvent(path, out FMOD.Studio.EventDescription eventDescription);
			return new EventDescription(eventDescription);
		}

		/// <summary>
		/// STUDIO:
		/// Retrieves an event via 128-bit GUID.
		/// To parse a GUID from a string id, i.e. "{2a3e48e6-94fc-4363-9468-33d2dd4d7b00}", use FMOD.Studio.Util.parseID().
		/// </summary>
		public static EventDescription GetEvent(Guid id)
		{
			FMODStudioSystem.getEventByID(id, out FMOD.Studio.EventDescription eventDescription);
			return new EventDescription(eventDescription);
		}

		/// <summary>
		/// STUDIO:
		/// Retrieves a VCA via internal path, i.e. "vca:/MyVCA", or ID string, i.e. "{d9982c58-a056-4e6c-b8e3-883854b4bffb}".
		/// </summary>
		public static VCA GetVCA(string path)
		{
			FMODStudioSystem.getVCA(path, out FMOD.Studio.VCA vca);
			return new VCA(vca);
		}

		/// <summary>
		/// STUDIO:
		/// Retrieves a VCA via 128-bit GUID.
		/// To parse a GUID from a string id, i.e. "{d9982c58-a056-4e6c-b8e3-883854b4bffb}", use FMOD.Studio.Util.parseID().
		/// </summary>
		public static VCA GetVCA(Guid id)
		{
			FMODStudioSystem.getVCAByID(id, out FMOD.Studio.VCA vca);
			return new VCA(vca);
		}

		/// <summary>
		/// STUDIO:
		/// Retrieves a global parameter description by its name.
		/// </summary>
		public static FMOD.Studio.PARAMETER_DESCRIPTION GetParameterDescription(string name)
		{
			FMODStudioSystem.getParameterDescriptionByName(name, out FMOD.Studio.PARAMETER_DESCRIPTION parameter);
			return parameter;
		}

		/// <summary>
		/// STUDIO:
		/// Retrieves a global parameter description by its ID.
		/// </summary>
		public static FMOD.Studio.PARAMETER_DESCRIPTION GetParameterDescription(FMOD.Studio.PARAMETER_ID id)
		{
			FMODStudioSystem.getParameterDescriptionByID(id, out FMOD.Studio.PARAMETER_DESCRIPTION parameter);
			return parameter;
		}

		/// <summary>
		/// STUDIO:
		/// Retrieves a global parameter's current value via its name (case sensitive).
		/// This ignores modulation / automation applied to the parameter within Studio.
		/// </summary>
		public static float GetParameterTargetValue(string name)
		{
			FMODStudioSystem.getParameterByName(name, out float value, out _);
			return value;
		}

		/// <summary>
		/// STUDIO:
		/// Retrieves a global parameter's current value via its ID.
		/// This ignores modulation / automation applied to the parameter within Studio.
		/// </summary>
		public static float GetParameterTargetValue(FMOD.Studio.PARAMETER_ID id)
		{
			FMODStudioSystem.getParameterByID(id, out float value, out _);
			return value;
		}

		/// <summary>
		/// STUDIO:
		/// Retrieves a global parameter's current value via its name (case sensitive).
		/// This takes into account modulation / automation applied to the parameter within Studio.
		/// </summary>
		public static float GetParameterCurrentValue(string name)
		{
			FMODStudioSystem.getParameterByName(name, out _, out float finalValue);
			return finalValue;
		}

		/// <summary>
		/// STUDIO:
		/// Retrieves a global parameter's current value via its ID.
		/// This takes into account modulation / automation applied to the parameter within Studio.
		/// </summary>
		public static float GetParameterCurrentValue(FMOD.Studio.PARAMETER_ID id)
		{
			FMODStudioSystem.getParameterByID(id, out _, out float finalValue);
			return finalValue;
		}

		/// <summary>
		/// STUDIO:
		/// Sets a global parameter's value via its name (case sensitive).
		/// Enable ignoreSeekSpeed to set the value instantly, ignoring the parameter's seek speed.
		/// </summary>
		public static void SetParameterValue(string name, float value, bool ignoreSeekSpeed = false)
		{
			FMODStudioSystem.setParameterByName(name, value, ignoreSeekSpeed);
		}

		/// <summary>
		/// STUDIO:
		/// Sets a global parameter's value via its ID.
		/// Enable ignoreSeekSpeed to set the value instantly, ignoring the parameter's seek speed.
		/// </summary>
		public static void SetParameterValue(FMOD.Studio.PARAMETER_ID id, float value, bool ignoreSeekSpeed = false)
		{
			FMODStudioSystem.setParameterByID(id, value, ignoreSeekSpeed);
		}

		/// <summary>
		/// STUDIO:
		/// Sets multiple global parameters' values via their IDs.
		/// Enable ignoreSeekSpeed to set the values instantly, ignoring the parameters' seek speeds.
		/// </summary>
		public static void SetParameterValues(FMOD.Studio.PARAMETER_ID[] ids, float[] values, bool ignoreSeekSpeed = false)
		{
			FMODStudioSystem.setParametersByIDs(ids, values, ids.Length, ignoreSeekSpeed);
		}

		/// <summary>
		/// CORE:
		/// Create new channel group with given name.
		/// </summary>
		public static FMOD.ChannelGroup CreateChannelGroup(string name)
		{
			FMODSystem.createChannelGroup(name, out FMOD.ChannelGroup channelGroup);
			return channelGroup;
		}

		/// <summary>
		/// CORE:
		/// Loads sound from file.
		/// Use this function to load short sound effects.
		/// </summary>
		public static Sound LoadSound(string name)
		{
			var buffer = LoadFileAsBuffer(Path.Combine(_rootDir, name));

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
		/// CORE:
		/// Loads streamed sound stream from file.
		/// Use this function to load music and long ambience tracks.
		/// </summary>
		public static Sound LoadStreamedSound(string name)
		{
			var buffer = LoadFileAsBuffer(Path.Combine(_rootDir, name));

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

		/// <summary>
		/// CORE:
		/// Loads file as a byte array.
		/// </summary>
		private static byte[] LoadFileAsBuffer(string path)
		{
			// TitleContainer is cross-platform Monogame file loader.
			var stream = TitleContainer.OpenStream(path);

			// File is opened as a stream, so we need to read it to the end.
			byte[] buffer = new byte[16 * 1024];
			byte[] bufferRes;
			using (MemoryStream ms = new MemoryStream())
			{
				int read;
				while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, read);
				}
				bufferRes = ms.ToArray();
			}
			return bufferRes;
		}
	}
}