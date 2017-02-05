using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Collect
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum State
        {
            Menu, Options, Play, End
        }
        State state;
        bool keyPressed = false;

        bool dPressed = false;
        bool fPressed = false;
        bool jPressed = false;
        bool kPressed = false;

        List<Block> blocks = new List<Block>();
        List<Block> remove = new List<Block>();

        Texture2D line;

        SpriteFont font;

        SoundEffect hit10;
        SoundEffect hit5;
        SoundEffect miss;

        Rectangle hit1Rect;
        Rectangle hit2Rect;
        Rectangle hit3Rect;
        Rectangle hit4Rect;

        Rectangle beforeRect;
        Rectangle afterRect;
        Rectangle missRect;

        Color hit1Color = Color.Red;
        Color hit2Color = Color.Red;
        Color hit3Color = Color.Red;
        Color hit4Color = Color.Red;

        Color tempcolor = Color.Wheat;
        Rectangle temprect;

        int rectPosX = 0;
        int hit1PosX = 0;
        int hit2PosX = 100;
        int hit3PosX = 200;
        int hit4PosX = 300;

        int beforePosY = 450;
        int hitPosY = 460;
        int afterPosY = 470;
        int missPosY = 500;

        int missCount = 0;
        int combo = 0;
        int hitScore = 0;
        int totalScore = 0;

        int timer = 0;
        int spawnTime = 500;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 400;
            this.graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            line = Content.Load<Texture2D>("pixel");
            font = Content.Load<SpriteFont>("Score");

            hit10 = Content.Load<SoundEffect>("hit10");
            hit5 = Content.Load<SoundEffect>("hit5");
            miss = Content.Load<SoundEffect>("miss");
            
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            switch (state)
            {
                case State.Menu:
                    UpdateMenu(gameTime);
                    break;
                case State.Options:
                    UpdateOptions(gameTime);
                    break;
                case State.Play:
                    UpdatePlay(gameTime);
                    break;
                case State.End:
                    UpdateEnd(gameTime);
                    break;
            }
        }
        void UpdateMenu(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Enter) && keyPressed == false)
            {
                keyPressed = true;
                state = State.Options;
            }
            if (kState.IsKeyUp(Keys.Enter))
            {
                keyPressed = false;
            }
        }
        void UpdateOptions(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Enter) && keyPressed == false)
            {
                keyPressed = true;
                state = State.Play;
            }
            if (kState.IsKeyUp(Keys.Enter))
            {
                keyPressed = false;
            }
        }
        void UpdatePlay(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();

            hit1Rect = new Rectangle(hit1PosX, hitPosY, 100, 10);
            hit2Rect = new Rectangle(hit2PosX, hitPosY, 100, 10);
            hit3Rect = new Rectangle(hit3PosX, hitPosY, 100, 10);
            hit4Rect = new Rectangle(hit4PosX, hitPosY, 100, 10);

            beforeRect = new Rectangle(rectPosX, beforePosY, GraphicsDevice.Viewport.Width, 10);
            afterRect = new Rectangle(rectPosX, afterPosY, GraphicsDevice.Viewport.Width, 10);
            missRect = new Rectangle(rectPosX, missPosY, GraphicsDevice.Viewport.Width, 1);

            temprect = new Rectangle(150, 550, 100, 50);

            timer += gameTime.ElapsedGameTime.Milliseconds;

            if (timer > spawnTime)
            {
                blocks.Add(new Block());
                timer = 0;
                spawnTime -= 2;
            }
           
            foreach(Block block in blocks)
            {
                block.LoadContent(Content);
                block.Update(gameTime);

                if (kState.IsKeyDown(Keys.D) && dPressed == false)
                {
                    if (hit1Rect.Intersects(block.blockRect) && beforeRect.Intersects(block.blockRect) && afterRect.Intersects(block.blockRect))
                    {
                        remove.Add(block);
                        tempcolor = Color.Blue;
                        combo++;
                        hitScore = 10;
                        totalScore += combo * hitScore;
                        hit10.Play();
                    }
                    if ((hit1Rect.Intersects(block.blockRect) && beforeRect.Intersects(block.blockRect) && !afterRect.Intersects(block.blockRect)) || (hit1Rect.Intersects(block.blockRect) && afterRect.Intersects(block.blockRect) && !beforeRect.Intersects(block.blockRect)))
                    {
                        remove.Add(block);
                        tempcolor = Color.Lime;
                        combo++;
                        hitScore = 5;
                        totalScore += combo * hitScore;
                        hit5.Play();
                    }
                    hit1Color = Color.Yellow;
                    dPressed = true;
                }
                if (kState.IsKeyUp(Keys.D)){
                    hit1Color = Color.Red;
                    dPressed = false;
                }

                if (kState.IsKeyDown(Keys.F) && fPressed == false)
                {
                    if (hit2Rect.Intersects(block.blockRect) && beforeRect.Intersects(block.blockRect) && afterRect.Intersects(block.blockRect))
                    {
                        remove.Add(block);
                        tempcolor = Color.Blue;
                        combo++;
                        hitScore = 10;
                        totalScore += combo * hitScore;
                        hit10.Play();
                    }
                    if ((hit2Rect.Intersects(block.blockRect) && beforeRect.Intersects(block.blockRect) && !afterRect.Intersects(block.blockRect)) || (hit2Rect.Intersects(block.blockRect) && afterRect.Intersects(block.blockRect) && !beforeRect.Intersects(block.blockRect)))
                    {
                        remove.Add(block);
                        tempcolor = Color.Lime;
                        combo++;
                        hitScore = 5;
                        totalScore += combo * hitScore;
                        hit5.Play();
                    }
                    hit2Color = Color.Yellow;
                    fPressed = true;
                }
                if (kState.IsKeyUp(Keys.F))
                {
                    hit2Color = Color.Red;
                    fPressed = false;
                }

                if (kState.IsKeyDown(Keys.J) && jPressed == false)
                {
                    if (hit3Rect.Intersects(block.blockRect) && beforeRect.Intersects(block.blockRect) && afterRect.Intersects(block.blockRect))
                    {
                        remove.Add(block);
                        tempcolor = Color.Blue;
                        combo++;
                        hitScore = 10;
                        totalScore += combo * hitScore;
                        hit10.Play();
                    }
                    if ((hit3Rect.Intersects(block.blockRect) && beforeRect.Intersects(block.blockRect) && !afterRect.Intersects(block.blockRect)) || (hit3Rect.Intersects(block.blockRect) && afterRect.Intersects(block.blockRect) && !beforeRect.Intersects(block.blockRect)))
                    {
                        remove.Add(block);
                        tempcolor = Color.Lime;
                        combo++;
                        hitScore = 5;
                        totalScore += combo * hitScore;
                        hit5.Play();
                    }
                    hit3Color = Color.Yellow;
                    jPressed = true;
                }
                if (kState.IsKeyUp(Keys.J))
                {
                    hit3Color = Color.Red;
                    jPressed = false;
                }

                if (kState.IsKeyDown(Keys.K) && kPressed == false)
                {
                    if (hit4Rect.Intersects(block.blockRect) && beforeRect.Intersects(block.blockRect) && afterRect.Intersects(block.blockRect))
                    {
                        remove.Add(block);
                        tempcolor = Color.Blue;
                        combo++;
                        hitScore = 10;
                        totalScore += combo * hitScore;
                        hit10.Play();
                    }
                    if ((hit4Rect.Intersects(block.blockRect) && beforeRect.Intersects(block.blockRect) && !afterRect.Intersects(block.blockRect)) || (hit4Rect.Intersects(block.blockRect) && afterRect.Intersects(block.blockRect) && !beforeRect.Intersects(block.blockRect)))
                    {
                        remove.Add(block);
                        tempcolor = Color.Lime;
                        combo++;
                        hitScore = 5;
                        totalScore += combo * hitScore;
                        hit5.Play();
                    }
                    hit4Color = Color.Yellow;
                    kPressed = true;
                }
                if (kState.IsKeyUp(Keys.K))
                {
                    hit4Color = Color.Red;
                    kPressed = false;
                }

                if (missRect.Intersects(block.blockRect))
                {
                    remove.Add(block);
                    missCount++;
                    tempcolor = Color.Red;
                    combo = 0;
                    miss.Play();
                }
            }
            foreach(Block block in remove)
            {
                blocks.Remove(block);
            }

            if (missCount == 3)
            {
                state = State.End;
            }
        }
        void UpdateEnd(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Enter) && keyPressed == false)
            {
                keyPressed = true;
                missCount = 0;
                combo = 0;
                hitScore = 0;
                totalScore = 0;

                timer = 0;
                spawnTime = 500;

                foreach (Block block in blocks)
                {
                    remove.Add(block);
                }
                state = State.Play;
            }
            if (kState.IsKeyUp(Keys.Enter))
            {
                keyPressed = false;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            switch (state)
            {
                case State.Menu:
                    DrawMenu(gameTime);
                    break;
                case State.Options:
                    DrawOptions(gameTime);
                    break;
                case State.Play:
                    DrawPlay(gameTime);
                    break;
                case State.End:
                    DrawEnd(gameTime);
                    break;
            }
        }
        void DrawMenu(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "no sound", new Vector2(100, 200), Color.Teal);
            spriteBatch.DrawString(font, "stepmania", new Vector2(100, 215), Color.Yellow);
            spriteBatch.DrawString(font, "ripoff", new Vector2(100, 230), Color.Red);
            spriteBatch.DrawString(font, "press enter to start", new Vector2(100, 400), Color.White);
            spriteBatch.End();
        }
        void DrawOptions(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "options", new Vector2(100, 200), Color.Teal);
            spriteBatch.DrawString(font, "nothing here yet", new Vector2(100, 300), Color.Yellow);
            spriteBatch.DrawString(font, "play with D,F,J,K btw", new Vector2(100, 350), Color.Red);
            spriteBatch.DrawString(font, "press enter", new Vector2(100, 400), Color.White);
            spriteBatch.End();
        }
        void DrawPlay(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(line, beforeRect, Color.Black);
            spriteBatch.Draw(line, hit1Rect, hit1Color);
            spriteBatch.Draw(line, hit2Rect, hit2Color);
            spriteBatch.Draw(line, hit3Rect, hit3Color);
            spriteBatch.Draw(line, hit4Rect, hit4Color);
            spriteBatch.Draw(line, afterRect, Color.Black);
            spriteBatch.Draw(line, missRect, Color.Black);
            spriteBatch.DrawString(font, "" + totalScore, new Vector2(100, 550), Color.White);
            spriteBatch.DrawString(font, "" + missCount, new Vector2(300, 550), Color.White);
            spriteBatch.DrawString(font, "" + combo, new Vector2(200, 520), Color.White);

            spriteBatch.Draw(line, temprect, tempcolor);

            foreach(Block block in blocks)
            {
                block.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
        void DrawEnd(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "leaderboard", new Vector2(100, 200), Color.Teal);
            spriteBatch.DrawString(font, "you scored: "+totalScore, new Vector2(100, 300), Color.Yellow);
            spriteBatch.DrawString(font, "press enter to retry", new Vector2(100, 400), Color.White);
            spriteBatch.End();
        }
    }
}
