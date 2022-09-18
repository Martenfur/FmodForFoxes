using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ChaiFoxes.FMODAudio
{
	/// Windows and Linux-specific part of an audio manager.
	public static class NativeLibraryLoader
	{
		private static bool _loggingEnabled;

		static NativeLibraryLoader()
		{
			NativeLibrary.SetDllImportResolver(
				Assembly.GetExecutingAssembly(),
				(libraryName, assembly, dllImportSearchPath) =>
				{
					libraryName = Path.GetFileNameWithoutExtension(libraryName);
					if (dllImportSearchPath == null)
					{
						dllImportSearchPath = DllImportSearchPath.AssemblyDirectory;
					}
					return NativeLibrary.Load(SelectDefaultLibraryName(libraryName, _loggingEnabled), assembly, dllImportSearchPath);
				}
			);
		}

		/// <summary>
		/// Loads Windows or Linux native library.
		/// </summary>
		public static void LoadNativeLibrary(string libName, bool loggingEnabled = false)
		{
			_loggingEnabled = loggingEnabled;
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
