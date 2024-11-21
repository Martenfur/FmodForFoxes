using Microsoft.Xna.Framework;
using System;

// DO NOT include FMOD namespace in ANY of your classes.
// Use FMOD.SomeClass instead.
// FMOD classes seriously interfere with System namespace.

namespace FmodForFoxes.Studio
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
		public readonly FMOD.Studio.EventDescription Native;

		/// <summary>
		/// Number of instances of the event currently in existence.
		/// </summary>
		public int InstanceCount
		{
			get
			{
				Native.getInstanceCount(out var instanceCount);
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
				Native.getInstanceList(out FMOD.Studio.EventInstance[] instanceArray);
				var returnArray = new EventInstance[instanceArray.Length];
				for (var i = 0; i < instanceArray.Length; i += 1)
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
		/// Returns true if the event is 3D.
		/// </summary>
		public bool Is3D
		{
			get
			{
				Native.is3D(out var is3D);
				return is3D;
			}
		}

		/// <summary>
		/// Event's 3D Attributes.
		/// </summary>
		public Attributes3D Attributes = new Attributes3D
		{
			Position = Vector3.Zero,
			Velocity = Vector3.Zero,
			ForwardVector = Vector3.UnitY,
			UpVector = Vector3.UnitZ
		};

		public bool IsOneshot
		{
			get
			{
				Native.isOneshot(out var isOneshot);
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
				Native.isSnapshot(out var isSnapshot);
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
				Native.isStream(out var isStream);
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
				Native.getParameterDescriptionCount(out var count);
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
				Native.getPath(out string path);
				return path;
			}
		}

		/// <summary>
		/// The event description's GUID.
		/// </summary>
		public FMOD.GUID ID
		{
			get
			{
				Native.getID(out var id);
				return id;
			}
		}

		public EventDescription(FMOD.Studio.EventDescription eventDescription)
		{
			Native = eventDescription;
		}

		/// <summary>
		/// Creates an instance of the event.
		/// If the sample data hasn't already been loaded, this will automatically do it.
		/// Loading sample data through this method requires a little time - make sure you don't want the event played immediately!
		/// </summary>
		public EventInstance CreateInstance()
		{
			Native.createInstance(out var eventInstance);
			return new EventInstance(this, eventInstance);
		}

		/// <summary>
		/// Immediately stops and releases all instances of this event.
		/// </summary>
		public void ReleaseAllInstances() =>
			Native.releaseAllInstances();

		/// <summary>
		/// Gets an event parameter description by its name.
		/// </summary>
		public FMOD.Studio.PARAMETER_DESCRIPTION GetParameterDescription(string name)
		{
			Native.getParameterDescriptionByName(name, out var parameter);
			return parameter;
		}

		/// <summary>
		/// Gets an event parameter description by its index.
		/// </summary>
		public FMOD.Studio.PARAMETER_DESCRIPTION GetParameterDescription(int index)
		{
			Native.getParameterDescriptionByIndex(index, out var parameter);
			return parameter;
		}

		/// <summary>
		/// Gets an event parameter description by its ID.
		/// </summary>
		public FMOD.Studio.PARAMETER_DESCRIPTION GetParameterDescription(FMOD.Studio.PARAMETER_ID id)
		{
			Native.getParameterDescriptionByID(id, out var parameter);
			return parameter;
		}

		/// <summary>
		/// Assigns a user callback for every subsequent instance of this event.
		/// </summary>
		public void SetCallback(FMOD.Studio.EVENT_CALLBACK callback, FMOD.Studio.EVENT_CALLBACK_TYPE callbackMask) =>
			Native.setCallback(callback, callbackMask);

		/// <summary>
		/// Loads all non-streaming sounds for the event.
		/// </summary>
		public void LoadSampleData() => 
			Native.loadSampleData();

		/// <summary>
		/// Unloads all non-streaming sounds for the event.
		/// </summary>
		public void UnloadSampleData() => 
			Native.unloadSampleData();
	}
}
