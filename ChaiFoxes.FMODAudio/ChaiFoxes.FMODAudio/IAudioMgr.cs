using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using ChaiFoxes.FMODAudio.Studio;

// DO NOT include FMOD namespace in ANY of your classes.
// Use FMOD.SomeClass instead.
// FMOD classes seriously interfere with System namespace.

namespace ChaiFoxes.FMODAudio
{
	/// <summary>
	/// Audio manager. Contains main audiosystem parameters.
	/// </summary>
	public interface IAudioMgr
	{

		/// <summary>
		/// Initializes FMOD Studio with default parameters.
		/// 
		/// If you want to exclusively use the default wrapper, call
		/// LoadNativeLibraries() instead.
		/// </summary>
		public void InitStudio(string rootDir);

		// TODO: Split Studio into its own namespace. Don't touch Core

		/// <summary>
		/// Initializes FMOD Studio with custom parameters.
		/// 
		/// If you want to exclusively use the default wrapper, call
		/// LoadNativeLibraries() instead.
		/// </summary>
		void InitStudio(
				string rootDir,
				int maxChannels,
				FMOD.Studio.INITFLAGS studioInitFlags,
				FMOD.INITFLAGS initFlags
		);


		/// <summary>
		/// Initializes FMOD Core with default parameters.
		/// Loading this way disables Studio functionality.
		/// 
		/// If you want to exclusively use the default wrapper, call
		/// LoadNativeLibraries() instead.
		/// </summary>
		void Init(string rootDir);

		/// <summary>
		/// Initializes FMOD Core with custom parameters.
		/// Loading this way disables Studio functionality.
		/// 
		/// If you want to exclusively use the default wrapper, call
		/// LoadNativeLibraries() instead.
		/// </summary>
		void Init(
				string rootDir,
				uint dspBufferLength,
				int dspBufferCount,
				int maxChannels,
				FMOD.INITFLAGS initFlags
		);


		void Update();

		void Unload();

		/// <summary>
		/// STUDIO:
		/// Loads bank from file with the default flag.
		/// </summary>
		Bank LoadBank(string name);

		/// <summary>
		/// STUDIO:
		/// Loads bank from file with custom flags.
		/// </summary>
		Bank LoadBank(string name, FMOD.Studio.LOAD_BANK_FLAGS flags);


		/// <summary>
		/// STUDIO:
		/// Retrieves an event via internal path, i.e. "event:/UI/Cancel", or ID string, i.e. "{2a3e48e6-94fc-4363-9468-33d2dd4d7b00}".
		/// </summary>
		EventDescription GetEvent(string path);


		/// <summary>
		/// STUDIO:
		/// Retrieves an event via 128-bit GUID.
		/// To parse a GUID from a string id, i.e. "{2a3e48e6-94fc-4363-9468-33d2dd4d7b00}", use FMOD.Studio.Util.parseID().
		/// </summary>
		EventDescription GetEvent(Guid id);

		/// <summary>
		/// STUDIO:
		/// Retrieves a VCA via internal path, i.e. "vca:/MyVCA", or ID string, i.e. "{d9982c58-a056-4e6c-b8e3-883854b4bffb}".
		/// </summary>
		VCA GetVCA(string path);

		/// <summary>
		/// STUDIO:
		/// Retrieves a VCA via 128-bit GUID.
		/// To parse a GUID from a string id, i.e. "{d9982c58-a056-4e6c-b8e3-883854b4bffb}", use FMOD.Studio.Util.parseID().
		/// </summary>
		VCA GetVCA(Guid id);

		/// <summary>
		/// STUDIO:
		/// Retrieves a global parameter description by its name.
		/// </summary>
		FMOD.Studio.PARAMETER_DESCRIPTION GetParameterDescription(string name);

		/// <summary>
		/// STUDIO:
		/// Retrieves a global parameter description by its ID.
		/// </summary>
		FMOD.Studio.PARAMETER_DESCRIPTION GetParameterDescription(FMOD.Studio.PARAMETER_ID id);

		/// <summary>
		/// STUDIO:
		/// Retrieves a global parameter's current value via its name (case sensitive).
		/// This ignores modulation / automation applied to the parameter within Studio.
		/// </summary>
		float GetParameterTargetValue(string name);

		/// <summary>
		/// STUDIO:
		/// Retrieves a global parameter's current value via its ID.
		/// This ignores modulation / automation applied to the parameter within Studio.
		/// </summary>
		float GetParameterTargetValue(FMOD.Studio.PARAMETER_ID id);

		/// <summary>
		/// STUDIO:
		/// Retrieves a global parameter's current value via its name (case sensitive).
		/// This takes into account modulation / automation applied to the parameter within Studio.
		/// </summary>
		float GetParameterCurrentValue(string name);

		/// <summary>
		/// STUDIO:
		/// Retrieves a global parameter's current value via its ID.
		/// This takes into account modulation / automation applied to the parameter within Studio.
		/// </summary>
		float GetParameterCurrentValue(FMOD.Studio.PARAMETER_ID id);

		/// <summary>
		/// STUDIO:
		/// Sets a global parameter's value via its name (case sensitive).
		/// Enable ignoreSeekSpeed to set the value instantly, ignoring the parameter's seek speed.
		/// </summary>
		void SetParameterValue(string name, float value, bool ignoreSeekSpeed = false);

		/// <summary>
		/// STUDIO:
		/// Sets a global parameter's value via its ID.
		/// Enable ignoreSeekSpeed to set the value instantly, ignoring the parameter's seek speed.
		/// </summary>
		void SetParameterValue(FMOD.Studio.PARAMETER_ID id, float value, bool ignoreSeekSpeed = false);

		/// <summary>
		/// STUDIO:
		/// Sets multiple global parameters' values via their IDs.
		/// Enable ignoreSeekSpeed to set the values instantly, ignoring the parameters' seek speeds.
		/// </summary>
		void SetParameterValues(FMOD.Studio.PARAMETER_ID[] ids, float[] values, bool ignoreSeekSpeed = false);

		/// <summary>
		/// CORE:
		/// Create new channel group with given name.
		/// </summary>
		FMOD.ChannelGroup CreateChannelGroup(string name)
		{
			FMODSystem.createChannelGroup(name, out FMOD.ChannelGroup channelGroup);
			return channelGroup;
		}

		/// <summary>
		/// CORE:
		/// Loads sound from file.
		/// Use this function to load short sound effects.
		/// </summary>
		ISound LoadSound(string name)
		{
			var buffer = LoadFileAsBuffer(Path.Combine(_rootDir, name));

			var info = new FMOD.CREATESOUNDEXINFO();
			info.length = (uint)buffer.Length;
			info.cbsize = Marshal.SizeOf(info);

			FMODSystem.createSound(
					buffer,
					SoundMode.OPENMEMORY | SoundMode.CREATESAMPLE,
					ref info,
					out FMOD.Sound newSound
			);

			return new ISound(newSound);
		}

		/// <summary>
		/// CORE:
		/// Loads streamed sound stream from file.
		/// Use this function to load music and long ambience tracks.
		/// </summary>
		ISound LoadStreamedSound(string name)
		{
			var buffer = LoadFileAsBuffer(Path.Combine(_rootDir, name));

			// Internal FMOD pointer points to this memory, so we don't want it to go anywhere.
			var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

			var info = new FMOD.CREATESOUNDEXINFO();
			info.length = (uint)buffer.Length;
			info.cbsize = Marshal.SizeOf(info);

			FMODSystem.createStream(
					buffer,
					SoundMode.OPENMEMORY | SoundMode.CREATESTREAM,
					ref info,
					out FMOD.Sound newSound
			);

			return new ISound(newSound, buffer, handle);
		}

		/// <summary>
		/// CORE:
		/// Loads file as a byte array.
		/// </summary>
		private static byte[] LoadFileAsBuffer(string path)
		{
			// TitleContainer is cross-platform Monogame file loader.
			var stream = TitleContainer.OpenStream(path);

			// File is opened as a stream, so we need to read it to the end.
			byte[] buffer = new byte[16 * 1024];
			byte[] bufferRes;
			using (MemoryStream ms = new MemoryStream())
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