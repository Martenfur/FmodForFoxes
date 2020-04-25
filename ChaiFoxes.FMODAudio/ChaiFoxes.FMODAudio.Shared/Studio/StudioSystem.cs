using System;

namespace ChaiFoxes.FMODAudio.Studio
{
	public static class StudioSystem
	{
		/// <summary>
		/// FMOD studio sound system.
		/// </summary>
		public static FMOD.Studio.System Native;

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
			Native.loadBankMemory(
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
			Native.getEvent(path, out FMOD.Studio.EventDescription eventDescription);
			return new EventDescription(eventDescription);
		}

		/// <summary>
		/// Retrieves an event via 128-bit GUID.
		/// To parse a GUID from a string id, i.e. "{2a3e48e6-94fc-4363-9468-33d2dd4d7b00}", use FMOD.Studio.Util.parseID().
		/// </summary>
		public static EventDescription GetEvent(Guid id)
		{
			Native.getEventByID(id, out FMOD.Studio.EventDescription eventDescription);
			return new EventDescription(eventDescription);
		}

		/// <summary>
		/// Retrieves a bus using path.
		/// Path may be a path, such as bus:/SFX/Ambience, or an ID string, such as {d9982c58-a056-4e6c-b8e3-883854b4bffb}.
		/// </summary>
		public static Bus GetBus(string path)
		{
			Native.getBus(path, out var bus);
			return new Bus(bus);
		}

		/// <summary>
		/// Retrieves a bus via 128-bit GUID.
		/// To parse a GUID from a string id, i.e. "{2a3e48e6-94fc-4363-9468-33d2dd4d7b00}", use FMOD.Studio.Util.parseID().
		/// </summary>
		public static Bus GetBusByID(Guid id)
		{
			Native.getBusByID(id, out var bus);
			return new Bus(bus);
		}


		/// <summary>
		/// Retrieves a VCA via internal path, i.e. "vca:/MyVCA", or ID string, i.e. "{d9982c58-a056-4e6c-b8e3-883854b4bffb}".
		/// </summary>
		public static VCA GetVCA(string path)
		{
			Native.getVCA(path, out var vca);
			return new VCA(vca);
		}

		/// <summary>
		/// Retrieves a VCA via 128-bit GUID.
		/// To parse a GUID from a string id, i.e. "{d9982c58-a056-4e6c-b8e3-883854b4bffb}", use FMOD.Studio.Util.parseID().
		/// </summary>
		public static VCA GetVCA(Guid id)
		{
			Native.getVCAByID(id, out var vca);
			return new VCA(vca);
		}

		/// <summary>
		/// Retrieves a global parameter description by its name.
		/// </summary>
		public static FMOD.Studio.PARAMETER_DESCRIPTION GetParameterDescription(string name)
		{
			Native.getParameterDescriptionByName(name, out FMOD.Studio.PARAMETER_DESCRIPTION parameter);
			return parameter;
		}

		/// <summary>
		/// Retrieves a global parameter description by its ID.
		/// </summary>
		public static FMOD.Studio.PARAMETER_DESCRIPTION GetParameterDescription(FMOD.Studio.PARAMETER_ID id)
		{
			Native.getParameterDescriptionByID(id, out FMOD.Studio.PARAMETER_DESCRIPTION parameter);
			return parameter;
		}

		/// <summary>
		/// Retrieves a global parameter's current value via its name (case sensitive).
		/// This ignores modulation / automation applied to the parameter within Studio.
		/// </summary>
		public static float GetParameterTargetValue(string name)
		{
			Native.getParameterByName(name, out var value, out _);
			return value;
		}

		/// <summary>
		/// Retrieves a global parameter's current value via its ID.
		/// This ignores modulation / automation applied to the parameter within Studio.
		/// </summary>
		public static float GetParameterTargetValue(FMOD.Studio.PARAMETER_ID id)
		{
			Native.getParameterByID(id, out var value, out _);
			return value;
		}

		/// <summary>
		/// Retrieves a global parameter's current value via its name (case sensitive).
		/// This takes into account modulation / automation applied to the parameter within Studio.
		/// </summary>
		public static float GetParameterCurrentValue(string name)
		{
			Native.getParameterByName(name, out _, out var finalValue);
			return finalValue;
		}

		/// <summary>
		/// Retrieves a global parameter's current value via its ID.
		/// This takes into account modulation / automation applied to the parameter within Studio.
		/// </summary>
		public static float GetParameterCurrentValue(FMOD.Studio.PARAMETER_ID id)
		{
			Native.getParameterByID(id, out _, out var finalValue);
			return finalValue;
		}

		/// <summary>
		/// Sets a global parameter's value via its name (case sensitive).
		/// Enable ignoreSeekSpeed to set the value instantly, ignoring the parameter's seek speed.
		/// </summary>
		public static void SetParameterValue(string name, float value, bool ignoreSeekSpeed = false) =>
			Native.setParameterByName(name, value, ignoreSeekSpeed);

		/// <summary>
		/// Sets a global parameter's value via its ID.
		/// Enable ignoreSeekSpeed to set the value instantly, ignoring the parameter's seek speed.
		/// </summary>
		public static void SetParameterValue(FMOD.Studio.PARAMETER_ID id, float value, bool ignoreSeekSpeed = false) =>
			Native.setParameterByID(id, value, ignoreSeekSpeed);

		/// <summary>
		/// Sets multiple global parameters' values via their IDs.
		/// Enable ignoreSeekSpeed to set the values instantly, ignoring the parameters' seek speeds.
		/// </summary>
		public static void SetParameterValues(FMOD.Studio.PARAMETER_ID[] ids, float[] values, bool ignoreSeekSpeed = false) =>
			Native.setParametersByIDs(ids, values, ids.Length, ignoreSeekSpeed);
		
	}
}
