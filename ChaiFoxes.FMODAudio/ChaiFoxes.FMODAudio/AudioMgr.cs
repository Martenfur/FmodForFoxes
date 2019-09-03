using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

// DO NOT include FMOD namespace in ANY of your classes.
// Use FMOD.SomeClass instead.
// FMOD classes seriously interfere with System namespace.

namespace ChaiFoxes.FMODAudio
{
    /// <summary>
    /// Audio manager. Contains main audiosystem parameters.
    /// </summary>
    public static partial class AudioMgr
    {
        /// <summary>
        /// FMOD Studio sound system.
        /// </summary>
        public static FMOD.Studio.System FMODSystem;

        /// <summary>
        /// Low level FMOD sound system.
        /// </summary>
        public static FMOD.System FMODCoreSystem;

        /// <summary>
        /// Root directory for banks, music, sounds, etc.
        /// </summary>
        public static string _rootDir;

        /// <summary>
        /// Initializes FMOD Studio with default parameters.<para/>
        /// If you want to exclusively use the default wrapper, call
        /// LoadNativeLibraries() instead.
        /// </summary>
        public static void Init(string rootDir) =>
            Init(rootDir, 256, FMOD.Studio.INITFLAGS.NORMAL, FMOD.INITFLAGS.NORMAL);

        /// <summary>
        /// Initializes FMOD Studio with custom parameters.<para/>
        /// 
        /// If you want to exclusively use the default wrapper, call
        /// LoadNativeLibraries() instead.
        /// </summary>
        public static void Init(
            string rootDir, 
            int maxChannels, 
            FMOD.Studio.INITFLAGS studioInitFlags,
            FMOD.INITFLAGS initFlags)
        {
            _rootDir = rootDir;
            LoadNativeLibraries();

            FMOD.Studio.System.create(out FMOD.Studio.System system);
            FMODSystem = system;

            FMODSystem.getCoreSystem(out FMOD.System coreSystem);
            FMODCoreSystem = coreSystem;

            FMODSystem.initialize(maxChannels, studioInitFlags, initFlags, (IntPtr) 0);
        }

        public static void Update() => FMODSystem.update();

        public static void Unload() => FMODSystem.release();

        /// <summary>
		/// Loads bank from file with the default flag.
		/// </summary>
        public static Bank LoadBank(string name) =>
            LoadBank(name, FMOD.Studio.LOAD_BANK_FLAGS.NORMAL);

        /// <summary>
		/// Loads bank from file with custom flags.
		/// </summary>
        public static Bank LoadBank(string name, FMOD.Studio.LOAD_BANK_FLAGS flags)
        {
            FMODSystem.loadBankFile(
                Path.Combine(_rootDir, name),
                flags,
                out FMOD.Studio.Bank bank);

            bank.getPath(out string path);

            return new Bank(bank);
        }

        /// <summary>
        /// Retrieve an event via internal path, i.e. "event:/UI/Cancel", or ID string, i.e. "{2a3e48e6-94fc-4363-9468-33d2dd4d7b00}".
        /// </summary>
        public static EventDescription GetEvent(string path)
        {
            FMODSystem.getEvent(path, out FMOD.Studio.EventDescription eventDescription);
            return new EventDescription(eventDescription);
        }

        /// <summary>
        /// Retrieve an event via 128-bit GUID.<para/>
        /// To parse a GUID from a string id, i.e. "{2a3e48e6-94fc-4363-9468-33d2dd4d7b00}", use FMOD.Studio.Util.parseID().
        /// </summary>
        public static EventDescription GetEvent(Guid id)
        {
            FMODSystem.getEventByID(id, out FMOD.Studio.EventDescription eventDescription);
            return new EventDescription(eventDescription);
        }

        /// <summary>
        /// Retrieve a VCA via internal path, i.e. "vca:/MyVCA", or ID string, i.e. "{d9982c58-a056-4e6c-b8e3-883854b4bffb}".
        /// </summary>
        public static VCA GetVCA(string path)
        {
            FMODSystem.getVCA(path, out FMOD.Studio.VCA vca);
            return new VCA(vca);
        }

        /// <summary>
        /// Retrieve a VCA via 128-bit GUID.<para/>
        /// To parse a GUID from a string id, i.e. "{d9982c58-a056-4e6c-b8e3-883854b4bffb}", use FMOD.Studio.Util.parseID().
        /// </summary>
        public static VCA GetVCA(Guid id)
        {
            FMODSystem.getVCAByID(id, out FMOD.Studio.VCA vca);
            return new VCA(vca);
        }

        /// <summary>
        /// CORE:
        /// Create new channel group with given name.
        /// </summary>
        public static FMOD.ChannelGroup CreateChannelGroup(string name)
        {
            FMODCoreSystem.createChannelGroup(name, out FMOD.ChannelGroup channelGroup);
            return channelGroup;
        }

        /// <summary>
        /// CORE:
		/// Loads sound from file.
		/// Use this function to load short sound effects.
		/// </summary>
		public static Sound LoadSound(string name)
        {
            var buffer = LoadFileAsBuffer(Path.Combine(_rootDir, name));

            var info = new FMOD.CREATESOUNDEXINFO();
            info.length = (uint)buffer.Length;
            info.cbsize = Marshal.SizeOf(info);

            FMODCoreSystem.createSound(
                buffer,
                FMOD.MODE.OPENMEMORY | FMOD.MODE.CREATESAMPLE,
                ref info,
                out FMOD.Sound newSound
            );

            return new Sound(newSound);
        }

        /// <summary>
        /// CORE:
        /// Loads streamed sound stream from file.
        /// Use this function to load music and long ambience tracks.
        /// </summary>
        public static Sound LoadStreamedSound(string name)
        {
            var buffer = LoadFileAsBuffer(Path.Combine(_rootDir, name));

            // Internal FMOD pointer points to this memory, so we don't want it to go anywhere.
            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

            var info = new FMOD.CREATESOUNDEXINFO();
            info.length = (uint)buffer.Length;
            info.cbsize = Marshal.SizeOf(info);

            FMODCoreSystem.createStream(
                buffer,
                FMOD.MODE.OPENMEMORY | FMOD.MODE.CREATESTREAM,
                ref info,
                out FMOD.Sound newSound
            );

            return new Sound(newSound, buffer, handle);
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