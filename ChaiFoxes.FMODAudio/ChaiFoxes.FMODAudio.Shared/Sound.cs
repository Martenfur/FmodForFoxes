using Microsoft.Xna.Framework;
using System;
using System.Runtime.InteropServices;

// DO NOT include FMOD namespace in ANY of your classes.
// Use FMOD.SomeClass instead.
// FMOD classes seriously interfere with System namespace.

namespace ChaiFoxes.FMODAudio
{
	/// <summary>
	/// Sound class. Can play sound with given attributes.
	/// </summary>
	public class Sound : I3DControl, IDisposable
	{
		public readonly FMOD.Sound Native;

		/// <summary>
		/// Contains all references to all sound objects.
		/// </summary>
		internal static readonly PointerLinker<Sound> _linker = new PointerLinker<Sound>();

		/// <summary>
		/// Tells if sound is looping.
		/// </summary>
		public bool Looping
		{
			get => (Loops == -1);
			set
			{
				if (value)
				{
					Loops = -1;
				}
				else
				{
					Loops = 0;
				}
			}
		}

		/// <summary>
		/// Amount of loops. 
		/// > 0 - Specific count.
		/// 0 - No loops.
		/// -1 - Infinite loops.
		/// </summary>
		public int Loops = 0;

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
		/// Low pass filter. Makes sound muffled.
		/// 1 - No filtering.
		/// 0 - Full filtering.
		/// </summary>
		public float LowPass = 1;

		/// <summary>
		/// Sound mode.
		/// </summary>
		public FMOD.MODE Mode
		{
			get
			{
				Native.getMode(out var mode);
				return mode;
			}
			set
			{
				Native.setMode(value);
			}
		}

		/// <summary>
		/// Sound's default channel group.
		/// </summary>
		public ChannelGroup ChannelGroup;

		public SoundGroup SoundGroup
		{
			get
			{
				Native.getSoundGroup(out var sound);
				sound.getUserData(out var ptr);
				return SoundGroup._linker.Get(ptr);
			}
			set
			{
				Native.setSoundGroup(value.Native);
			}
		}

		/// <summary>
		/// If true, allows sound to be positioned in 3D space.
		/// </summary>
		public bool Is3D = false;

		/// <summary>
		/// Sound's position in 3D space. Can be used only if 3D positioning is enabled.
		/// </summary>
		public Vector3 Position3D { get; set; } = Vector3.Zero;


		/// <summary>
		/// Sound's velocity in 3D space. Can be used only if 3D positioning is enabled.
		/// </summary>
		public Vector3 Velocity3D { get; set; } = Vector3.Zero;

		/// <summary>
		/// Distance from the source where attenuation begins.
		/// </summary>
		public float MinDistance3D
		{
			get
			{
				Native.get3DMinMaxDistance(out var minDistance, out var maxDistance);
				return minDistance;
			}
			set =>
				Native.set3DMinMaxDistance(value, MaxDistance3D);
		}

		/// <summary>
		/// Distance from the source where attenuation ends.
		/// </summary>
		public float MaxDistance3D
		{
			get
			{
				Native.get3DMinMaxDistance(out var minDistance, out var maxDistance);
				return maxDistance;
			}
			set =>
				Native.set3DMinMaxDistance(MinDistance3D, value);
		}

		/// <summary>
		/// Sound length in specified time units
		/// </summary>
		public uint Length
		{
			get
			{
				Native.getLength(out var length, LengthTimeunit);
				return length;
			}
		}

		public int LoopCount
		{
			get
			{
				Native.getLoopCount(out var loopCount);
				return loopCount;
			}
			set
			{
				Native.setLoopCount(value);
			}
		}

		public float DefaultFrequency
		{
			get
			{
				Native.getDefaults(out var frequency, out var priority);
				return frequency;
			}
			set =>
				Native.setDefaults(value, DefaultPriority);
		}

		public int DefaultPriority
		{
			get
			{
				Native.getDefaults(out var frequency, out var priority);
				return priority;
			}
			set =>
				Native.setDefaults(DefaultFrequency, value);
		}

		public ConeSettings3D ConeSettings3D
		{
			get
			{
				var cone = new ConeSettings3D();
				Native.get3DConeSettings(out cone.InsideConeAngle, out cone.OutsideVolume, out cone.OutsideVolume);
				return cone;
			}
			set =>
				Native.set3DConeSettings(value.InsideConeAngle, value.OutsideConeAngle, value.OutsideVolume);
		}


		/// <summary>
		/// Sound buffer. Used for streamed sounds, which point to this memory.
		/// In other words, we need to just reference it somewhere to prevent
		/// garbage collector from collecting it.
		/// This memory is also pinned, so GC won't move it anywhere.
		/// 
		/// If any unexpected crashes emerge, this is the first suspect.
		/// </summary>
		private byte[] _buffer;

		/// <summary>
		/// Buffer's handle.
		/// </summary>
		private GCHandle _bufferHandle;


		public Sound(FMOD.Sound sound, byte[] buffer, GCHandle bufferHandle)
			: this(sound)
		{
			_buffer = buffer;
			_bufferHandle = bufferHandle;
		}

		public Sound(FMOD.Sound sound)
		{
			Native = sound;

			Native.setUserData(_linker.Add(this));
		}

		public Channel Play(bool paused = false) =>
			Play(ChannelGroup, paused);

		public Channel Play(ChannelGroup group, bool paused = false)
		{
			CoreSystem.Native.playSound(Native, group.Native, paused, out FMOD.Channel fmodChannel);
			return new Channel(this, fmodChannel);
		}

		/// <summary>
		/// Unloads the sound and frees its handles.
		/// </summary>
		public void Dispose()
		{
			Native.getUserData(out var ptr);
			_linker.Remove(ptr);

			Native.release();
			if (_buffer != null)
			{
				_bufferHandle.Free();
			}
		}

		public FMOD.TIMEUNIT LengthTimeunit = FMOD.TIMEUNIT.MS;
	}
}
