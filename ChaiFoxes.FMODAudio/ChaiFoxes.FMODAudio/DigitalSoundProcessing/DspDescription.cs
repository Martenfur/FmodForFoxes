
namespace ChaiFoxes.FMODAudio.DigitalSoundProcessing
{
  interface IDspDescription
  {
    uint Pluginsdkversion { get; set; }   /* [w] The plugin SDK version this plugin is built for.  set to this to FMOD_PLUGIN_SDK_VERSION defined above. */
    char[] Name { get; set; }               /* [w] Name of the unit to be displayed in the network. */
    uint Version { get; set; }            /* [w] Plugin writer's version number. */
    int Numinputbuffers { get; set; }    /* [w] Number of input buffers to process.  Use 0 for DSPs that only generate sound and 1 for effects that process incoming sound. */
    int Numoutputbuffers { get; set; }   /* [w] Number of audio output buffers.  Only one output buffer is currently supported. */

    // TODO: Add callbacks.
  }
}
