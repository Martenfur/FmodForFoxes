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
		public FMOD.Studio.EventInstance Instance => _instance;
		protected FMOD.Studio.EventInstance _instance;

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
				_instance.getPitch(out var pitch);
				return pitch;
			}
			set =>
				_instance.setPitch(value);
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
				_instance.getVolume(out var targetVolume);
				return targetVolume;
			}
			set =>
				_instance.setVolume(value);
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
				_instance.getVolume(out _, out var currentVolume);
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
				_instance.get3DAttributes(out FMOD.ATTRIBUTES_3D attributes);

				return attributes.ToAttributes3D();
			}
			set
			{
				_instance.set3DAttributes(value.ToFmodAttributes());
			}
		}

		/// <summary>
		/// Timeline position in milliseconds.
		/// </summary>
		public int TimelinePosition
		{
			get
			{
				_instance.getTimelinePosition(out var position);
				return position;
			}
			set =>
				_instance.setTimelinePosition(value);
		}

		/// <summary>
		/// This event instance's current playback state.
		/// If the instance is invalid, this will default to PLAYBACK_STATE.STOPPED.
		/// </summary>
		public FMOD.Studio.PLAYBACK_STATE PlaybackState
		{
			get
			{
				_instance.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
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
				_instance.getPaused(out var paused);
				return paused;
			}

			set =>
				_instance.setPaused(value);
		}

		/// <summary>
		/// The event instance's arbitrary user data.
		/// </summary>
		public IntPtr UserData
		{
			set =>
				 _instance.setUserData(value);

			get
			{
				_instance.getUserData(out IntPtr userData);
				return userData;
			}
		}

		public EventInstance(EventDescription eventDescription, FMOD.Studio.EventInstance eventInstance)
		{
			Description = eventDescription;
			_instance = eventInstance;

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
			_instance.getParameterByName(name, out var value, out _);
			return value;
		}

		/// <summary>
		/// Retrieves a parameter's current value via its ID.
		/// This ignores modulation / automation applied to the parameter within Studio.
		/// </summary>
		public float GetParameterTargetValue(FMOD.Studio.PARAMETER_ID id)
		{
			_instance.getParameterByID(id, out var value, out _);
			return value;
		}

		/// <summary>
		/// Retrieves a parameter's current value via its name (case sensitive).
		/// This takes into account modulation / automation applied to the parameter within Studio.
		/// </summary>
		public float GetParameterCurrentValue(string name)
		{
			_instance.getParameterByName(name, out _, out var finalValue);
			return finalValue;
		}

		/// <summary>
		/// Retrieves a parameter's current value via its ID.
		/// This takes into account modulation / automation applied to the parameter within Studio.
		/// </summary>
		public float GetParameterCurrentValue(FMOD.Studio.PARAMETER_ID id)
		{
			_instance.getParameterByID(id, out _, out var finalValue);
			return finalValue;
		}

		/// <summary>
		/// Sets a parameter's value via its name (case sensitive).
		/// Enable ignoreSeekSpeed to set the value instantly, ignoring the parameter's seek speed.
		/// </summary>
		public void SetParameterValue(string name, float value, bool ignoreSeekSpeed = false)
		{
			_instance.setParameterByName(name, value, ignoreSeekSpeed);
		}

		/// <summary>
		/// Sets a parameter's value via its ID.
		/// Enable ignoreSeekSpeed to set the value instantly, ignoring the parameter's seek speed.
		/// </summary>
		public void SetParameterValue(FMOD.Studio.PARAMETER_ID id, float value, bool ignoreSeekSpeed = false)
		{
			_instance.setParameterByID(id, value, ignoreSeekSpeed);
		}

		/// <summary>
		/// Sets multiple parameters' values via their IDs.
		/// Enable ignoreSeekSpeed to set the values instantly, ignoring the parameters' seek speeds.
		/// </summary>
		public void SetParameterValues(FMOD.Studio.PARAMETER_ID[] ids, float[] values, bool ignoreSeekSpeed = false)
		{
			_instance.setParametersByIDs(ids, values, ids.Length, ignoreSeekSpeed);
		}

		/// <summary>
		/// Assigns a user callback for this specific event instance.
		/// </summary>
		public void SetCallback(FMOD.Studio.EVENT_CALLBACK callback, FMOD.Studio.EVENT_CALLBACK_TYPE callbackMask) =>
			_instance.setCallback(callback, callbackMask);

		public void Start() =>
			_instance.start();

		public void Stop(bool immediate = false) =>
			_instance.stop(immediate ? FMOD.Studio.STOP_MODE.IMMEDIATE : FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

		/// <summary>
		/// Allows the timeline cursor to move past sustain points.
		/// </summary>
		public void TriggerCue() =>
			_instance.triggerCue();

		public void Release() =>
			_instance.release();
	}
}