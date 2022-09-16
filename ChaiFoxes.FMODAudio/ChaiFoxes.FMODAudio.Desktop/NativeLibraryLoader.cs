using System;
using System.Runtime.InteropServices;

namespace ChaiFoxes.FMODAudio
{
	/// Windows and Linux-specific part of an audio manager.
	public static class NativeLibraryLoader
	{
		/// <summary>
		/// Loads Windows or Linux native library.
		/// </summary>
		public static void LoadNativeLibrary(string libName)
		{
			var prefix = "x86/";
			if (Environment.Is64BitProcess)
			{
				prefix = "x64/";
			}

			NativeLibrary.Load(prefix + SelectDefaultLibraryName(libName));
		}

		public static string SelectDefaultLibraryName(string libName, bool loggingEnabled = false)
		{
			string name;

			if (OperatingSystem.IsWindows())
			{
				name = loggingEnabled ? $"{libName}L.dll" : $"{libName}.dll";
			}
			else if (OperatingSystem.IsLinux() || OperatingSystem.IsAndroid())
			{
				name = loggingEnabled ? $"lib{libName}L.so" : $"lib{libName}.so";
			}
			else
			{
				throw new PlatformNotSupportedException();
			}

			return name;
		}
	}
}
