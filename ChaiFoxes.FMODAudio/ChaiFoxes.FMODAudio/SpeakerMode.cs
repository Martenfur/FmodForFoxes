
namespace ChaiFoxes.FMODAudio
{
	public enum SpeakerMode : int
	{
		Default,          /* Default speaker mode for the chosen output mode which will resolve after System::init. */
		Raw,              /* Assume there is no special mapping from a given channel to a speaker, channels map 1:1 in order. Use System::setSoftwareFormat to specify the speaker count. */
		Mono,             /*  1 speaker setup (monaural). */
		Stereo,           /*  2 speaker setup (stereo) front left, front right. */
		Quad,             /*  4 speaker setup (4.0)    front left, front right, surround left, surround right. */
		Surround,         /*  5 speaker setup (5.0)    front left, front right, center, surround left, surround right. */
		FivePointOne,         /*  6 speaker setup (5.1)    front left, front right, center, low frequency, surround left, surround right. */
		SevenpointOne,         /*  8 speaker setup (7.1)    front left, front right, center, low frequency, surround left, surround right, back left, back right. */
		SevenOnePointFour,   /* 12 speaker setup (7.1.4)  front left, front right, center, low frequency, surround left, surround right, back left, back right, top front left, top front right, top back left, top back right. */

		Max,              /* Maximum number of speaker modes supported. */
	}
}
