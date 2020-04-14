
namespace ChaiFoxes.FMODAudio.DigitalSoundProcessing
{
  // TODO: Format properly. 
  public enum DspType : int
  {
    UNKNOWN,            /* This unit was created via a non FMOD plugin so has an unknown purpose. */
    MIXER,              /* This unit does nothing but take inputs and mix them together then feed the result to the soundcard unit. */
    OSCILLATOR,         /* This unit generates sine/square/saw/triangle or noise tones. */
    LOWPASS,            /* This unit filters sound using a high quality, resonant lowpass filter algorithm but consumes more CPU time. */
    ITLOWPASS,          /* This unit filters sound using a resonant lowpass filter algorithm that is used in Impulse Tracker, but with limited cutoff range (0 to 8060hz). */
    HIGHPASS,           /* This unit filters sound using a resonant highpass filter algorithm. */
    ECHO,               /* This unit produces an echo on the sound and fades out at the desired rate. */
    FADER,              /* This unit pans and scales the volume of a unit. */
    FLANGE,             /* This unit produces a flange effect on the sound. */
    DISTORTION,         /* This unit distorts the sound. */
    NORMALIZE,          /* This unit normalizes or amplifies the sound to a certain level. */
    LIMITER,            /* This unit limits the sound to a certain level. */
    PARAMEQ,            /* This unit attenuates or amplifies a selected frequency range. */
    PITCHSHIFT,         /* This unit bends the pitch of a sound without changing the speed of playback. */
    CHORUS,             /* This unit produces a chorus effect on the sound. */
    VSTPLUGIN,          /* This unit allows the use of Steinberg VST plugins */
    WINAMPPLUGIN,       /* This unit allows the use of Nullsoft Winamp plugins */
    ITECHO,             /* This unit produces an echo on the sound and fades out at the desired rate as is used in Impulse Tracker. */
    COMPRESSOR,         /* This unit implements dynamic compression (linked multichannel, wideband) */
    SFXREVERB,          /* This unit implements SFX reverb */
    LOWPASS_SIMPLE,     /* This unit filters sound using a simple lowpass with no resonance, but has flexible cutoff and is fast. */
    DELAY,              /* This unit produces different delays on individual channels of the sound. */
    TREMOLO,            /* This unit produces a tremolo / chopper effect on the sound. */
    LADSPAPLUGIN,       /* This unit allows the use of LADSPA standard plugins. */
    SEND,               /* This unit sends a copy of the signal to a return DSP anywhere in the DSP tree. */
    RETURN,             /* This unit receives signals from a number of send DSPs. */
    HIGHPASS_SIMPLE,    /* This unit filters sound using a simple highpass with no resonance, but has flexible cutoff and is fast. */
    PAN,                /* This unit pans the signal, possibly upmixing or downmixing as well. */
    THREE_EQ,           /* This unit is a three-band equalizer. */
    FFT,                /* This unit simply analyzes the signal and provides spectrum information back through getParameter. */
    LOUDNESS_METER,     /* This unit analyzes the loudness and true peak of the signal. */
    ENVELOPEFOLLOWER,   /* This unit tracks the envelope of the input/sidechain signal. Deprecated and will be removed in a future release. */
    CONVOLUTIONREVERB,  /* This unit implements convolution reverb. */
    CHANNELMIX,         /* This unit provides per signal channel gain, and output channel mapping to allow 1 multichannel signal made up of many groups of signals to map to a single output signal. */
    TRANSCEIVER,        /* This unit 'sends' and 'receives' from a selection of up to 32 different slots.  It is like a send/return but it uses global slots rather than returns as the destination.  It also has other features.  Multiple transceivers can receive from a single channel, or multiple transceivers can send to a single channel, or a combination of both. */
    OBJECTPAN,          /* This unit sends the signal to a 3d object encoder like Dolby Atmos. */
    MULTIBAND_EQ,       /* This unit is a flexible five band parametric equalizer. */
    MAX
  }
}
