
namespace ChaiFoxes.FMODAudio.DigitalSoundProcessing
{
	public interface IDspConnection : IUserData
	{
		Dsp Input { get; }
		Dsp Output { get; }
		float MixVolume { get; set; } 

		FmodResult SetMixMatrix(float[] matrix, int outchannels, int inchannels, int inchannel_hop = 0);

		FmodResult GetMixMatrix(float[] matrix, out int outchannels, out int inchannels, int inchannel_hop = 0);

		FmodResult GetType(out DspConnectionType type);
	}
}
