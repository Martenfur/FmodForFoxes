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
			// All our music files reside in Content directory.
			FMODManager.Init(FMODMode.Core, "Content");
		
			// You can load pretty much any popular audio format.
			// I'd recommend .ogg for music, tho.
			var sound = CoreSystem.LoadStreamedSound("test.mp3");
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
			FMODManager.Unload();
		}

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

			FMODManager.Update();

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
	}
}
