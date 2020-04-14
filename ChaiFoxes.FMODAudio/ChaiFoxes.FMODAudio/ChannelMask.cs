using System;

namespace ChaiFoxes.FMODAudio
{
	[Flags]
	public enum ChannelMask : uint
	{
		FrontLeft = 0x00000001,
		FrontRight = 0x00000002,
		FrontCenter = 0x00000004,
		LowFrequency = 0x00000008,
		SurroundLeft = 0x00000010,
		SurroundRight = 0x00000020,
		BackLeft = 0x00000040,
		BackRight = 0x00000080,
		BackCenter = 0x00000100,

		Mono = (FrontLeft),
		Stereo = (FrontLeft | FrontRight),
		LRC = (FrontLeft | FrontRight | FrontCenter),
		Quad = (FrontLeft | FrontRight | SurroundLeft | SurroundRight),
		Surround = (FrontLeft | FrontRight | FrontCenter | SurroundLeft | SurroundRight),
		FivePoint = (FrontLeft | FrontRight | FrontCenter | LowFrequency | SurroundLeft | SurroundRight),
		FivePointOneRears = (FrontLeft | FrontRight | FrontCenter | LowFrequency | BackLeft | BackRight),
		SevenPoint0 = (FrontLeft | FrontRight | FrontCenter | SurroundLeft | SurroundRight | BackLeft | BackRight),
		SevenPoint1 = (FrontLeft | FrontRight | FrontCenter | LowFrequency | SurroundLeft | SurroundRight | BackLeft | BackRight)
	}
}
