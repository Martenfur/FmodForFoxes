using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;
using System;

// DO NOT include FMOD namespace in ANY of your classes.
// Use FMOD.SomeClass instead.
// FMOD classes seriously interfere with System namespace.

namespace ChaiFoxes.FMODAudio
{
    /// <summary>
    /// Bank class. Stores event metadata and sound data all in one file.
    /// </summary>
    public class Bank
    {
        protected FMOD.Studio.Bank _FMODBank { get; private set; }

        /// <summary>
        /// The bank's arbitrary user data.
        /// </summary>
        public IntPtr UserData
        {
            set =>
                 _FMODBank.setUserData(value);

            get
            {
                _FMODBank.getUserData(out IntPtr userData);
                return userData;
            }
        }

        public Bank(FMOD.Studio.Bank bank)
        {
            _FMODBank = bank;
        }

        /// <summary>
        /// Loads all non-streaming sounds in the bank.
        /// </summary>
        public void LoadSampleData() => _FMODBank.loadSampleData();

        /// <summary>
        /// Unloads all non-streaming sounds in the bank.
        /// </summary>
        public void UnloadSampleData() => _FMODBank.unloadSampleData();

        /// <summary>
        /// Unloads the bank, invalidating all related event descriptions and destroying associated instances.
        /// </summary>
		public void Unload() => _FMODBank.unload();
	}
}
