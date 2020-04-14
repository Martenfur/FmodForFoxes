using System;

namespace ChaiFoxes.FMODAudio.DigitalSoundProcessing
{
	public interface IDsp : IUserData, IDisposable
	{

		FmodResult GetSystemObject(out System system);

		// Connection / disconnection / input and output enumeration.
		FmodResult AddInput(IDsp input);

		FmodResult AddInput(IDsp input, out IDspConnection connection, DspConnectionType type = DspConnectionType.Standard);

		FmodResult DisconnectFrom(IDsp target, IDspConnection connection);

		FmodResult DisconnectAll(bool inputs, bool outputs);

		int NumInputs { get; }
		int NumOutputs { get; }

		FmodResult GetInput(int index, out IDsp input, out IDspConnection inputconnection);

		FmodResult GetOutput(int index, out IDsp output, out IDspConnection outputconnection);


		// IDsp unit control.
		bool Active { get; set; }
		bool Bypass { get; set; }

		FmodResult SetWetDryMix(float prewet, float postwet, float dry);

		FmodResult GetWetDryMix(out float prewet, out float postwet, out float dry);

		FmodResult SetChannelFormat(ChannelMask channelmask, int numchannels, SpeakerMode source_speakermode);

		FmodResult GetChannelFormat(out ChannelMask channelmask, out int numchannels, out SpeakerMode source_speakermode);

		FmodResult GetOutputChannelFormat(ChannelMask inmask, int inchannels, SpeakerMode inspeakermode, out ChannelMask outmask, out int outchannels, out SpeakerMode outspeakermode);

		FmodResult reset();



		// IDsp parameter control.
		FmodResult SetParameterFloat(int index, float value);

		FmodResult SetParameterInt(int index, int value);

		FmodResult SetParameterBool(int index, bool value);

		FmodResult SetParameterData(int index, byte[] data);

		FmodResult GetParameterFloat(int index, out float value);

		FmodResult GetParameterInt(int index, out int value);

		FmodResult GetParameterBool(int index, out bool value);

		FmodResult GetParameterData(int index, out IntPtr data, out uint length);

		FmodResult GetNumParameters(out int numparams);

		FmodResult GetParameterInfo(int index, out Dsp_PARAMETER_DESC desc);

		FmodResult GetDataParameterIndex(int datatype, out int index);

		FmodResult showConfigDialog(IntPtr hwnd, bool show);

		//  IDsp attributes.
		FmodResult GetInfo(out string name, out uint version, out int channels, out int configwidth, out int configheight);

		FmodResult GetInfo(out uint version, out int channels, out int configwidth, out int configheight);

		FmodResult GetType(out DspType type);

		FmodResult GetIdle(out bool idle);



		// Userdata set/get.
		FmodResult SetUserData(IntPtr userdata);

		FmodResult GetUserData(out IntPtr userdata);



		// Metering.
		FmodResult SetMeteringEnabled(bool inputEnabled, bool outputEnabled);

		FmodResult GetMeteringEnabled(out bool inputEnabled, out bool outputEnabled);

		FmodResult GetMeteringInfo(IntPtr zero, out IDspMeteringInfo outputInfo);

		FmodResult GetMeteringInfo(out IDspMeteringInfo inputInfo, IntPtr zero);

		FmodResult GetMeteringInfo(out IDspMeteringInfo inputInfo, out IDspMeteringInfo outputInfo);

		FmodResult GetCPUUsage(out uint exclusive, out uint inclusive);

	}
}