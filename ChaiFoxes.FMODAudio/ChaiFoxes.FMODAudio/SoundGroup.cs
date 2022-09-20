using System;

namespace ChaiFoxes.FMODAudio
{
	public class SoundGroup : IDisposable
	{
		public readonly FMOD.SoundGroup Native;

		/// <summary>
		/// Contains all references to all sound grorup objects.
		/// </summary>
		internal static readonly PointerLinker<SoundGroup> _linker = new PointerLinker<SoundGroup>();


		public int MaxAudible
		{
			get
			{
				Native.getMaxAudible(out var audible);
				return audible;
			}
			set =>
				Native.setMaxAudible(value);
		}

		public FMOD.SOUNDGROUP_BEHAVIOR MaxAudibleBehavior
		{
			get
			{
				Native.getMaxAudibleBehavior(out var behavior);
				return behavior;
			}
			set =>
				Native.setMaxAudibleBehavior(value);
		}

		public float MuteFadeSpeed
		{
			get
			{
				Native.getMuteFadeSpeed(out var mute);
				return mute;
			}
			set =>
				Native.setMuteFadeSpeed(value);
		}

		/// <summary>
		/// Sound volume.
		/// 1 - Normal volume.
		/// 0 - Muted.
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

		public int SoundsCount
		{
			get
			{
				Native.getNumSounds(out var count);
				return count;
			}
		}

		public int PlayingSoundsCount
		{
			get
			{
				Native.getNumPlaying(out var count);
				return count;
			}
		}

		public SoundGroup(string name)
		{
			CoreSystem.Native.createSoundGroup(name, out Native);
		}

		public void Stop() =>
			Native.stop();

		public Sound GetSound(int index)
		{
			Native.getSound(index, out var sound);
			sound.getUserData(out var ptr);
			return Sound._linker.Get(ptr);
		}


		public void Dispose()
		{
			Native.getUserData(out var ptr);
			_linker.Remove(ptr);

			Native.release();
		}

	}
}
