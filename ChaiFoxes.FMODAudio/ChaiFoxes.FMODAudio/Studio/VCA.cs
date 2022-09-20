using System;

// DO NOT include FMOD namespace in ANY of your classes.
// Use FMOD.SomeClass instead.
// FMOD classes seriously interfere with System namespace.

namespace ChaiFoxes.FMODAudio.Studio
{
	/// <summary>
	/// VCA class. Allows for grouping multiple buses together over different group buses, returns, and even events.
	/// </summary>
	public class VCA
	{
		public readonly FMOD.Studio.VCA Native;

		/// <summary>
		/// VCA target volume.
		/// 1 - Normal volume.
		/// 0 - Muted.
		/// This ignores modulation / automation applied to the volume within Studio.
		/// </summary>
		public float Volume
		{
			get
			{
				Native.getVolume(out var volume);
				return volume;
			}
			set =>
				Native.setVolume(value);
		}

		/// <summary>
		/// VCA current volume.
		/// 1 - Normal volume.
		/// 0 - Muted.
		/// This takes into account modulation / automation applied to the volume within Studio.
		/// </summary>
		public float CurrentVolume
		{
			get
			{
				Native.getVolume(out _, out var finalVolume);
				return finalVolume;
			}
		}

		/// <summary>
		/// The VCA's internal path, i.e. "vca:/MyVCA".
		/// </summary>
		public string Path
		{
			get
			{
				Native.getPath(out string path);
				return path;
			}
		}

		/// <summary>
		/// The VCA's GUID.
		/// </summary>
		// TODO: Convert back.
		public FMOD.GUID ID
		{
			get
			{
				Native.getID(out FMOD.GUID id);
				return id;
			}
		}

		internal VCA(FMOD.Studio.VCA vca)
		{
			Native = vca;
		}
	}
}
