using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChaiFoxes.FMODAudio
{
	public struct ReverbProperties
	{
		public int Instance;
		public float Wet;
	}

	public interface IChannelControl
	{
		// General control functionality for Channels and ChannelGroups.
		void Stop();

		bool Paused { get; set; }
		float Volume { get; set; }
		bool VolumeRamp { get; set; }
		float Audibility { get; }
		float Pitch { get; set; }
		bool Mute { get; set; }
		float LowpassGain { get; set; }
		FMOD.MODE Mode { get; set; }
		bool IsPlaying { get; }

		ReverbProperties ReverbProperties { get; set; }

		FMOD.RESULT SetCallback(FMOD.CHANNELCONTROL_CALLBACK callback);

		// Panning and level adjustment.
		FMOD.RESULT SetPan(float pan);

		FMOD.RESULT SetMixLevelsOutput(float frontleft, float frontright, float center, float lfe, float surroundleft, float surroundright, float backleft, float backright);

		FMOD.RESULT SetMixLevelsInput(float[] levels, int numlevels);

		FMOD.RESULT SetMixMatrix(float[] matrix, int outchannels, int inchannels, int inchannel_hop);

		FMOD.RESULT SetMixMatrix(float[] matrix, out int outchannels, out int inchannels, int inchannel_hop);


		// Clock based functionality.
		FMOD.RESULT GetDSPClock(out ulong dspclock, out ulong parentclock);

		FMOD.RESULT SetDelay(ulong dspclock_start, ulong dspclock_end, bool stopchannels);

		FMOD.RESULT GetDelay(out ulong dspclock_start, out ulong dspclock_end);

		FMOD.RESULT GetDelay(out ulong dspclock_start, out ulong dspclock_end, out bool stopchannels);

		FMOD.RESULT AddFadePoint(ulong dspclock, float volume);

		FMOD.RESULT SetFadePointRamp(ulong dspclock, float volume);

		FMOD.RESULT RemoveFadePoints(ulong dspclock_start, ulong dspclock_end);

		FMOD.RESULT GetFadePoints(ref uint numpoints, ulong[] point_dspclock, float[] point_volume);

		// FMOD.DSP effects.
		FMOD.RESULT GetDSP(int index, out FMOD.DSP dsp);

		FMOD.RESULT AddDSP(int index, FMOD.DSP dsp);

		FMOD.RESULT RemoveDSP(FMOD.DSP dsp);

		FMOD.RESULT GetNumDSPs(out int numdsps);

		FMOD.RESULT SetDSPIndex(FMOD.DSP dsp, int index);

		FMOD.RESULT GetDSPIndex(FMOD.DSP dsp, out int index);


		// 3D functionality.
		FMOD.RESULT Set3DAttributes(ref Vector3 pos, ref Vector3 vel);

		FMOD.RESULT Get3DAttributes(out Vector3 pos, out Vector3 vel);

		FMOD.RESULT Set3DMinMaxDistance(float mindistance, float maxdistance);

		FMOD.RESULT Get3DMinMaxDistance(out float mindistance, out float maxdistance);

		FMOD.RESULT Set3DConeSettings(float insideconeangle, float outsideconeangle, float outsidevolume);

		FMOD.RESULT Get3DConeSettings(out float insideconeangle, out float outsideconeangle, out float outsidevolume);

		Vector3 ConeOrientation3D { get; set; }

		FMOD.RESULT Set3DCustomRolloff(ref Vector3 points, int numpoints);

		FMOD.RESULT Get3DCustomRolloff(out IntPtr points, out int numpoints);

		FMOD.RESULT Set3DOcclusion(float directocclusion, float reverbocclusion);

		FMOD.RESULT Get3DOcclusion(out float directocclusion, out float reverbocclusion);

		float Spread3D { get; set; }

		float Level3D { get; set; }

		float DopplerLevel3D { get; set; }

		FMOD.RESULT Set3DDistanceFilter(bool custom, float customLevel, float centerFreq);

		FMOD.RESULT Get3DDistanceFilter(out bool custom, out float customLevel, out float centerFreq);
	}
}
