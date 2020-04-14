using ChaiFoxes.FMODAudio.DigitalSoundProcessing;
using Microsoft.Xna.Framework;
using System;

namespace ChaiFoxes.FMODAudio
{
  interface IFmodSystem : IUserData, IDisposable
  {

    // Pre-init functions.
    FmodResult setOutput(OUTPUTTYPE output);

    FmodResult getOutput(out OUTPUTTYPE output);

    FmodResult getNumDrivers(out int numdrivers);

    FmodResult getDriverInfo(int id, out string name, int namelen, out Guid guid, out int systemrate, out SpeakerMode speakermode, out int speakermodechannels);

    FmodResult getDriverInfo(int id, out Guid guid, out int systemrate, out SpeakerMode speakermode, out int speakermodechannels);

    FmodResult setDriver(int driver);

    FmodResult getDriver(out int driver);

    FmodResult setSoftwareChannels(int numsoftwarechannels);

    FmodResult getSoftwareChannels(out int numsoftwarechannels);

    FmodResult setSoftwareFormat(int samplerate, SpeakerMode speakermode, int numrawspeakers);

    FmodResult getSoftwareFormat(out int samplerate, out SpeakerMode speakermode, out int numrawspeakers);

    FmodResult setDSPBufferSize(uint bufferlength, int numbuffers);

    FmodResult getDSPBufferSize(out uint bufferlength, out int numbuffers);

    FmodResult setFileSystem(FILE_OPEN_CALLBACK useropen, FILE_CLOSE_CALLBACK userclose, FILE_READ_CALLBACK userread, FILE_SEEK_CALLBACK userseek, FILE_ASYNCREAD_CALLBACK userasyncread, FILE_ASYNCCANCEL_CALLBACK userasynccancel, int blockalign);
 
    FmodResult attachFileSystem(FILE_OPEN_CALLBACK useropen, FILE_CLOSE_CALLBACK userclose, FILE_READ_CALLBACK userread, FILE_SEEK_CALLBACK userseek);

    FmodResult setAdvancedSettings(ref ADVANCEDSETTINGS settings);
 
    FmodResult getAdvancedSettings(ref ADVANCEDSETTINGS settings);
 
    FmodResult setCallback(SYSTEM_CALLBACK callback, SYSTEM_CALLBACK_TYPE callbackmask = SYSTEM_CALLBACK_TYPE.ALL);
 
    // Plug-in support.
    FmodResult setPluginPath(string path);

    FmodResult loadPlugin(string filename, out uint handle, uint priority = 0);

    FmodResult unloadPlugin(uint handle);

    FmodResult getNumNestedPlugins(uint handle, out int count);

    FmodResult getNestedPlugin(uint handle, int index, out uint nestedhandle);

    FmodResult getNumPlugins(PLUGINTYPE plugintype, out int numplugins);

    FmodResult getPluginHandle(PLUGINTYPE plugintype, int index, out uint handle);

    FmodResult getPluginInfo(uint handle, out PLUGINTYPE plugintype, out string name, int namelen, out uint version);

    FmodResult getPluginInfo(uint handle, out PLUGINTYPE plugintype, out uint version);

    FmodResult setOutputByPlugin(uint handle);

    FmodResult getOutputByPlugin(out uint handle);

    FmodResult createDSPByPlugin(uint handle, out IDsp dsp);

    FmodResult getDSPInfoByPlugin(uint handle, out IntPtr description);


    FmodResult registerDSP(ref IDspDescription description, out uint handle);


    // Init/Close.
    FmodResult init(int maxchannels, INITFLAGS flags, IntPtr extradriverdata);

    FmodResult close();


    // General post-init system functions.
    FmodResult update();

    FmodResult setSpeakerPosition(SPEAKER speaker, float x, float y, bool active);

    FmodResult getSpeakerPosition(SPEAKER speaker, out float x, out float y, out bool active);

    FmodResult setStreamBufferSize(uint filebuffersize, TimeUnit filebuffersizetype);

    FmodResult getStreamBufferSize(out uint filebuffersize, out TimeUnit filebuffersizetype);

    FmodResult set3DSettings(float dopplerscale, float distancefactor, float rolloffscale);

    FmodResult get3DSettings(out float dopplerscale, out float distancefactor, out float rolloffscale);

    FmodResult set3DNumListeners(int numlisteners);

    FmodResult get3DNumListeners(out int numlisteners);

    FmodResult set3DListenerAttributes(int listener, ref Vector3 pos, ref Vector3 vel, ref Vector3 forward, ref Vector3 up);

    FmodResult get3DListenerAttributes(int listener, out Vector3 pos, out Vector3 vel, out Vector3 forward, out Vector3 up);

    FmodResult set3DRolloffCallback(CB_3D_ROLLOFF_CALLBACK callback);

    FmodResult mixerSuspend();

    FmodResult mixerResume();

    FmodResult getDefaultMixMatrix(SpeakerMode sourcespeakermode, SpeakerMode targetspeakermode, float[] matrix, int matrixhop);

    FmodResult getSpeakerModeChannels(SpeakerMode mode, out int channels);


    // System information functions.
    FmodResult getVersion(out uint version);

    FmodResult getOutputHandle(out IntPtr handle);

    FmodResult getChannelsPlaying(out int channels);

    FmodResult getChannelsPlaying(out int channels, out int realchannels);

    FmodResult getCPUUsage(out float dsp, out float stream, out float geometry, out float update, out float total);

    FmodResult getFileUsage(out Int64 sampleBytesRead, out Int64 streamBytesRead, out Int64 otherBytesRead);


    // ISound/IDsp/Channel/FX creation and retrieval.
    FmodResult createSound(string name, SoundMode mode, ref CREATESOUNDEXINFO exinfo, out ISound sound);

    FmodResult createSound(byte[] data, SoundMode mode, ref CREATESOUNDEXINFO exinfo, out ISound sound);

    FmodResult createSound(IntPtr name_or_data, SoundMode mode, ref CREATESOUNDEXINFO exinfo, out ISound sound);

    FmodResult createSound(string name, SoundMode mode, out ISound sound);

    FmodResult createStream(string name, SoundMode mode, ref CREATESOUNDEXINFO exinfo, out ISound sound);

    FmodResult createStream(byte[] data, SoundMode mode, ref CREATESOUNDEXINFO exinfo, out ISound sound);

    FmodResult createStream(IntPtr name_or_data, SoundMode mode, ref CREATESOUNDEXINFO exinfo, out ISound sound);

    FmodResult createStream(string name, SoundMode mode, out ISound sound);

    FmodResult createDSP(ref IDspDescription description, out IDsp dsp);

    FmodResult createDSPByType(DspType type, out IDsp dsp);

    FmodResult createChannelGroup(string name, out IChannelGroup channelgroup);

    FmodResult createSoundGroup(string name, out SoundGroup soundgroup);

    FmodResult createReverb3D(out Reverb3D reverb);

    FmodResult playSound(ISound sound, IChannelGroup channelgroup, bool paused, out Channel channel);

    FmodResult playDSP(IDsp dsp, IChannelGroup channelgroup, bool paused, out Channel channel);

    FmodResult getChannel(int channelid, out Channel channel);

    FmodResult getMasterChannelGroup(out IChannelGroup channelgroup);

    FmodResult getMasterSoundGroup(out SoundGroup soundgroup);


    // Routing to ports.
    FmodResult attachChannelGroupToPort(uint portType, ulong portIndex, IChannelGroup channelgroup, bool passThru = false);

    FmodResult detachChannelGroupFromPort(IChannelGroup channelgroup);


    // Reverb api.
    FmodResult setReverbProperties(int instance, ref REVERB_PROPERTIES prop);

    FmodResult getReverbProperties(int instance, out REVERB_PROPERTIES prop);


    // System level IDsp functionality.
    FmodResult lockDSP();

    FmodResult unlockDSP();


    // Recording api
    FmodResult getRecordNumDrivers(out int numdrivers, out int numconnected);

    FmodResult getRecordDriverInfo(int id, out string name, int namelen, out Guid guid, out int systemrate, out SpeakerMode speakermode, out int speakermodechannels, out DRIVER_STATE state);

    FmodResult getRecordDriverInfo(int id, out Guid guid, out int systemrate, out SpeakerMode speakermode, out int speakermodechannels, out DRIVER_STATE state);

    FmodResult getRecordPosition(int id, out uint position);

    FmodResult recordStart(int id, ISound sound, bool loop);

    FmodResult recordStop(int id);

    FmodResult isRecording(int id, out bool recording);


    // Geometry api
    FmodResult createGeometry(int maxpolygons, int maxvertices, out IGeometry geometry);

    FmodResult setGeometrySettings(float maxworldsize);

    FmodResult getGeometrySettings(out float maxworldsize);

    FmodResult loadGeometry(IntPtr data, int datasize, out IGeometry geometry);

    FmodResult getGeometryOcclusion(ref Vector3 listener, ref Vector3 source, out float direct, out float reverb);


    // Network functions
    FmodResult setNetworkProxy(string proxy);

    FmodResult getNetworkProxy(out string proxy, int proxylen);

    FmodResult setNetworkTimeout(int timeout);

    FmodResult getNetworkTimeout(out int timeout);

  }
}
