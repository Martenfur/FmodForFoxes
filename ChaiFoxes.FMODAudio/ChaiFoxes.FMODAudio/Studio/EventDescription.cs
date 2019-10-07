using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;

// DO NOT include FMOD namespace in ANY of your classes.
// Use FMOD.SomeClass instead.
// FMOD classes seriously interfere with System namespace.

namespace ChaiFoxes.FMODAudio.Studio
{
	/// <summary>
	/// Event description wrapper. Static reference to an event, which is then used to create event instances.
	/// Can be retrieved from a bank by querying all events in a bank, or through a specific path / GUID.
	/// </summary>
	public class EventDescription
	{
		/// <summary>
		/// FMOD event description using the default wrapper. Use this if you need full FMOD functionality.
		/// </summary>
		public FMOD.Studio.EventDescription Description => _FMODEventDescription;
		protected FMOD.Studio.EventDescription _FMODEventDescription;

		/// <summary>
		/// Number of instances of the event currently in existence.
		/// </summary>
		public int InstanceCount
		{
			get
			{
				_FMODEventDescription.getInstanceCount(out int instanceCount);
				return instanceCount;
			}
		}

		/// <summary>
		/// All instances of the event currently in existence.
		/// </summary>
		public EventInstance[] Instances
		{
			get
			{
				_FMODEventDescription.getInstanceList(out FMOD.Studio.EventInstance[] instanceArray);
				EventInstance[] returnArray = new EventInstance[instanceArray.Length];
				for (int i = 0; i < instanceArray.Length; i++)
				{
					returnArray[i] = new EventInstance(this, instanceArray[i]);
				}
				return returnArray;
			}
		}

		/// <summary>
		/// Sound pitch. Affects speed too.
		/// 1 - Normal pitch.
		/// More than 1 - Higher pitch.
		/// Less than 1 - Lower pitch.
		/// </summary>
		public float Pitch = 1;

		/// <summary>
		/// Sound volume.
		/// 1 - Normal volume.
		/// 0 - Muted.
		/// </summary>
		public float Volume = 1;

		/// <summary>
		/// Event's default channel group.
		/// </summary>
		public FMOD.ChannelGroup ChannelGroup;

		/// <summary>
		/// Returns true if the event is 3D.
		/// </summary>
		public bool Is3D
		{
			get
			{
				_FMODEventDescription.is3D(out bool is3D);
				return is3D;
			}
		}

		/// <summary>
		/// Event's 3D Attributes.
		/// </summary>
		public Attributes3D Attributes = new Attributes3D
		{
			position = Vector3.Zero,
			velocity = Vector3.Zero,
			forwardVector = Vector3.UnitY,
			upVector = Vector3.UnitZ
		};

		/// <summary>
		/// Returns true if the event is 3D.
		/// </summary>
		public bool IsOneshot
		{
			get
			{
				_FMODEventDescription.isOneshot(out bool isOneshot);
				return isOneshot;
			}
		}

		/// <summary>
		/// Returns true if the event is a snapshot.
		/// </summary>
		public bool IsSnapshot
		{
			get
			{
				_FMODEventDescription.isSnapshot(out bool isSnapshot);
				return isSnapshot;
			}
		}

		/// <summary>
		/// Returns true if the event has any streamed sound.
		/// </summary>
		public bool IsStream
		{
			get
			{
				_FMODEventDescription.isStream(out bool isStream);
				return isStream;
			}
		}

		/// <summary>
		/// Number of parameters in the event.
		/// </summary>
		public int ParameterCount
		{
			get
			{
				_FMODEventDescription.getParameterDescriptionCount(out int count);
				return count;
			}
		}

		/// <summary>
		/// The event description's internal path, i.e. "event:/UI/Cancel".
		/// </summary>
		public string Path
		{
			get
			{
				_FMODEventDescription.getPath(out string path);
				return path;
			}
		}

		/// <summary>
		/// The event description's GUID.
		/// </summary>
		public Guid ID
		{
			get
			{
				_FMODEventDescription.getID(out Guid id);
				return id;
			}
		}

		/// <summary>
		/// The event description's arbitrary user data.
		/// </summary>
		public IntPtr UserData
		{
			set =>
				 _FMODEventDescription.setUserData(value);

			get
			{
				_FMODEventDescription.getUserData(out IntPtr userData);
				return userData;
			}
		}

		public EventDescription(FMOD.Studio.EventDescription eventDescription)
		{
			_FMODEventDescription = eventDescription;
		}

		/// <summary>
		/// Creates an instance of the event.
		/// If the sample data hasn't already been loaded, this will automatically do it.
		/// Loading sample data through this method requires a little time - make sure you don't want the event played immediately!
		/// </summary>
		public EventInstance CreateInstance()
		{
			_FMODEventDescription.createInstance(out FMOD.Studio.EventInstance eventInstance);
			return new EventInstance(this, eventInstance);
		}

		/// <summary>
		/// Immediately stops and releases all instances of this event.
		/// </summary>
		public void ReleaseAllInstances() =>
			_FMODEventDescription.releaseAllInstances();

		/// <summary>
		/// Gets an event parameter description by its name.
		/// </summary>
		public FMOD.Studio.PARAMETER_DESCRIPTION GetParameterDescription(string name)
		{
			_FMODEventDescription.getParameterDescriptionByName(name, out FMOD.Studio.PARAMETER_DESCRIPTION parameter);
			return parameter;
		}

		/// <summary>
		/// Gets an event parameter description by its index.
		/// </summary>
		public FMOD.Studio.PARAMETER_DESCRIPTION GetParameterDescription(int index)
		{
			_FMODEventDescription.getParameterDescriptionByIndex(index, out FMOD.Studio.PARAMETER_DESCRIPTION parameter);
			return parameter;
		}

		/// <summary>
		/// Gets an event parameter description by its ID.
		/// </summary>
		public FMOD.Studio.PARAMETER_DESCRIPTION GetParameterDescription(FMOD.Studio.PARAMETER_ID id)
		{
			_FMODEventDescription.getParameterDescriptionByID(id, out FMOD.Studio.PARAMETER_DESCRIPTION parameter);
			return parameter;
		}

		/// <summary>
		/// Assigns a user callback for every subsequent instance of this event.
		/// </summary>
		public void SetCallback(FMOD.Studio.EVENT_CALLBACK callback, FMOD.Studio.EVENT_CALLBACK_TYPE callbackMask) =>
			_FMODEventDescription.setCallback(callback, callbackMask);

		/// <summary>
		/// Loads all non-streaming sounds for the event.
		/// </summary>
		public void LoadSampleData() => _FMODEventDescription.loadSampleData();

		/// <summary>
		/// Unloads all non-streaming sounds for the event.
		/// </summary>
		public void UnloadSampleData() => _FMODEventDescription.unloadSampleData();
	}
}
