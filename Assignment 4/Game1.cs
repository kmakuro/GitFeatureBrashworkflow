using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace Assignment_4
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Texture2D ballTexture;
        Texture2D charTexture;

        Texture2D ballTexture;
        Texture2D charTexture;
        Vector2 charPosition = new Vector2(0, 250);
        Vector2[] ballPosition = new Vector2[4];
        bool personHit;
        int[] ballColor = new int[4];
        Random rand = new Random();

        int direction = 0;

        int frame;
        int totalFrame;
        int framePerSec;
        float timePerFrame;
        float totalElapsed;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            charTexture = Content.Load<Texture2D>("Char01");
            ballTexture = Content.Load<Texture2D>("ball");
            frame = 0;
            totalFrame = 4;
            framePerSec = 8;
            timePerFrame = (float)1 / framePerSec;
            totalElapsed = 0;

            charTexture = Content.Load<Texture2D>("Char01");
            ballTexture = Content.Load<Texture2D>("ball");

            for (int i = 0; i < 4; i++)
            {
                ballPosition[i].X = rand.Next(graphics.GraphicsDevice.Viewport.Width - ballTexture.Width / 6);
                ballPosition[i].Y = rand.Next(graphics.GraphicsDevice.Viewport.Height - ballTexture.Height);
                ballColor[i] = rand.Next(6);
            }
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Left))
            {
                charPosition.X = charPosition.X - 2;
                direction = 1;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
                charPosition.X = charPosition.X + 2;
                direction = 2;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (keyboard.IsKeyDown(Keys.Up))
            {
                charPosition.Y = charPosition.Y - 2;
                direction = 3;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                charPosition.Y = charPosition.Y + 2;
                direction = 0;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            Rectangle charRectagle = new Rectangle((int)charPosition.X, (int)charPosition.Y, 32, 48);


            for (int i = 0; i < 4; i++)
            {
                Rectangle blockRectagle = new Rectangle((int)ballPosition[i].X, (int)ballPosition[i].Y, 24, 24);
                if (charRectagle.Intersects(blockRectagle) == true)
                {
                    personHit = true;
                    ballPosition[i].X = rand.Next(graphics.GraphicsDevice.Viewport.Width - ballTexture.Width / 6);
                    ballPosition[i].Y = rand.Next(graphics.GraphicsDevice.Viewport.Height - ballTexture.Height);
                    ballColor[i] = rand.Next(6);
                }
                if (charRectagle.Intersects(blockRectagle) == false)
                {
                    personHit = false;
                }
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice device = graphics.GraphicsDevice;
            if (personHit == true)
            {
                device.Clear(Color.Red);
            }
            else
            {
                device.Clear(Color.CornflowerBlue);
            }
            spriteBatch.Begin();
            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(ballTexture, ballPosition[i], new Rectangle(24 * ballColor[i], 0, 24, 24), Color.White);
            }


            spriteBatch.Draw(charTexture, charPosition, new Rectangle(32 * frame, 48 * direction, 32, 48), Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        void UpdateFrame(float elapsed)
        {
            totalElapsed += elapsed;
            if (totalElapsed > timePerFrame)
            {
                frame = (frame + 1) % totalFrame;
                totalElapsed -= timePerFrame;
            }
        }
    }
}