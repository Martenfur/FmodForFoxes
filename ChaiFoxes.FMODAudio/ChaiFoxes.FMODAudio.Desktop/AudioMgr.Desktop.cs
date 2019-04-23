using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ChaiFoxes.FMODAudio
{
	/// <summary>
	/// Windows and Linux-specific part of an audio manager.
	/// </summary>
	public static partial class AudioMgr
	{

		[DllImport("kernel32.dll")]
		public static extern IntPtr LoadLibrary(string dllToLoad);


		// NOTE: To make native libraries work on Linux, we also need <dllmap> entries in App.config.
		[DllImport("libdl.so.2")]
		static extern IntPtr dlopen(string filename, int flags);

		private const int RTLD_LAZY = 0x0001;
		
		/// <summary>
		/// Loads Windows or Linux native library.
		/// </summary>
		public static void LoadNativeLibrary()
		{
			if (Environment.OSVersion.Platform != PlatformID.Unix)
			{
				if (Environment.Is64BitProcess)
				{
					LoadLibrary(Path.GetFullPath("x64/fmod.dll"));
				}
				else
				{
					LoadLibrary(Path.GetFullPath("x86/fmod.dll"));
				}
			}
			else
			{
				if (Environment.Is64BitProcess)
				{
					dlopen(Path.GetFullPath("/x64/libfmod.so"), RTLD_LAZY);
				}
				else
				{
					dlopen(Path.GetFullPath("/x86/libfmod.so"), RTLD_LAZY);
				}
			}
		}
	}
}