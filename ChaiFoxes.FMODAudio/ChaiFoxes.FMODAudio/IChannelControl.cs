using ChaiFoxes.FMODAudio.DigitalSoundProcessing;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChaiFoxes.FMODAudio
{
	public interface IChannelControl
	{
    FmodResult getSystemObject(out System system);


    // General control functionality for Channels and ChannelGroups.
    FmodResult Stop();


    bool Paused { get; set; }
    float Volume { get; set; }
    bool VolumeRamp { get; set; }
    float Audibility { get; }
    float Pitch { get; set; }
    bool Mute { get; set; }
    float LowpassGain { get; set; }
    SoundMode Mode { get; set; }
    bool IsPlaying { get; }

    FmodResult SetReverbProperties(int instance, float wet);

    FmodResult GetReverbProperties(int instance, out float wet);

    FmodResult SetCallback(ChannelControlCallback callback);

    // Panning and level adjustment.
    FmodResult SetPan(float pan);

    FmodResult SetMixLevelsOutput(float frontleft, float frontright, float center, float lfe, float surroundleft, float surroundright, float backleft, float backright);

    FmodResult SetMixLevelsInput(float[] levels, int numlevels);

    FmodResult SetMixMatrix(float[] matrix, int outchannels, int inchannels, int inchannel_hop);

    FmodResult SetMixMatrix(float[] matrix, out int outchannels, out int inchannels, int inchannel_hop);


    // Clock based functionality.
    FmodResult GetDSPClock(out ulong dspclock, out ulong parentclock);

    FmodResult SetDelay(ulong dspclock_start, ulong dspclock_end, bool stopchannels);

    FmodResult GetDelay(out ulong dspclock_start, out ulong dspclock_end);

    FmodResult GetDelay(out ulong dspclock_start, out ulong dspclock_end, out bool stopchannels);

    FmodResult AddFadePoint(ulong dspclock, float volume);

    FmodResult SetFadePointRamp(ulong dspclock, float volume);

    FmodResult RemoveFadePoints(ulong dspclock_start, ulong dspclock_end);

    FmodResult GetFadePoints(ref uint numpoints, ulong[] point_dspclock, float[] point_volume);

    // IDsp effects.
    FmodResult GetDSP(int index, out IDsp dsp);

    FmodResult AddDSP(int index, IDsp dsp);

    FmodResult RemoveDSP(IDsp dsp);

    FmodResult GetNumDSPs(out int numdsps);

    FmodResult SetDSPIndex(IDsp dsp, int index);

    FmodResult GetDSPIndex(IDsp dsp, out int index);


    // 3D functionality.
    FmodResult Set3DAttributes(ref Vector3 pos, ref Vector3 vel);

    FmodResult Get3DAttributes(out Vector3 pos, out Vector3 vel);

    FmodResult Set3DMinMaxDistance(float mindistance, float maxdistance);

    FmodResult Get3DMinMaxDistance(out float mindistance, out float maxdistance);

    FmodResult Set3DConeSettings(float insideconeangle, float outsideconeangle, float outsidevolume);

    FmodResult Get3DConeSettings(out float insideconeangle, out float outsideconeangle, out float outsidevolume);

    Vector3 ConeOrientation3D { get; set; }

    FmodResult Set3DCustomRolloff(ref Vector3 points, int numpoints);

    FmodResult Get3DCustomRolloff(out IntPtr points, out int numpoints);

    FmodResult Set3DOcclusion(float directocclusion, float reverbocclusion);

    FmodResult Get3DOcclusion(out float directocclusion, out float reverbocclusion);

    float Spread3D { get; set; }

    float Level3D { get; set; }

    float DopplerLevel3D { get; set; }

    FmodResult Set3DDistanceFilter(bool custom, float customLevel, float centerFreq);

    FmodResult Get3DDistanceFilter(out bool custom, out float customLevel, out float centerFreq);
  }
}
