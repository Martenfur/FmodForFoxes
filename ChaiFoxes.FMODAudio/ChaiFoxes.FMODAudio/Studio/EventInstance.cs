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
        /// 0 - Muted.<para/>
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
        /// 0 - Muted.<para/>
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
        /// Event's position in 3D space. Will only have an effect if the event is 3D.
        /// </summary>
        public Vector3 Position3D
        {
            get
            {
                GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector);
                return position;
            }
            set
            {
                GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector);
                SetAttributes(value, velocity, forwardVector, upVector);
            }
        }

        /// <summary>
        /// Event's velocity in 3D space. Will only have an effect if the event is 3D.
        /// </summary>
        public Vector3 Velocity3D
        {
            get
            {
                GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector);
                return velocity;
            }
            set
            {
                GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector);
                SetAttributes(position, value, forwardVector, upVector);
            }
        }

        /// <summary>
        /// Forwards orientation, must be of unit length (1.0) and perpendicular to up.
		/// UnitY by default. Will only have an effect if the event is 3D.
        /// </summary>
        public Vector3 OrientationForward3D
        {
            get
            {
                GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector);
                return forwardVector;
            }
            set
            {
                GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector);
                SetAttributes(position, velocity, value, upVector);
            }
        }

        /// <summary>
        /// Upwards orientation, must be of unit length (1.0) and perpendicular to forward.
		/// UnitZ by default. Will only have an effect if the event is 3D.
        /// </summary>
        public Vector3 OrientationUpward3D
        {
            get
            {
                GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector);
                return upVector;
            }
            set
            {
                GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector);
                SetAttributes(position, velocity, forwardVector, value);
            }
        }
        /// <summary>
		/// Gets all 3D attributes at once.
		/// </summary>
		public void GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector)
        {
            _FMODEventInstance.get3DAttributes(out FMOD.ATTRIBUTES_3D attributes);

            position = attributes.position.ToVector3();
            velocity = attributes.velocity.ToVector3();
            forwardVector = attributes.forward.ToVector3();
            upVector = attributes.up.ToVector3();
        }

        /// <summary>
        /// Sets all 3D attributes at once.
        /// </summary>
        public void SetAttributes(Vector3 position, Vector3 velocity, Vector3 forwardVector, Vector3 upVector)
        {
            var attributes = new FMOD.ATTRIBUTES_3D();

            attributes.position = position.ToFmodVector();
            attributes.velocity = velocity.ToFmodVector();
            attributes.forward = forwardVector.ToFmodVector();
            attributes.up = upVector.ToFmodVector();

            _FMODEventInstance.set3DAttributes(attributes);
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
            SetAttributes(
                eventDescription.Position3D,
                eventDescription.Velocity3D,
                eventDescription.OrientationForward3D,
                eventDescription.OrientationUpward3D
            );
        }

        /// <summary>
        /// Retrieves a parameter's current value via its name (case sensitive).<para/>
        /// This ignores modulation / automation applied to the parameter within Studio.
        /// </summary>
        public float GetParameterTargetValue(string name)
        {
            _FMODEventInstance.getParameterByName(name, out float value, out _);
            return value;
        }

        /// <summary>
        /// Retrieves a parameter's current value via its ID.<para/>
        /// This ignores modulation / automation applied to the parameter within Studio.
        /// </summary>
        public float GetParameterTargetValue(FMOD.Studio.PARAMETER_ID id)
        {
            _FMODEventInstance.getParameterByID(id, out float value, out _);
            return value;
        }

        /// <summary>
        /// Retrieves a parameter's current value via its name (case sensitive).<para/>
        /// This takes into account modulation / automation applied to the parameter within Studio.
        /// </summary>
        public float GetParameterCurrentValue(string name)
        {
            _FMODEventInstance.getParameterByName(name, out _, out float finalValue);
            return finalValue;
        }

        /// <summary>
        /// Retrieves a parameter's current value via its ID.<para/>
        /// This takes into account modulation / automation applied to the parameter within Studio.
        /// </summary>
        public float GetParameterCurrentValue(FMOD.Studio.PARAMETER_ID id)
        {
            _FMODEventInstance.getParameterByID(id, out _, out float finalValue);
            return finalValue;
        }

        /// <summary>
        /// Sets a parameter's value via its name (case sensitive).<para/>
        /// Enable ignoreSeekSpeed to set the value instantly, ignoring the parameter's seek speed.
        /// </summary>
        public void SetParameterValue(string name, float value, bool ignoreSeekSpeed = false)
        {
            _FMODEventInstance.setParameterByName(name, value, ignoreSeekSpeed);
        }

        /// <summary>
        /// Sets a parameter's value via its ID.<para/>
        /// Enable ignoreSeekSpeed to set the value instantly, ignoring the parameter's seek speed.
        /// </summary>
        public void SetParameterValue(FMOD.Studio.PARAMETER_ID id, float value, bool ignoreSeekSpeed = false)
        {
            _FMODEventInstance.setParameterByID(id, value, ignoreSeekSpeed);
        }

        /// <summary>
        /// Sets multiple parameters' values via their IDs.<para/>
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