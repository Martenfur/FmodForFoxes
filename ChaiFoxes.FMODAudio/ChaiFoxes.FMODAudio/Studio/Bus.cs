using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;
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
		protected FMOD.Studio.Bus _Bus { get; private set; }

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
				_Bus.getVolume(out float volume);
				return volume;
			}
			set =>
				_Bus.setVolume(value);
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
				_Bus.getVolume(out _, out float finalVolume);
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
				_Bus.getMute(out bool mute);
				return mute;
			}
			set =>
				_Bus.setMute(value);
		}

		/// <summary>
		/// The bus' core channel group.
		/// Please be aware that, by default, the channel group will be deleted when it isn't needed.
		/// </summary>
		public FMOD.ChannelGroup ChannelGroup
		{
			get
			{
				_Bus.getChannelGroup(out FMOD.ChannelGroup channelGroup);
				return channelGroup;
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
				_Bus.getPaused(out bool paused);
				return paused;
			}

			set =>
				_Bus.setPaused(value);
		}

		/// <summary>
		/// The Bus's internal path, i.e. "bus:/SFX/Ambience".
		/// </summary>
		public string Path
		{
			get
			{
				_Bus.getPath(out string path);
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
				_Bus.getID(out Guid id);
				return id;
			}
		}

		public Bus(FMOD.Studio.Bus bus)
		{
			_Bus = bus;
		}

		/// <summary>
		/// Forces FMOD to create the channel group if it doesn't exist yet,
		/// as well as forcing it to stay loaded.
		/// </summary>
		public void LockChannelGroup() =>
			_Bus.lockChannelGroup();

		/// <summary>
		/// Allows FMOD to destroy the channel group when it isn't needed.
		/// </summary>
		public void UnlockChannelGroup() =>
			_Bus.unlockChannelGroup();

		/// <summary>
		/// Stop all events routed to the bus.
		/// </summary>
		public void StopAllEvents(bool immediate = false) =>
			_Bus.stopAllEvents(immediate ? FMOD.Studio.STOP_MODE.IMMEDIATE : FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	}
}
