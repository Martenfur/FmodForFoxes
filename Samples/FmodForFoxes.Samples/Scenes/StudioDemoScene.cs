using FmodForFoxes.Samples.UI;
using FmodForFoxes.Studio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FmodForFoxes.Samples.Scenes
{
	public class StudioDemoScene : Scene
	{
		private List<Bank> _banks = new List<Bank>();

		EventDescription _engineDescription;
		EventDescription _musicDescription;
		EventDescription _bonkDescription;

		EventInstance _engineInstance;
		EventInstance _musicInstance;

		int _rpm = 2300;

		private float _rotation;
		private float _rotationSpeed = 0.0001f;

		private int _musicIntensity = 1;


		private Button _rpmUp;
		private Button _rpmDown;
		private Label _rpmLabel;
		private Button _engineStart;

		private Button _intensityUp;
		private Button _intensityDown;
		private Label _musicLabel;
		private Button _musicStart;

		private Button _bonk;

		private Button _back;


		public override void Enter()
		{
			InitUI();

			// You would rather place the following in LoadContent() - it's here more for readability.
			// Here you load any banks that you're using. This could be a music bank, a SFX bank, etc.
			// The strings bank isn't actually necessary - however if you want to do string lookups, include it.
			_banks.Add(StudioSystem.LoadBank("Master.bank"));
			_banks.Add(StudioSystem.LoadBank("Master.strings.bank"));
			_banks.Add(StudioSystem.LoadBank("Music.bank"));
			_banks.Add(StudioSystem.LoadBank("SFX.bank"));
			_banks.Add(StudioSystem.LoadBank("Vehicles.bank"));
			_banks.Add(StudioSystem.LoadBank("VO.bank"));
			_banks.Add(StudioSystem.LoadBank("Dialogue_EN.bank"));

			// Events are split in the code into descriptions and instances.
			// You may have multiple instances of one event, but the description for it should only be loaded once.
			// Here is why you want the strings bank loaded, btw. So much more intuitive than Guids.

			_engineDescription = StudioSystem.GetEvent("event:/Vehicles/Car Engine");
			_musicDescription = StudioSystem.GetEvent("event:/Music/Level 03");

			// Loading an event description by Guid.
			// FMOD Studio will give you a string like below when you select "Copy Guid". It has to be parsed to be used.
			FMOD.Studio.Util.parseID("{be6203d8-c8d8-41c5-8ce6-bce0de95807b}", out var sfxGuid);
			_bonkDescription = StudioSystem.GetEvent(sfxGuid);

			// There are three ways to load sample data (any non-streamed sounds):
			// -From a bank. This will keep ALL the bank's data in memory until unloaded.
			// -From an event description. This will just keep the event's necessary data in memory until unloaded.
			// -From an event instance. Same as above, except the data will only be in memory while an instance is.
			// Assess when you need memory loaded and for how long, then choose which method to use properly.
			_engineDescription.LoadSampleData();
			// The music doesn't need its data pre-loaded, since it'll just play right away, continuously.
			_engineInstance = _engineDescription.CreateInstance();
			_musicInstance = _musicDescription.CreateInstance();
			
			// Sound effects could be played whenever, so you don't want them constantly loading / unloading.
			_bonkDescription.LoadSampleData();

			// If you have any parameters set within FMOD Studio, you can change them within the code.
			_engineInstance.SetParameterValue("RPM", _rpm);
			_engineInstance.SetParameterValue("Load", -1f);

		}


		public override void Update()
		{
			_rpmLabel.Text = "rpm " + _rpm;
			_musicLabel.Text = "music intensity " + _musicIntensity;

			if (_engineInstance.PlaybackState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
			{
				_rotation += _rotationSpeed * _rpm;
			}
			if (_rotation > MathHelper.TwoPi)
			{
				_rotation -= MathHelper.TwoPi;
			}

			// haxx 
			// Basically, I am too lazy to look up how 
			// to detect the end of music properly. : - )
			if (_musicInstance.TimelinePosition > (60 * 2 + 32) * 1000)
			{
				_musicStart.Text = "start";
				_musicInstance.Stop();
			}
		}


		public override void Draw()
		{
			var scale = 1f;
			if (OperatingSystem.IsAndroid())
			{
				scale = Math.Min(Game1.ScreenSize.X, Game1.ScreenSize.Y) / 2f / (float)Resources.Gato.Width;
			}


			UIController.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
			UIController.SpriteBatch.Draw(
				Resources.Gato,
				new Vector2(Game1.ScreenSize.X / 2, Game1.ScreenSize.Y * 0.3f),
				null,
				UIController.Text,
				_rotation,
				Vector2.One * Resources.Gato.Width / 2,
				Vector2.One * scale,
				SpriteEffects.None,
				0
			);
			UIController.SpriteBatch.End();
		}

		public override void Leave()
		{
			_engineInstance.Stop();
			_musicInstance.Stop();

			_engineInstance.Dispose();
			_musicInstance.Dispose();

			_rpmUp.Destroy();
			_rpmDown.Destroy();
			_rpmLabel.Destroy();
			_engineStart.Destroy();

			_intensityUp.Destroy();
			_intensityDown.Destroy();
			_musicLabel.Destroy();
			_musicStart.Destroy();

			_bonk.Destroy();

			_back.Destroy();

			foreach(var bank in _banks)
			{ 
				bank.Unload();
			}
		}

		private void InitUI()
		{

			_rpmLabel = new Label(
				"rpm",
				() => new Vector2(Game1.ScreenSize.X / 2 - Game1.ScreenSize.X / 4, Game1.ScreenSize.Y * 0.7f - 64)
			);
			_rpmDown = new Button(
				"<",
				() => new Vector2(
					_rpmLabel.Position.X - 48,
					_rpmLabel.Position.Y + 64
				),
				new Vector2(64, 64),
				() =>
				{
					_rpm -= 25;
					if (_rpm < 0)
					{
						_rpm = 0;
					}
					_engineInstance.SetParameterValue("RPM", _rpm);
				}
			);
			_rpmUp = new Button(
				">",
				() => new Vector2(
					_rpmLabel.Position.X + 48,
					_rpmLabel.Position.Y + 64
				),
				new Vector2(64, 64),
				() => 
				{
					_rpm += 25;
					if (_rpm > 10000)
					{ 
						_rpm = 10000;	
					}
					_engineInstance.SetParameterValue("RPM", _rpm);
				}
			);

			_engineStart = new Button(
				"start",
				() => new Vector2(
					_rpmLabel.Position.X,
					_rpmLabel.Position.Y + 128 + 32
				),
				new Vector2(128 + 32, 64),
				null,
				() =>
				{
					if (_engineInstance.PlaybackState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
					{
						_engineStart.Text = "start";
						_engineInstance.Stop();
					}
					else
					{
						_engineStart.Text = "stop";
						_engineInstance.Start();
					}
				}
			);

			_musicLabel = new Label(
				"music",
				() => new Vector2(Game1.ScreenSize.X / 2 + Game1.ScreenSize.X / 4, Game1.ScreenSize.Y * 0.7f - 64)
			);
			_intensityDown = new Button(
				"<",
				() => new Vector2(
					_musicLabel.Position.X - 48,
					_musicLabel.Position.Y + 64
				),
				new Vector2(64, 64),
				null,
				() =>
				{
					_musicIntensity -= 1;
					if (_musicIntensity < 0)
					{
						_musicIntensity = 0;
					}
					_musicInstance.SetParameterValue("Intensity", _musicIntensity);
				}
			);
			_intensityUp = new Button(
				">",
				() => new Vector2(
					_musicLabel.Position.X + 48,
					_musicLabel.Position.Y + 64
				),
				new Vector2(64, 64),
				null,
				() =>
				{
					_musicIntensity += 1;
					if (_musicIntensity > 4)
					{
						_musicIntensity = 4;
					}
					_musicInstance.SetParameterValue("Intensity", _musicIntensity);
				}
			);

			_musicStart = new Button(
				"start",
				() => new Vector2(
					_musicLabel.Position.X,
					_musicLabel.Position.Y + 128 + 32
				),
				new Vector2(128 + 32, 64),
				null,
				() =>
				{
					if (_musicInstance.PlaybackState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
					{
						_musicStart.Text = "start";
						_musicInstance.Stop();
					}
					else
					{
						_musicStart.Text = "stop";
						_musicInstance.Start();
						_musicInstance.SetParameterValue("Intensity", _musicIntensity);
					}
				}
			);


			_bonk = new Button(
				"bonk",
				() => new Vector2(Game1.ScreenSize.X / 2, _musicLabel.Position.Y + 128 + 32),
				new Vector2(128, 64),
				null,
				() =>
				{
					var i = _bonkDescription.CreateInstance();
					i.SetParameterValue("Speed", 4);
					i.Start(); // That's actually a memory leak. Event instances should be disposed.
				}
			);


			_back = new Button(
				"<-",
				() => new Vector2(32, 32),
				new Vector2(64, 64),
				null,
				() => SceneController.ChangeScene(new DemoSelectorScene())
			);
		}
	}
}
