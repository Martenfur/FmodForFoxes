using Microsoft.Xna.Framework;
using System;

// DO NOT include FMOD namespace in ANY of your classes.
// Use FMOD.SomeClass instead.
// FMOD classes seriously interfere with System namespace.

namespace FmodForFoxes.Studio
{
	/// <summary>
	/// Event instance wrapper.
	/// An event can theoretically have any number of these, but only one event description.
	/// </summary>
	public class EventInstance : IDisposable
	{
		/// <summary>
		/// FMOD event instance using the default wrapper. Use this if you need full FMOD functionality.
		/// </summary>
		public readonly FMOD.Studio.EventInstance Native;

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
				Native.getPitch(out var pitch);
				return pitch;
			}
			set =>
				Native.setPitch(value);
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
				Native.getVolume(out var targetVolume);
				return targetVolume;
			}
			set =>
				Native.setVolume(value);
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
				Native.getVolume(out _, out var currentVolume);
				return currentVolume;
			}
		}

		/// <summary>
		/// Retrieves whether or not the event is 3D.
		/// </summary>
		public bool Is3D => Description.Is3D;

		/// <summary>
		/// Event's 3D Attributes.
		/// </summary>
		public Attributes3D Attributes
		{
			get
			{
				Native.get3DAttributes(out FMOD.ATTRIBUTES_3D attributes);

				return new Attributes3D(attributes);
			}
			set =>
				Native.set3DAttributes(value.ToFmodAttributes());
		}

		/// <summary>
		/// Sound's position in 3D space. Can be used only if 3D positioning is enabled.
		/// </summary>
		public Vector3 Position3D
		{
			get
			{
				Native.get3DAttributes(out FMOD.ATTRIBUTES_3D attributes);

				return attributes.position.ToVector3();
			}
			set
			{
				Native.get3DAttributes(out FMOD.ATTRIBUTES_3D attributes);
				attributes.position = value.ToFmodVector();
				Native.set3DAttributes(attributes);
			}
		}


		/// <summary>
		/// Sound's velocity in 3D space. Can be used only if 3D positioning is enabled.
		/// </summary>
		public Vector3 Velocity3D
		{
			get
			{
				Native.get3DAttributes(out FMOD.ATTRIBUTES_3D attributes);

				return attributes.velocity.ToVector3();
			}
			set
			{
				Native.get3DAttributes(out FMOD.ATTRIBUTES_3D attributes);
				attributes.velocity = value.ToFmodVector();
				Native.set3DAttributes(attributes);
			}
		}


		/// <summary>
		/// Distance from the source where attenuation begins.
		/// </summary>
		public float MinDistance3D
		{
			get
			{
				Native.getMinMaxDistance(out var minDistance, out var maxDistance);
				return minDistance;
			}
		}

		/// <summary>
		/// Distance from the source where attenuation ends.
		/// </summary>
		public float MaxDistance3D
		{
			get
			{
				Native.getMinMaxDistance(out var minDistance, out var maxDistance);
				return maxDistance;
			}
		}



		/// <summary>
		/// Timeline position in milliseconds.
		/// </summary>
		public int TimelinePosition
		{
			get
			{
				Native.getTimelinePosition(out var position);
				return position;
			}
			set =>
				Native.setTimelinePosition(value);
		}

		/// <summary>
		/// This event instance's current playback state.
		/// If the instance is invalid, this will default to PLAYBACK_STATE.STOPPED.
		/// </summary>
		public FMOD.Studio.PLAYBACK_STATE PlaybackState
		{
			get
			{
				Native.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
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
				Native.getPaused(out var paused);
				return paused;
			} // TODO: Do the same on core sounds?

			set =>
				Native.setPaused(value);
		}

		public EventInstance(EventDescription eventDescription, FMOD.Studio.EventInstance eventInstance)
		{
			Description = eventDescription;
			Native = eventInstance;

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
			Native.getParameterByName(name, out var value, out _);
			return value;
		}

		/// <summary>
		/// Retrieves a parameter's current value via its ID.
		/// This ignores modulation / automation applied to the parameter within Studio.
		/// </summary>
		public float GetParameterTargetValue(FMOD.Studio.PARAMETER_ID id)
		{
			Native.getParameterByID(id, out var value, out _);
			return value;
		}

		/// <summary>
		/// Retrieves a parameter's current value via its name (case sensitive).
		/// This takes into account modulation / automation applied to the parameter within Studio.
		/// </summary>
		public float GetParameterCurrentValue(string name)
		{
			Native.getParameterByName(name, out _, out var finalValue);
			return finalValue;
		}

		/// <summary>
		/// Retrieves a parameter's current value via its ID.
		/// This takes into account modulation / automation applied to the parameter within Studio.
		/// </summary>
		public float GetParameterCurrentValue(FMOD.Studio.PARAMETER_ID id)
		{
			Native.getParameterByID(id, out _, out var finalValue);
			return finalValue;
		}

		/// <summary>
		/// Sets a parameter's value via its name (case sensitive).
		/// Enable ignoreSeekSpeed to set the value instantly, ignoring the parameter's seek speed.
		/// </summary>
		public void SetParameterValue(string name, float value, bool ignoreSeekSpeed = false) =>
			Native.setParameterByName(name, value, ignoreSeekSpeed);

		/// <summary>
		/// Sets a parameter's value via its ID.
		/// Enable ignoreSeekSpeed to set the value instantly, ignoring the parameter's seek speed.
		/// </summary>
		public void SetParameterValue(FMOD.Studio.PARAMETER_ID id, float value, bool ignoreSeekSpeed = false) =>
			Native.setParameterByID(id, value, ignoreSeekSpeed);

		/// <summary>
		/// Sets multiple parameters' values via their IDs.
		/// Enable ignoreSeekSpeed to set the values instantly, ignoring the parameters' seek speeds.
		/// </summary>
		public void SetParameterValues(FMOD.Studio.PARAMETER_ID[] ids, float[] values, bool ignoreSeekSpeed = false) =>
			Native.setParametersByIDs(ids, values, ids.Length, ignoreSeekSpeed);

		/// <summary>
		/// Assigns a user callback for this specific event instance.
		/// </summary>
		public void SetCallback(FMOD.Studio.EVENT_CALLBACK callback, FMOD.Studio.EVENT_CALLBACK_TYPE callbackMask) =>
			Native.setCallback(callback, callbackMask);

		public void Start() =>
			Native.start();

		public void Stop(bool immediate = false) =>
			Native.stop(immediate ? FMOD.Studio.STOP_MODE.IMMEDIATE : FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

		/// <summary>
		/// Allows the timeline cursor to move past sustain points.
		/// </summary>
		public void KeyOff() =>
			Native.keyOff();
		
		public void Dispose() =>
			Native.release();
	}
}
