
namespace ChaiFoxes.FMODAudio.DigitalSoundProcessing
{
	public enum DspConnectionType : int
	{
		/// <summary>
		/// Default connection type.         
		/// Audio is mixed from the input to the output DSP's audible buffer.
		/// </summary>
		Standard,
		/// <summary>
		/// Sidechain connection type.       
		/// Audio is mixed from the input to the output DSP's sidechain buffer.
		/// </summary>
		Sidechain,
		/// <summary>
		/// Send connection type.            
		/// Audio is mixed from the input to the output DSP's audible buffer, but the input is NOT executed, only copied from.  
		/// A standard connection or sidechain needs to make an input execute to generate data.
		/// </summary>
		Send,
		/// <summary>
		/// Send sidechain connection type.  
		/// Audio is mixed from the input to the output DSP's sidechain buffer, but the input is NOT executed, only copied from.  
		/// A standard connection or sidechain needs to make an input execute to generate data.
		/// </summary>
		SendSidechain,

		/// <summary>
		/// Maximum number of DSP connection types supported.
		/// </summary>
		Max,
	}
}
