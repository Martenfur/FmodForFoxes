using Microsoft.Xna.Framework;

namespace FmodForFoxes
{
	public static class FileLoader
	{
		public static string RootDirectory = "";

		/// <summary>
		/// Loads file as a byte array.
		/// </summary>
		public static byte[] LoadFileAsBuffer(string path)
		{
			// NOTE: Use this method to load audio files from memory 
			// instead of built-in methods which load files directly.
			// They will not work on some platforms.

			// TitleContainer is cross-platform Monogame file loader.
			var stream = TitleContainer.OpenStream(Path.Combine(RootDirectory, path));

			return LoadFileAsBuffer(stream);
		}

		/// <summary>
		/// Loads entire stream as a byte array.
		/// </summary>
		public static byte[] LoadFileAsBuffer(Stream stream)
		{
			// NOTE: Use this method to load audio files from memory 
			// instead of built-in methods which load files directly.
			// They will not work on some platforms.

			// File is opened as a stream, so we need to read it to the end.
			var buffer = new byte[16 * 1024];
			byte[] bufferRes;
			using (var ms = new MemoryStream())
			{
				int read;
				while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, read);
				}
				bufferRes = ms.ToArray();
			}
			return bufferRes;
		}
	}
}
