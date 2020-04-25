using ChaiFoxes.FMODAudio.Studio;
using System;

namespace ChaiFoxes.FMODAudio.Demos.Scenes
{
	public class StudioDemoScene : Scene
	{
		Bank _masterBank;
		Bank _masterStringBank;

		EventDescription _musicDescription;
		EventDescription _sfxDescription;

		EventInstance _musicInstance;

		public override void Enter()
		{
			// Initialized here only for the sake of the demo.
			// Usually has to be initialized right at startup.
			FMODManager.Init(FMODMode.CoreAndStudio, "Content");
			InitUI();

			// You would rather place the following in LoadContent() - it's here more for readability.
			// Here you load any banks that you're using. This could be a music bank, a SFX bank, etc.
			// The strings bank isn't actually necessary - however if you want to do string lookups, include it.
			_masterBank = StudioSystem.LoadBank("Master.bank");
			_masterStringBank = StudioSystem.LoadBank("Master.strings.bank");

			// Events are split in the code into descriptions and instances.
			// You may have multiple instances of one event, but the description for it should only be loaded once.
			// Here is why you want the strings bank loaded, btw. So much more intuitive than Guids.
			_musicDescription = StudioSystem.GetEvent("event:/Music/Demo");

			// Loading an event description by Guid.
			// FMOD Studio will give you a string like below when you select "Copy Guid". It has to be parsed to be used.
			FMOD.Studio.Util.parseID("{432d05d4-71d0-49bc-81fc-9c0beeaf217c}", out Guid sfxGuid);
			_sfxDescription = StudioSystem.GetEvent(sfxGuid);

			// There are three ways to load sample data (any non-streamed sounds):
			// -From a bank. This will keep ALL the bank's data in memory until unloaded.
			// -From an event description. This will just keep the event's necessary data in memory until unloaded.
			// -From an event instance. Same as above, except the data will only be in memory while an instance is.
			// Assess when you need memory loaded and for how long, then choose which method to use properly.

			// The music doesn't need its data pre-loaded, since it'll just play right away, continuously.
			_musicInstance = _musicDescription.CreateInstance();

			// Sound effects could be played whenever, so you don't want them constantly loading / unloading.
			_sfxDescription.LoadSampleData();

			_musicInstance.Start();
			//musicInstance.Paused = true;
			//musicInstance.Volume = 0.5f;
			//musicInstance.Pitch = 2f;

			// If you have any parameters set within FMOD Studio, you can change them within the code.
			//musicInstance.SetParameterValue("Local Parameter", 1);
			//AudioMgr.SetParameterValue("Global Parameter", 0.5f);

			// FMOD Studio has its own instance of the FMOD Core system, so all normal functions will still work.
			// AKA: DON'T LOAD BOTH AT THE SAME TIME!!! I don't know, nor do I want to know, what would happen.
			//var sound = AudioMgr.LoadStreamedSound("test.mp3");
			//sound.Looping = true;
			//var channel = sound.Play();

		}

		public override void Update()
		{
		}

		public override void Draw()
		{
			FMODManager.Update();
		}

		public override void Leave()
		{
		}

		private void InitUI()
		{


		}
	}
}
