using System;

// DO NOT include FMOD namespace in ANY of your classes.
// Use FMOD.SomeClass instead.
// FMOD classes seriously interfere with System namespace.

namespace ChaiFoxes.FMODAudio.Studio
{
	/// <summary>
	/// Bus class.
	/// Represents a global mixer bus within FMOD Studio.
	/// </summary>
	public class Bus
	{
		public readonly FMOD.Studio.Bus Native;

		/// <summary>
		/// Bus target volume.
		/// 1 - Normal volume.
		/// 0 - Muted.
		/// -1 - Inverted Signal.
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
		/// Bus current volume.
		/// 1 - Normal volume.
		/// 0 - Muted.
		/// -1 - Inverted Signal.
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
		/// Tells if the bus is muted.
		/// Muting a bus will override its inputs,
		/// but they will obey their individual mute states once it is unmuted.
		/// </summary>
		public bool Muted
		{
			get
			{
				Native.getMute(out var mute);
				return mute;
			}
			set =>
				Native.setMute(value);
		}

		/// <summary>
		/// The bus' core channel group.
		/// Please be aware that, by default, the channel group will be deleted when it isn't needed.
		/// </summary>
		public ChannelGroup ChannelGroup
		{
			get
			{
				Native.getChannelGroup(out var sound);
				sound.getUserData(out var ptr);
				return ChannelGroup._linker.Get(ptr);
			}
		}

		/// <summary>
		/// Tells if the bus is paused.
		/// Pausing a bus will override its inputs,
		/// but they will obey their individual playback states once it is unpaused.
		/// </summary>
		public bool Paused
		{
			get
			{
				Native.getPaused(out var paused);
				return paused;
			}

			set =>
				Native.setPaused(value);
		}

		/// <summary>
		/// The Bus's internal path, i.e. "bus:/SFX/Ambience".
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
		/// The Bus's GUID.
		/// </summary>
		public Guid ID
		{
			get
			{
				Native.getID(out Guid id);
				return id;
			}
		}

		internal Bus(FMOD.Studio.Bus bus)
		{
			Native = bus;
			// TODO: Figure out where those are created.
		}

		/// <summary>
		/// Forces FMOD to create the channel group if it doesn't exist yet,
		/// as well as forcing it to stay loaded.
		/// </summary>
		public void LockChannelGroup() =>
			Native.lockChannelGroup();

		/// <summary>
		/// Allows FMOD to destroy the channel group when it isn't needed.
		/// </summary>
		public void UnlockChannelGroup() =>
			Native.unlockChannelGroup();

		/// <summary>
		/// Stop all events routed to the bus.
		/// </summary>
		public void StopAllEvents(bool immediate = false) =>
			Native.stopAllEvents(immediate ? FMOD.Studio.STOP_MODE.IMMEDIATE : FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	}
}
