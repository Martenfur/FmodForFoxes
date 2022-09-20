using System;

// DO NOT include FMOD namespace in ANY of your classes.
// Use FMOD.SomeClass instead.
// FMOD classes seriously interfere with System namespace.

namespace ChaiFoxes.FMODAudio.Studio
{
	/// <summary>
	/// Bank class. Stores event metadata and sound data all in one file.
	/// </summary>
	public class Bank
	{
		public readonly FMOD.Studio.Bank Native;

		/// <summary>
		/// The bank's arbitrary user data.
		/// </summary>
		public IntPtr UserData
		{
			get
			{
				Native.getUserData(out IntPtr userData);
				return userData;
			}

			set =>
				 Native.setUserData(value);
		}

		internal Bank(FMOD.Studio.Bank bank)
		{
			Native = bank;
		}

		/// <summary>
		/// Loads all non-streaming sounds in the bank.
		/// </summary>
		public void LoadSampleData() => 
			Native.loadSampleData();

		/// <summary>
		/// Unloads all non-streaming sounds in the bank.
		/// </summary>
		public void UnloadSampleData() => 
			Native.unloadSampleData();

		/// <summary>
		/// Unloads the bank, invalidating all related event descriptions and destroying associated instances.
		/// </summary>
		public void Unload() => 
			Native.unload();
	}
}
