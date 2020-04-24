using System;
using System.Runtime.InteropServices;
using ChaiFoxes.FMODAudio.Demos.Scenes;
using ChaiFoxes.FMODAudio.Demos.UI;
using ChaiFoxes.FMODAudio.Studio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ChaiFoxes.FMODAudio.Demos
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		private static GraphicsDeviceManager graphics;

		float rotation;
		float rotationSpeed = 0.01f;


		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

#if ANDROID
			graphics.IsFullScreen = true;
			graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
			graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			graphics.SupportedOrientations = DisplayOrientation.Portrait;
#endif

			IsMouseVisible = true;
		}

		Bank masterBank;
		Bank masterStringBank;

		EventDescription musicDescription;
		EventDescription sfxDescription;

		EventInstance musicInstance;


		public static Vector2 ScreenSize
		{
			get
			{
#if !ANDROID
				 return new Vector2(
					graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight
				);
#else
				return new Vector2(
					GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
					GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
				);
#endif
			}
		}


		Listener3D l;
		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// All our music files reside in Content directory.
			FMODManager.Init(FMODMode.CoreAndStudio, "Content");
			UIController.Init(GraphicsDevice);
			SceneController.ChangeScene(new DemoSelectorScene());

			/*
				l = new Listener3D();

				// You can load pretty much any popular audio format.
				// I'd recommend .ogg for music, tho.
				sound = CoreSystem.LoadStreamedSound("test.mp3");
				sound.Looping = true;
				//sound.LowPass = 0.1f;
				//sound.Volume = 2;
				//sound.Pitch = 2;
				sound.MinDistance3D = 100;
				sound.MaxDistance3D = 20000;
				//sound.3D
				sound.Mode = FMOD.MODE._3D;
				sound.Position3D = new Vector3(0, 0, 0);

				var channel = sound.Play();

				Console.WriteLine(sound.MaxDistance3D + " ---- " + channel.MinDistance3D);
				Console.WriteLine(channel.Mode);

				/*
				// Add some effects to the sound! :0
				channel.LowPass = 0.5f;
				channel.Pitch = 2f;
				*/

			/*
			// You would rather place the following in LoadContent() - it's here more for readability.
			// Here you load any banks that you're using. This could be a music bank, a SFX bank, etc.
			// The strings bank isn't actually necessary - however if you want to do string lookups, include it.
			masterBank = StudioSystem.LoadBank("Master.bank");
			masterStringBank = StudioSystem.LoadBank("Master.strings.bank");

			// Events are split in the code into descriptions and instances.
			// You may have multiple instances of one event, but the description for it should only be loaded once.
			// Here is why you want the strings bank loaded, btw. So much more intuitive than Guids.
			musicDescription = StudioSystem.GetEvent("event:/Music/Demo");

			// Loading an event description by Guid.
			// FMOD Studio will give you a string like below when you select "Copy Guid". It has to be parsed to be used.
			FMOD.Studio.Util.parseID("{432d05d4-71d0-49bc-81fc-9c0beeaf217c}", out Guid sfxGuid);
			sfxDescription = StudioSystem.GetEvent(sfxGuid);

			// There are three ways to load sample data (any non-streamed sounds):
			// -From a bank. This will keep ALL the bank's data in memory until unloaded.
			// -From an event description. This will just keep the event's necessary data in memory until unloaded.
			// -From an event instance. Same as above, except the data will only be in memory while an instance is.
			// Assess when you need memory loaded and for how long, then choose which method to use properly.

			// The music doesn't need its data pre-loaded, since it'll just play right away, continuously.
			musicInstance = musicDescription.CreateInstance();

			// Sound effects could be played whenever, so you don't want them constantly loading / unloading.
			sfxDescription.LoadSampleData();

			musicInstance.Start();
			//musicInstance.Paused = true;
			//musicInstance.Volume = 0.5f;
			//musicInstance.Pitch = 2f;
			*/
			// If you have any parameters set within FMOD Studio, you can change them within the code.
			//musicInstance.SetParameterValue("Local Parameter", 1);
			//AudioMgr.SetParameterValue("Global Parameter", 0.5f);

			// FMOD Studio has its own instance of the FMOD Core system, so all normal functions will still work.
			// AKA: DON'T LOAD BOTH AT THE SAME TIME!!! I don't know, nor do I want to know, what would happen.
			//var sound = AudioMgr.LoadStreamedSound("test.mp3");
			//sound.Looping = true;
			//var channel = sound.Play();

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.

			Resources.Load(Content);

		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			FMODManager.Unload();
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			UIController.Update();
			SceneController.Update();

			if (
				GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
				|| GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed
				|| Keyboard.GetState().IsKeyDown(Keys.Escape)
			)
			{
				Exit();
			}

			FMODManager.Update();

			rotation += rotationSpeed;
			if (rotation > MathHelper.TwoPi)
			{
				rotation -= MathHelper.TwoPi;
			}

			var mouse = Mouse.GetState().Position.ToVector2() * 100;
			//l.Position3D = new Vector3(mouse.X, mouse.Y, 0);

			base.Update(gameTime);
		}


		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(UIController.Backgroud);


#if !ANDROID
			var scale = 1;
#else
			var scale = Math.Min(ScreenSize.X, ScreenSize.Y) / 2f / (float)Resources.Gato.Width;
#endif

			UIController.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
			UIController.SpriteBatch.Draw(
				Resources.Gato,
				ScreenSize / 2,
				null,
				UIController.Text,
				rotation,
				Vector2.One * Resources.Gato.Width / 2,
				Vector2.One * scale,
				SpriteEffects.None,
				0
			);
			UIController.SpriteBatch.End();

			UIController.Draw();
			SceneController.Draw();

			base.Draw(gameTime);
		}
	}
}
