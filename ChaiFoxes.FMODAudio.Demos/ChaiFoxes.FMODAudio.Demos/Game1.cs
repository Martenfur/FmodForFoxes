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
		private static GraphicsDeviceManager _graphics;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

#if ANDROID
			_graphics.IsFullScreen = true;
			_graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
			_graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			_graphics.SupportedOrientations = DisplayOrientation.Portrait;
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
					_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight
				);
#else
				return new Vector2(
					GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
					GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
				);
#endif
			}
		}


		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// NOTE: You HAVE TO init fmod in the Initialize().
			// Otherwise, it may not work on some platforms.
			FMODManager.Init(FMODMode.CoreAndStudio, "Content");
			//FMODManager.Init(FMODMode.Core, "Content"); // Use this if you don't want Studio functionality.

			UIController.Init(GraphicsDevice);
			SceneController.ChangeScene(new DemoSelectorScene());

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
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

			base.Update(gameTime);
		}


		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(UIController.Backgroud);

			UIController.Draw();
			SceneController.Draw();

			base.Draw(gameTime);
		}
	}
}
