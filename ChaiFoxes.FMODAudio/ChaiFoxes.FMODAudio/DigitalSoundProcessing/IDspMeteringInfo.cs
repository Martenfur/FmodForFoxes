
namespace ChaiFoxes.FMODAudio.DigitalSoundProcessing
{
	public interface IDspMeteringInfo
	{
		int NumSamples { get; set; }        /* [r] The number of samples considered for this metering info. */
		float[] PeakLevel { get; set; }       /* [r] The peak level per channel. */
		float[] RmsLevel { get; set; }        /* [r] The rms level per channel. */
		short NumChannels { get; set; }       /* [r] Number of channels. */
	}
}
