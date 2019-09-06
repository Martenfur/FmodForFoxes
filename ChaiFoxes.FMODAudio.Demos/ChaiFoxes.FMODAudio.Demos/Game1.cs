using System;
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
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Texture2D gato;

        Bank masterBank;
        Bank stringsBank;
        EventDescription sfxDescription;

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
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// All our files reside in the Content directory.
			AudioMgr.Init("Content");

            // An example of the low level API can be found at the bottom of this class
            // It all works identically to how it used to, so don't worry about your legacy code.
            // PlaySoundExample();
            /*
            // Loading in any necessary .bank files as exported from FMOD Studio.
            masterBank = AudioMgr.LoadBank("Master.bank");
            // The Strings bank allows for string lookup of event paths, i.e. eventDescription.Path
            // This is functionally identical to any other bank, but never contains sound data.
            stringsBank = AudioMgr.LoadBank("Master.strings.bank");

            // Once a bank is loaded, you can retrieve any audio descriptions they contain.
            // Creating an instance of an event will load all sample data on the spot -
            // this can also be done beforehand with description.LoadSampleData();
            var musicDescription = AudioMgr.GetEvent("event:/Music/Demo");
            var musicInstance = musicDescription.CreateInstance();
            musicInstance.Start();
            //musicInstance.SetParameterValue("Low Pass", 1);
            //musicInstance.Volume = 2;
            //musicInstance.Pitch = 2;

            sfxDescription = AudioMgr.GetEvent("event:/Sfx/Sound");
            sfxDescription.LoadSampleData();

            /*
            VCAs allow you to group buses across a variety of sources, for GLOBAL VOLUME CONTROL!!!
            Think of them like global buses that any other bus can be routed to.
            Admittedly, in implementation they're not very exciting...

            var exampleVCA = AudioMgr.GetVCA("vca:/VCA Example");
            exampleVCA.Volume = 2;
            */

            base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			gato = Content.Load<Texture2D>("gato");
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
            /*masterBank.Unload();
            stringsBank.Unload();*/

            AudioMgr.Unload();
		}

        private bool buttonPressed;

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if (
				GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
				|| GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed
				|| Keyboard.GetState().IsKeyDown(Keys.Escape)
			)
			{
				Exit();
			}

            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                /*if(!buttonPressed)
                {
                    // Because LoadSampleData() was called earlier, these wil play instantly without waiting to load.
                    EventInstance sfxInstance = sfxDescription.CreateInstance();
                    sfxInstance.Start();
                    // Queue up the event instance to be released once it stops playing.
                    sfxInstance.Release();
                }
                buttonPressed = true;*/
            }
            else buttonPressed = false;


            AudioMgr.Update();

			rotation += rotationSpeed;
			if (rotation > MathHelper.TwoPi)
			{
				rotation -= MathHelper.TwoPi;
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(new Color(187, 163, 255));

			#if !ANDROID
				var screenSize = new Vector2(
					graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight
				) / 2;
			#else
				var screenSize = new Vector2(
					GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
					GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
				) / 2;
			#endif

			var scale = Math.Min(screenSize.X, screenSize.Y) / (float)gato.Width;

			spriteBatch.Begin();
			spriteBatch.Draw(
				gato,
				screenSize,
				null,
				Color.White,
				rotation,
				Vector2.One * gato.Width / 2,
				Vector2.One * scale,
				SpriteEffects.None,
				0
			);
			spriteBatch.End();

			base.Draw(gameTime);
		}

        /// <summary>
        /// An example of how to play a sound using the low level FMOD API.
        /// It all works like it used to ;)
        /// </summary>
        protected void PlaySoundExample()
        {
            // You can load pretty much any popular audio format.
            // I'd recommend .ogg for music, tho.
            var sound = AudioMgr.LoadStreamedSound("test.mp3");
            sound.Looping = true;
            //sound.LowPass = 0.1f;
            //sound.Volume = 2;
            //sound.Pitch = 2;

            var channel = sound.Play();

            /*
			// Add some effects to the sound! :0
			channel.LowPass = 0.5f;
			channel.Pitch = 2f;
			*/
        }
    }
}
