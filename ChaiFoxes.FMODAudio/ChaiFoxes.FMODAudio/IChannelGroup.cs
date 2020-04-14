using ChaiFoxes.FMODAudio.DigitalSoundProcessing;
using System;

namespace ChaiFoxes.FMODAudio
{
  public interface IChannelGroup : IUserData, IDisposable, IChannelControl
  {

    // Nested channel groups.
    FmodResult AddGroup(IChannelGroup group, bool propagatedspclock = true);
    
    FmodResult AddGroup(IChannelGroup group, bool propagatedspclock, out IDspConnection connection);
    
    FmodResult getNumGroups(out int numgroups);
    
    FmodResult getGroup(int index, out IChannelGroup group);
    
    FmodResult getParentGroup(out IChannelGroup group);
    

    // Information only functions.
    FmodResult getName(out string name, int namelen);

    FmodResult getNumChannels(out int numchannels);

    FmodResult getChannel(int index, out IChannel channel);

  }

}
