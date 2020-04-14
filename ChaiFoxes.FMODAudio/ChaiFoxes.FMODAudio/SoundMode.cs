using System;

namespace ChaiFoxes.FMODAudio
{
	[Flags]
	public enum SoundMode : uint
	{
		/// <summary>
		/// Default for all modes listed below. FMOD_LOOP_OFF, FMOD_2D, FMOD_3D_WORLDRELATIVE, FMOD_3D_INVERSEROLLOFF 
		/// </summary>
		Default = 0x00000000,
		/// <summary>
		/// For non looping sounds. (default).  Overrides FMOD_LOOP_NORMAL / FMOD_LOOP_BIDI. 
		/// </summary>
		LoopOff = 0x00000001,
		/// <summary>
		/// For forward looping sounds. 
		/// </summary>
		LoopNormal = 0x00000002,
		/// <summary>
		/// For bidirectional looping sounds. (only works on software mixed static sounds). 
		/// </summary>
		LoopBidi = 0x00000004,
		/// <summary>
		/// Ignores any 3d processing. (default). 
		/// </summary>
		Mode2D = 0x00000008,
		/// <summary>
		/// Makes the sound positionable in 3D.  Overrides FMOD_2D. 
		/// </summary>
		Mode3D = 0x00000010,
		/// <summary>
		/// Decompress at runtime, streaming from the source provided (standard stream).  Overrides FMOD_CREATESAMPLE. 
		/// </summary>
		CreateStream = 0x00000080,
		/// <summary>
		/// Decompress at loadtime, decompressing or decoding whole file into memory as the target sample format. (standard sample). 
		/// </summary>
		CreateSample = 0x00000100,
		/// <summary>
		/// Load MP2, MP3, IMAADPCM or XMA into memory and leave it compressed.  During playback the FMOD software mixer will decode it in realtime as a 'compressed sample'.  Can only be used in combination with FMOD_SOFTWARE. 
		/// </summary>
		CreateCompressedSample = 0x00000200,
		/// <summary>
		/// Opens a user created static sample or stream. Use FMOD_CREATESOUNDEXINFO to specify format and/or read callbacks.  If a user created 'sample' is created with no read callback, the sample will be empty.  Use FMOD_Sound_Lock and FMOD_Sound_Unlock to place sound data into the sound if this is the case. 
		/// </summary>
		OpenUser = 0x00000400,
		/// <summary>
		/// "name_or_data" will be interpreted as a pointer to memory instead of filename for creating sounds. 
		/// </summary>
		OpenMemory = 0x00000800,
		/// <summary>
		/// "name_or_data" will be interpreted as a pointer to memory instead of filename for creating sounds.  Use FMOD_CREATESOUNDEXINFO to specify length.  This differs to FMOD_OPENMEMORY in that it uses the memory as is, without duplicating the memory into its own buffers.  Cannot be freed after open, only after Sound::release.   Will not work if the data is compressed and FMOD_CREATECOMPRESSEDSAMPLE is not used. 
		/// </summary>
		OpenMemoryPoint = 0x10000000,
		/// <summary>
		/// Will ignore file format and treat as raw pcm.  User may need to declare if data is FMOD_SIGNED or FMOD_UNSIGNED 
		/// </summary>
		OpenRaw = 0x00001000,
		/// <summary>
		/// Just open the file, dont prebuffer or read.  Good for fast opens for info, or when sound::readData is to be used. 
		/// </summary>
		OpenOnly = 0x00002000,
		/// <summary>
		/// For FMOD_CreateSound - for accurate FMOD_Sound_GetLength / FMOD_Channel_SetPosition on VBR MP3, AAC and MOD/S3M/XM/IT/MIDI files.  Scans file first, so takes longer to open. FMOD_OPENONLY does not affect this. 
		/// </summary>
		AccurateTime = 0x00004000,
		/// <summary>
		/// For corrupted / bad MP3 files.  This will search all the way through the file until it hits a valid MPEG header.  Normally only searches for 4k. 
		/// </summary>
		MpegSearch = 0x00008000,
		/// <summary>
		/// For opening sounds and getting streamed subsounds (seeking) asyncronously.  Use Sound::getOpenState to poll the state of the sound as it opens or retrieves the subsound in the background. 
		/// </summary>
		Nonblocking = 0x00010000,
		/// <summary>
		/// Unique sound, can only be played one at a time 
		/// </summary>
		Unique = 0x00020000,
		/// <summary>
		/// Make the sound's position, velocity and orientation relative to the listener. 
		/// </summary>
		HeadRelative3D = 0x00040000,
		/// <summary>
		/// Make the sound's position, velocity and orientation absolute (relative to the world). (DEFAULT) 
		/// </summary>
		WorldRelative3d = 0x00080000,
		/// <summary>
		/// This sound will follow the inverse rolloff model where mindistance = full volume, maxdistance = where sound stops attenuating, and rolloff is fixed according to the global rolloff factor.  (DEFAULT) 
		/// </summary>
		InverseRolloff3D = 0x00100000,
		/// <summary>
		/// This sound will follow a linear rolloff model where mindistance = full volume, maxdistance = silence.  
		/// </summary>
		LinearRolloff3D = 0x00200000,
		/// <summary>
		/// This sound will follow a linear-square rolloff model where mindistance = full volume, maxdistance = silence.  Rolloffscale is ignored. 
		/// </summary>
		LinearSquareRolloff3D = 0x00400000,
		/// <summary>
		/// This sound will follow the inverse rolloff model at distances close to mindistance and a linear-square rolloff close to maxdistance. 
		/// </summary>
		InverseTaperedRolloff3D = 0x00800000,
		/// <summary>
		/// This sound will follow a rolloff model defined by Sound::set3DCustomRolloff / Channel::set3DCustomRolloff.  
		/// </summary>
		CustomRolloff3D = 0x04000000,
		/// <summary>
		/// Is not affect by geometry occlusion.  If not specified in Sound::setMode, or Channel::setMode, the flag is cleared and it is affected by geometry again. 
		/// </summary>
		IgnoreGeometry3D = 0x40000000,
		/// <summary>
		/// Skips id3v2/asf/etc tag checks when opening a sound, to reduce seek/read overhead when opening files (helps with CD performance). 
		/// </summary>
		IgnoreTags = 0x02000000,
		/// <summary>
		/// Removes some features from samples to give a lower memory overhead, like Sound::getName. 
		/// </summary>
		Lowmem = 0x08000000,
		/// <summary>
		/// For sounds that start virtual (due to being quiet or low importance), instead of swapping back to audible, and playing at the correct offset according to time, this flag makes the sound play from the start. 
		/// </summary>
		VirtualPlayFromStart = 0x80000000
	}
}
