using System.Runtime.InteropServices;

// DO NOT include FMOD namespace in ANY of your classes.
// Use FMOD.SomeClass instead.
// FMOD classes seriously interfere with System namespace.

namespace ChaiFoxes.FMODAudio
{
	/// <summary>
	/// Audio manager. Contains main audiosystem parameters.
	/// </summary>
	public static class CoreSystem
	{
		/// <summary>
		/// Low level FMOD sound system.
		/// </summary>
		public static FMOD.System Native;

		/// <summary>
		/// Loads sound from file.
		/// Use this function to load short sound effects.
		/// </summary>
		public static Sound LoadSound(string path)
		{
			var buffer = FileLoader.LoadFileAsBuffer(path);

			var info = new FMOD.CREATESOUNDEXINFO();
			info.length = (uint)buffer.Length;
			info.cbsize = Marshal.SizeOf(info);

			Native.createSound(
					buffer,
					FMOD.MODE.OPENMEMORY | FMOD.MODE.CREATESAMPLE,
					ref info,
					out FMOD.Sound newSound
			);

			return new Sound(newSound);
		}

		/// <summary>
		/// Loads streamed sound stream from file.
		/// Use this function to load music and long ambience tracks.
		/// </summary>
		public static Sound LoadStreamedSound(string path)
		{
			var buffer = FileLoader.LoadFileAsBuffer(path);

			// Internal FMOD pointer points to this memory, so we don't want it to go anywhere.
			var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

			var info = new FMOD.CREATESOUNDEXINFO();
			info.length = (uint)buffer.Length;
			info.cbsize = Marshal.SizeOf(info);

			Native.createStream(
					buffer,
					FMOD.MODE.OPENMEMORY | FMOD.MODE.CREATESTREAM,
					ref info,
					out FMOD.Sound newSound
			);

			return new Sound(newSound, buffer, handle);
		}


	}
}
