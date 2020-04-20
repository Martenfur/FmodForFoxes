using Microsoft.Xna.Framework;
using System;

namespace ChaiFoxes.FMODAudio
{
	public class ChannelGroup : IDisposable, IChannelControl
	{
		public readonly FMOD.ChannelGroup Native;

		private readonly PointerLinker<ChannelGroup> _linker = new PointerLinker<ChannelGroup>();

		/// <summary>
		/// Sound pitch. Affects speed too.
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

		/// <summary>
		/// Low pass filter. Makes sound muffled.
		/// 1 - No filtering.
		/// 0 - Full filtering.
		/// </summary>
		public float LowPass
		{
			get
			{
				Native.getLowPassGain(out var lowPassGain);
				return lowPassGain;
			}
			set =>
				Native.setLowPassGain(value);
		}

		/// <summary>
		/// Sound mode. Mainly used for 3D sound.
		/// </summary>
		public FMOD.MODE Mode
		{
			get
			{
				Native.getMode(out FMOD.MODE mode);
				return mode;
			}
			set =>
				Native.setMode(value);
		}


		/// <summary>
		/// If true, allows sound to be positioned in 3D space.
		/// </summary>
		public bool Is3D
		{
			get =>
				(Mode & FMOD.MODE._3D) != 0;
			set
			{
				if (value)
				{
					Mode = FMOD.MODE._3D;
				}
				else
				{
					Mode = FMOD.MODE._2D;
				}
			}
		}

		/// <summary>
		/// Sound's position in 3D space. Can be used only if 3D positioning is enabled.
		/// </summary>
		public Vector3 Position3D
		{
			get
			{
				Native.get3DAttributes(out FMOD.VECTOR pos, out FMOD.VECTOR vel);
				return pos.ToVector3();
			}
			set
			{
				var fmodPos = value.ToFmodVector();
				var fmodVel = Velocity3D.ToFmodVector();
				Native.set3DAttributes(ref fmodPos, ref fmodVel);
			}
		}

		/// <summary>
		/// Sound's velocity in 3D space. Can be used only if 3D positioning is enabled.
		/// </summary>
		public Vector3 Velocity3D
		{
			get
			{
				Native.get3DAttributes(out FMOD.VECTOR pos, out FMOD.VECTOR vel);
				return vel.ToVector3();
			}
			set
			{
				var fmodPos = Position3D.ToFmodVector();
				var fmodVel = value.ToFmodVector();
				Native.set3DAttributes(ref fmodPos, ref fmodVel);
			}
		}

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
		/// Tells if sound is playing.
		/// </summary>
		public bool IsPlaying
		{
			get
			{
				Native.isPlaying(out var isPlaying);
				return isPlaying;
			}
		}

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

		public bool VolumeRamp
		{
			get
			{
				Native.getVolumeRamp(out var volumeRamp);
				return volumeRamp;
			}
			set =>
				Native.setVolumeRamp(value);
		}

		public float Audibility
		{
			get
			{
				Native.getAudibility(out var audibility);
				return audibility;
			}
		}

		public bool Mute
		{
			get
			{
				Native.getMute(out var mute);
				return mute;
			}
			set =>
				Native.setMute(value);
		}

		public float LowpassGain
		{
			get
			{
				Native.getLowPassGain(out var lowpassGain);
				return lowpassGain;
			}
			set =>
				Native.setLowPassGain(value);
		}

		public Vector3 ConeOrientation3D
		{
			get
			{
				Native.get3DConeOrientation(out var orientation);
				return orientation.ToVector3();
			}
			set
			{
				var v = value.ToFmodVector();
				Native.set3DConeOrientation(ref v);
			}
		}

		public Occlusion3D Occlusion3D
		{
			get
			{
				var occlusion = new Occlusion3D();
				Native.get3DOcclusion(out occlusion.DirectOcclusion, out occlusion.ReverbOcclusion);
				return occlusion;
			}
			set =>
				Native.set3DOcclusion(value.DirectOcclusion, value.ReverbOcclusion);
		}

		public float Spread3D
		{
			get
			{
				Native.get3DSpread(out var spread);
				return spread;
			}
			set =>
				Native.set3DSpread(value);
		}

		public float Level3D
		{
			get
			{
				Native.get3DLevel(out var level);
				return level;
			}
			set =>
				Native.set3DLevel(value);
		}

		public float DopplerLevel3D
		{
			get
			{
				Native.get3DDopplerLevel(out var doppler);
				return doppler;
			}
			set =>
				Native.set3DDopplerLevel(value);
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

		public DistanceFilter3D DistanceFilter3D
		{
			get
			{
				var filter = new DistanceFilter3D();
				Native.get3DDistanceFilter(out filter.Custom, out filter.CustomLevel, out filter.CenterFreqency);
				return filter;
			}
			set =>
				Native.set3DDistanceFilter(value.Custom, value.CustomLevel, value.CenterFreqency);
		}



		// Nested channel groups.
		
		public FMOD.DSPConnection AddNestedGroup(
			ChannelGroup group, 
			bool propagateDspclock
		)
		{
			Native.addGroup(group.Native, propagateDspclock, out var connection);
			return connection;
		}

		public int NestedGroupsCount
		{ 
			get
			{ 
				Native.getNumGroups(out var count);
				return count;
			}
		}

		public ChannelGroup GetNestedGroup(int index)
		{
			Native.getGroup(index, out var group);
			group.getUserData(out var ptr);
			return _linker.Get(ptr);
		}

		public ChannelGroup getParentGroup()
		{
			Native.getParentGroup(out var sound);
			sound.getUserData(out var ptr);
			return _linker.Get(ptr);
		}


		public int ChannelsCount
		{ 
			get
			{ 
				Native.getNumChannels(out var count);
				return count;
			}
		}

		public Channel GetChannel(int index)
		{
			Native.getChannel(index, out var nativeChannel);
			return new Channel(nativeChannel);
		}

		public ChannelGroup(string name)
		{
			CoreSystem.Native.createChannelGroup(name, out Native);

			Native.setUserData(_linker.Add(this));
		}

		public void Stop() =>
			Native.stop();

		public void Dispose()
		{
			Native.getUserData(out var ptr);
			_linker.Remove(ptr);

			Native.release();
		}
	}

}
