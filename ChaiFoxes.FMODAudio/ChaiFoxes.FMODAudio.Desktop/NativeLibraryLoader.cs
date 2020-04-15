using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ChaiFoxes.FMODAudio
{
	/// Windows and Linux-specific part of an audio manager.
	public static class NativeLibraryLoader
	{
		[DllImport("kernel32.dll")]
		private static extern IntPtr LoadLibrary(string dllToLoad);

		// NOTE: To make native libraries work on Linux, we also need <dllmap> entries in App.config.
		[DllImport("libdl.so.2")]
		private static extern IntPtr dlopen(string filename, int flags);

		private const int RTLD_LAZY = 0x0001;

		/// <summary>
		/// Loads Windows or Linux native library.
		/// </summary>
		public static void LoadNativeLibrary(string libName)
		{
			if (Environment.OSVersion.Platform != PlatformID.Unix)
			{
				if (Environment.Is64BitProcess)
				{
					LoadLibrary(Path.GetFullPath("x64/" + libName + ".dll"));
				}
				else
				{
					LoadLibrary(Path.GetFullPath("x86/" + libName + ".dll"));
				}
			}
			else
			{
				if (Environment.Is64BitProcess)
				{
					dlopen(Path.GetFullPath("/x64/lib" + libName + ".so"), RTLD_LAZY);
				}
				else
				{
					dlopen(Path.GetFullPath("/x86/lib" + libName + ".so"), RTLD_LAZY);
				}
			}
		}
	}
}