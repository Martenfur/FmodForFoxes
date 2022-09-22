using FmodForFoxes.Samples.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FmodForFoxes.Samples.Scenes
{
	public class CoreDemoScene : Scene
	{
		private Sound _sound;
		private Channel _channel;

		private float _rotation;
		private float _rotationSpeed = 0.01f;

		private Button _lowPassUp;
		private Button _lowPassDown;
		private Label _lowPass;

		private Button _pitchUp;
		private Button _pitchDown;
		private Label _pitch;

		private Button _pause;

		private Button _back;

		public override void Enter()
		{
			InitUI();

			// You can load pretty much any popular audio format.
			// I'd recommend .ogg for music, tho.
			_sound = CoreSystem.LoadStreamedSound("test.mp3");
			_sound.Looping = true;
			//sound.LowPass = 0.1f;
			//sound.Volume = 2;
			//sound.Pitch = 2;

			_channel = _sound.Play();
		}


		public override void Update()
		{
			_lowPass.Text = "low pass " + _channel.LowPass.ToString("0.00");
			_pitch.Text = "pitch " + _channel.Pitch.ToString("0.00");

			if (_channel.Paused)
			{
				return;
			}

			_rotation += _rotationSpeed * _channel.Pitch * _channel.Pitch;
			if (_rotation > MathHelper.TwoPi)
			{
				_rotation -= MathHelper.TwoPi;
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
				Game1.ScreenSize / 2,
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
			_lowPassUp.Destroy();
			_lowPassDown.Destroy();
			_lowPass.Destroy();

			_pitchUp.Destroy();
			_pitchDown.Destroy();
			_pitch.Destroy();

			_pause.Destroy();

			_channel.Stop();
			_sound.Dispose();

			_back.Destroy();
		}


		private void InitUI()
		{
			_lowPass = new Label(
				"low pass",
				() => new Vector2(Game1.ScreenSize.X / 2 - Game1.ScreenSize.X / 4, Game1.ScreenSize.Y * 0.7f)
			);
			_lowPassDown = new Button(
				"<",
				() => new Vector2(
					_lowPass.Position.X - 48,
					_lowPass.Position.Y + 64
				),
				new Vector2(64, 64),
				() => _channel.LowPass -= 0.01f
			);
			_lowPassUp = new Button(
				">",
				() => new Vector2(
					_lowPass.Position.X + 48,
					_lowPass.Position.Y + 64
				),
				new Vector2(64, 64),
				() => _channel.LowPass += 0.01f
			);

			_pitch = new Label(
				"low pass",
				() => new Vector2(Game1.ScreenSize.X / 2 + Game1.ScreenSize.X / 4, Game1.ScreenSize.Y * 0.7f)
			);
			_pitchDown = new Button(
				"<",
				() => new Vector2(
					_pitch.Position.X - 48,
					_pitch.Position.Y + 64
				),
				new Vector2(64, 64),
				() => _channel.Pitch -= 0.01f
			);
			_pitchUp = new Button(
				">",
				() => new Vector2(
					_pitch.Position.X + 48,
					_pitch.Position.Y + 64
				),
				new Vector2(64, 64),
				() => _channel.Pitch += 0.01f
			);

			_pause = new Button(
				"pause",
				() => new Vector2(Game1.ScreenSize.X / 2, Game1.ScreenSize.Y * 0.7f + 64),
				new Vector2(128, 64),
				null,
				() =>
				{
					if (_channel.Paused)
					{
						_pause.Text = "pause";
						_channel.Resume();
					}
					else
					{
						_pause.Text = "resume";
						_channel.Pause();
					}
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
