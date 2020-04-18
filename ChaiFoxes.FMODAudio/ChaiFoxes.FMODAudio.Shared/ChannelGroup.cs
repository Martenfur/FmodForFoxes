using ChaiFoxes.FMODAudio.DigitalSoundProcessing;
using System;

namespace ChaiFoxes.FMODAudio
{
	public class ChannelGroup : IDisposable, IChannelControl
	{

		// Nested channel groups.
		FMOD.RESULT AddGroup(ChannelGroup group, bool propagatedspclock = true);

		FMOD.RESULT AddGroup(ChannelGroup group, bool propagatedspclock, out IDspConnection connection);

		FMOD.RESULT getNumGroups(out int numgroups);

		FMOD.RESULT getGroup(int index, out ChannelGroup group);

		FMOD.RESULT getParentGroup(out ChannelGroup group);


		// Information only functions.
		FMOD.RESULT getName(out string name, int namelen);

		FMOD.RESULT getNumChannels(out int numchannels);

		SoundChannel GetChannel(int index, out SoundChannel channel)
		{ 
		
		}

	}

}
