using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;
using System;

// DO NOT include FMOD namespace in ANY of your classes.
// Use FMOD.SomeClass instead.
// FMOD classes seriously interfere with System namespace.

namespace ChaiFoxes.FMODAudio.Studio
{
	/// <summary>
	/// Event instance wrapper.
	/// An event can theoretically have any number of these, but only one event description.
	/// </summary>
	public class EventInstance
	{
		/// <summary>
		/// FMOD event instance using the default wrapper. Use this if you need full FMOD functionality.
		/// </summary>
		public FMOD.Studio.EventInstance Instance => _FMODEventInstance;
		protected FMOD.Studio.EventInstance _FMODEventInstance;

		/// <summary>
		/// Event description, from which this instance was created.
		/// </summary>
		public readonly EventDescription Description;

		/// <summary>
		/// Event pitch. Affects speed too.
		/// 1 - Normal pitch.
		/// More than 1 - Higher pitch.
		/// Less than 1 - Lower pitch.
		/// </summary>
		public float Pitch
		{
			get
			{
				_FMODEventInstance.getPitch(out float pitch);
				return pitch;
			}
			set =>
				_FMODEventInstance.setPitch(value);
		}

		/// <summary>
		/// Event target volume.
		/// 1 - Normal volume.
		/// 0 - Muted.
		/// This ignores modulation / automation applied to the volume within Studio.
		/// </summary>
		public float Volume
		{
			get
			{
				_FMODEventInstance.getVolume(out float targetVolume);
				return targetVolume;
			}
			set =>
				_FMODEventInstance.setVolume(value);
		}

		/// <summary>
		/// Event current volume.
		/// 1 - Normal volume.
		/// 0 - Muted.
		/// This takes into account modulation / automation applied to the volume within Studio.
		/// </summary>
		public float CurrentVolume
		{
			get
			{
				_FMODEventInstance.getVolume(out _, out float currentVolume);
				return currentVolume;
			}
		}

		/// <summary>
		/// Retrieves whether or not the event is 3D.
		/// </summary>
		public bool Is3D
		{
			get => Description.Is3D;
		}

		/// <summary>
		/// Event's 3D Attributes.
		/// </summary>
		public Attributes3D Attributes
		{
			get
			{
				_FMODEventInstance.get3DAttributes(out FMOD.ATTRIBUTES_3D attributes);

				return attributes.ToAttributes3D();
			}
			set
			{
				_FMODEventInstance.set3DAttributes(value.ToFmodAttributes());
			}
		}

		/// <summary>
		/// Timeline position in milliseconds.
		/// </summary>
		public int TimelinePosition
		{
			get
			{
				_FMODEventInstance.getTimelinePosition(out int position);
				return position;
			}
			set =>
				_FMODEventInstance.setTimelinePosition(value);
		}

		/// <summary>
		/// This event instance's current playback state.
		/// If the instance is invalid, this will default to PLAYBACK_STATE.STOPPED.
		/// </summary>
		public FMOD.Studio.PLAYBACK_STATE PlaybackState
		{
			get
			{
				_FMODEventInstance.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
				return state;
			}
		}

		/// <summary>
		/// Tells if the event is paused.
		/// </summary>
		public bool Paused
		{
			get
			{
				_FMODEventInstance.getPaused(out bool paused);
				return paused;
			}

			set =>
				_FMODEventInstance.setPaused(value);
		}

		/// <summary>
		/// The event instance's arbitrary user data.
		/// </summary>
		public IntPtr UserData
		{
			set =>
				 _FMODEventInstance.setUserData(value);

			get
			{
				_FMODEventInstance.getUserData(out IntPtr userData);
				return userData;
			}
		}

		public EventInstance(EventDescription eventDescription, FMOD.Studio.EventInstance eventInstance)
		{
			Description = eventDescription;
			_FMODEventInstance = eventInstance;

			Volume = eventDescription.Volume;
			Pitch = eventDescription.Pitch;
			Attributes = eventDescription.Attributes;
		}

		/// <summary>
		/// Retrieves a parameter's current value via its name (case sensitive).
		/// This ignores modulation / automation applied to the parameter within Studio.
		/// </summary>
		public float GetParameterTargetValue(string name)
		{
			_FMODEventInstance.getParameterByName(name, out float value, out _);
			return value;
		}

		/// <summary>
		/// Retrieves a parameter's current value via its ID.
		/// This ignores modulation / automation applied to the parameter within Studio.
		/// </summary>
		public float GetParameterTargetValue(FMOD.Studio.PARAMETER_ID id)
		{
			_FMODEventInstance.getParameterByID(id, out float value, out _);
			return value;
		}

		/// <summary>
		/// Retrieves a parameter's current value via its name (case sensitive).
		/// This takes into account modulation / automation applied to the parameter within Studio.
		/// </summary>
		public float GetParameterCurrentValue(string name)
		{
			_FMODEventInstance.getParameterByName(name, out _, out float finalValue);
			return finalValue;
		}

		/// <summary>
		/// Retrieves a parameter's current value via its ID.
		/// This takes into account modulation / automation applied to the parameter within Studio.
		/// </summary>
		public float GetParameterCurrentValue(FMOD.Studio.PARAMETER_ID id)
		{
			_FMODEventInstance.getParameterByID(id, out _, out float finalValue);
			return finalValue;
		}

		/// <summary>
		/// Sets a parameter's value via its name (case sensitive).
		/// Enable ignoreSeekSpeed to set the value instantly, ignoring the parameter's seek speed.
		/// </summary>
		public void SetParameterValue(string name, float value, bool ignoreSeekSpeed = false)
		{
			_FMODEventInstance.setParameterByName(name, value, ignoreSeekSpeed);
		}

		/// <summary>
		/// Sets a parameter's value via its ID.
		/// Enable ignoreSeekSpeed to set the value instantly, ignoring the parameter's seek speed.
		/// </summary>
		public void SetParameterValue(FMOD.Studio.PARAMETER_ID id, float value, bool ignoreSeekSpeed = false)
		{
			_FMODEventInstance.setParameterByID(id, value, ignoreSeekSpeed);
		}

		/// <summary>
		/// Sets multiple parameters' values via their IDs.
		/// Enable ignoreSeekSpeed to set the values instantly, ignoring the parameters' seek speeds.
		/// </summary>
		public void SetParameterValues(FMOD.Studio.PARAMETER_ID[] ids, float[] values, bool ignoreSeekSpeed = false)
		{
			_FMODEventInstance.setParametersByIDs(ids, values, ids.Length, ignoreSeekSpeed);
		}

		/// <summary>
		/// Assigns a user callback for this specific event instance.
		/// </summary>
		public void SetCallback(FMOD.Studio.EVENT_CALLBACK callback, FMOD.Studio.EVENT_CALLBACK_TYPE callbackMask) =>
			_FMODEventInstance.setCallback(callback, callbackMask);

		public void Start() =>
			_FMODEventInstance.start();

		public void Stop(bool immediate = false) =>
			_FMODEventInstance.stop(immediate ? FMOD.Studio.STOP_MODE.IMMEDIATE : FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

		/// <summary>
		/// Allows the timeline cursor to move past sustain points.
		/// </summary>
		public void TriggerCue() =>
			_FMODEventInstance.triggerCue();

		public void Release() =>
			_FMODEventInstance.release();
	}
}