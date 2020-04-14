using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;
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
		protected FMOD.Studio.Bank _bank { get; private set; }

		/// <summary>
		/// The bank's arbitrary user data.
		/// </summary>
		public IntPtr UserData
		{
			set =>
				 _bank.setUserData(value);

			get
			{
				_bank.getUserData(out IntPtr userData);
				return userData;
			}
		}

		public Bank(FMOD.Studio.Bank bank)
		{
			_bank = bank;
		}

		/// <summary>
		/// Loads all non-streaming sounds in the bank.
		/// </summary>
		public void LoadSampleData() => _bank.loadSampleData();

		/// <summary>
		/// Unloads all non-streaming sounds in the bank.
		/// </summary>
		public void UnloadSampleData() => _bank.unloadSampleData();

		/// <summary>
		/// Unloads the bank, invalidating all related event descriptions and destroying associated instances.
		/// </summary>
		public void Unload() => _bank.unload();
	}
}
