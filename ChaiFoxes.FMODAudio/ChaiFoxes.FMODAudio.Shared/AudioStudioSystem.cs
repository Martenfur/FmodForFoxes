using ChaiFoxes.FMODAudio.Studio;
using System;
using System.IO;

namespace ChaiFoxes.FMODAudio.Shared
{
	public static class AudioStudioSystem
	{
		/// <summary>
		/// FMOD studio sound system.
		/// </summary>
		public static FMOD.Studio.System FMODStudioSystem;

		/// <summary>
		/// Initializes FMOD Studio with default parameters.
		/// 
		/// If you want to exclusively use the default wrapper, call
		/// LoadNativeLibraries() instead.
		/// </summary>
		public static void Init(string rootDir) =>
				Init(rootDir, 256, FMOD.Studio.INITFLAGS.NORMAL, FMOD.INITFLAGS.NORMAL);

		/// <summary>
		/// Initializes FMOD Studio with custom parameters.
		/// 
		/// If you want to exclusively use the default wrapper, call
		/// LoadNativeLibraries() instead.
		/// </summary>
		public static void Init(
				string rootDir,
				int maxChannels,
				FMOD.Studio.INITFLAGS studioInitFlags,
				FMOD.INITFLAGS initFlags
		)
		{
			FileLoader.RootDirectory = rootDir;
			NativeLibraryLoader.LoadNativeLibrary("fmod");
			NativeLibraryLoader.LoadNativeLibrary("fmodstudio");

			FMOD.Studio.System.create(out FMOD.Studio.System system);
			FMODStudioSystem = system;

			FMODStudioSystem.getCoreSystem(out FMOD.System coreSystem);

			FMODStudioSystem.initialize(maxChannels, studioInitFlags, initFlags, (IntPtr)0);
		}

		public static void Update() =>
			FMODStudioSystem.update();

		public static void Unload() =>
			FMODStudioSystem.update();

		/// <summary>
		/// Loads bank from file with the default flag.
		/// </summary>
		public static Bank LoadBank(string name) =>
				LoadBank(name, FMOD.Studio.LOAD_BANK_FLAGS.NORMAL);

		/// <summary>
		/// Loads bank from file with custom flags.
		/// </summary>
		public static Bank LoadBank(string path, FMOD.Studio.LOAD_BANK_FLAGS flags)
		{
			FMODStudioSystem.loadBankMemory(
				FileLoader.LoadFileAsBuffer(path),
				flags,
				out FMOD.Studio.Bank bank
			);

			return new Bank(bank);
		}

		/// <summary>
		/// Retrieves an event via internal path, i.e. "event:/UI/Cancel", or ID string, i.e. "{2a3e48e6-94fc-4363-9468-33d2dd4d7b00}".
		/// </summary>
		public static EventDescription GetEvent(string path)
		{
			FMODStudioSystem.getEvent(path, out FMOD.Studio.EventDescription eventDescription);
			return new EventDescription(eventDescription);
		}

		/// <summary>
		/// Retrieves an event via 128-bit GUID.
		/// To parse a GUID from a string id, i.e. "{2a3e48e6-94fc-4363-9468-33d2dd4d7b00}", use FMOD.Studio.Util.parseID().
		/// </summary>
		public static EventDescription GetEvent(Guid id)
		{
			FMODStudioSystem.getEventByID(id, out FMOD.Studio.EventDescription eventDescription);
			return new EventDescription(eventDescription);
		}

		/// <summary>
		/// Retrieves a VCA via internal path, i.e. "vca:/MyVCA", or ID string, i.e. "{d9982c58-a056-4e6c-b8e3-883854b4bffb}".
		/// </summary>
		public static VCA GetVCA(string path)
		{
			FMODStudioSystem.getVCA(path, out FMOD.Studio.VCA vca);
			return new VCA(vca);
		}

		/// <summary>
		/// Retrieves a VCA via 128-bit GUID.
		/// To parse a GUID from a string id, i.e. "{d9982c58-a056-4e6c-b8e3-883854b4bffb}", use FMOD.Studio.Util.parseID().
		/// </summary>
		public static VCA GetVCA(Guid id)
		{
			FMODStudioSystem.getVCAByID(id, out FMOD.Studio.VCA vca);
			return new VCA(vca);
		}

		/// <summary>
		/// Retrieves a global parameter description by its name.
		/// </summary>
		public static FMOD.Studio.PARAMETER_DESCRIPTION GetParameterDescription(string name)
		{
			FMODStudioSystem.getParameterDescriptionByName(name, out FMOD.Studio.PARAMETER_DESCRIPTION parameter);
			return parameter;
		}

		/// <summary>
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
		public static void SetParameterValue(string name, float value, bool ignoreSeekSpeed = false) =>
			FMODStudioSystem.setParameterByName(name, value, ignoreSeekSpeed);

		/// <summary>
		/// STUDIO:
		/// Sets a global parameter's value via its ID.
		/// Enable ignoreSeekSpeed to set the value instantly, ignoring the parameter's seek speed.
		/// </summary>
		public static void SetParameterValue(FMOD.Studio.PARAMETER_ID id, float value, bool ignoreSeekSpeed = false) =>
			FMODStudioSystem.setParameterByID(id, value, ignoreSeekSpeed);

		/// <summary>
		/// STUDIO:
		/// Sets multiple global parameters' values via their IDs.
		/// Enable ignoreSeekSpeed to set the values instantly, ignoring the parameters' seek speeds.
		/// </summary>
		public static void SetParameterValues(FMOD.Studio.PARAMETER_ID[] ids, float[] values, bool ignoreSeekSpeed = false) =>
			FMODStudioSystem.setParametersByIDs(ids, values, ids.Length, ignoreSeekSpeed);
		
	}
}
