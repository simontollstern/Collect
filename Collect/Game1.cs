using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Collect {
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //De states spelet kan befinna sig i
        enum State {
            Menu, Options, Hiscores, Play, End
        }
        State state;

        //Integer som kontrollerar vilken knapp i menyn som är markerad
        int menu = 1;

        //Färger för markerad knapp i menyn
        Color menuColor1 = Color.White;
        Color menuColor2 = Color.White;
        Color menuColor3 = Color.White;
        Color menuColor4 = Color.White;
        Color menuColor5 = Color.White;
        Color menuColor6 = Color.White;
        Color menuColor7 = Color.White;
        Color menuColor8 = Color.White;
        Color menuColor9 = Color.White;
        Color menuColor10 = Color.White;

        //Booleans som kontrollerar input
        bool enterPressed = false;
        bool upPressed = false;
        bool downPressed = false;
        bool rightPressed = false;
        bool leftPressed = false;
        bool dPressed = false;
        bool fPressed = false;
        bool jPressed = false;
        bool kPressed = false;

        //Booleans som ser till att information endast skickas/hämtas en gång
        bool hasRead = false;
        bool hasPrinted = false;
        bool hasSubmitted = false;

        //Objekt av Judge.cs
        Judge judge = new Judge();

        //Lista för pilar och lista där de läggs för att tas bort
        List<Arrow> arrows = new List<Arrow>();
        List<Arrow> remove = new List<Arrow>();

        //Lista för Hiscore-objekt
        List<Hiscore> hiscores = new List<Hiscore>();

        //Lista för alfabetet i chars
        List<string> chars = new List<string>();

        //Typsnitt
        SpriteFont font;
        SpriteFont bigfont;

        //Integers för antalet bra/dåliga träffar
        int hit10count = 0;
        int hit5count = 0;

        //Ljudeffekter
        SoundEffect clickclick;
        SoundEffect click;
        SoundEffect miss;
        SoundEffect pop;

        //Strings för att bestämma användarnamn
        string char1;
        string char2;
        string char3;

        //Integers som håller koll på vilka bokstäver ovanstående strings blir
        int char1nr = 0;
        int char2nr = 0;
        int char3nr = 0;

        //Strings för att bestämma keybinds (TBA)
        string key1 = "D";
        string key2 = "F";
        string key3 = "J";
        string key4 = "K";

        //Integer nödvändiga unders spelets gång:
        int speed = 10; //Pilarnas hastighet
        int lives = 3; //Antal liv
        int combo = 0; //Nuvarande träffar i rad
        int maxCombo = 0; //Maximalt antal träffar i rad
        int hitScore = 0; //Poäng en träff ger innan uträkning hitScore * combo (10 eller 5)
        int totalScore = 0; //Totalpoäng

        //Integers som håller koll på tid
        int arrowTimer = 0;
        int intervalTimer = 0;
        int spawnTime = 500;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 400;
            this.graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("fonts/font");
            bigfont = Content.Load<SpriteFont>("fonts/bigfont");

            clickclick = Content.Load<SoundEffect>("sounds/hit10");
            click = Content.Load<SoundEffect>("sounds/hit5");
            miss = Content.Load<SoundEffect>("sounds/miss");
            pop = Content.Load<SoundEffect>("sounds/pop");
        }

        protected override void UnloadContent() {

        }

        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);

            switch (state) {
                case State.Menu:
                    UpdateMenu(gameTime);
                    break;
                case State.Options:
                    UpdateOptions(gameTime);
                    break;
                case State.Hiscores:
                    UpdateHiscores(gameTime);
                    break;
                case State.Play:
                    UpdatePlay(gameTime);
                    break;
                case State.End:
                    UpdateEnd(gameTime);
                    break;
            }
        }
        void UpdateMenu(GameTime gameTime) {
            KeyboardState kState = Keyboard.GetState();

            if (kState.IsKeyDown(Keys.Up) && upPressed == false) {
                upPressed = true;
                click.Play();
                if (menu == 1) {
                    menu = 3;
                } else {
                    menu--;
                }
            }
            if (kState.IsKeyDown(Keys.Down) && downPressed == false) {
                downPressed = true;
                click.Play();
                if (menu == 3) {
                    menu = 1;
                } else {
                    menu++;
                }
            }
            if (kState.IsKeyDown(Keys.Enter) && enterPressed == false) {
                if (menu == 1) {
                    state = State.Options;
                }
                if (menu == 2) {
                    state = State.Hiscores;
                }
                if (menu == 3) {
                    this.Exit();
                }
                enterPressed = true;
                clickclick.Play();
            }
            if (kState.IsKeyUp(Keys.Enter)) {
                enterPressed = false;
            }
            if (kState.IsKeyUp(Keys.Up)) {
                upPressed = false;
            }
            if (kState.IsKeyUp(Keys.Down)) {
                downPressed = false;
            }

            if (menu == 1) {
                menuColor1 = Color.Teal;

            } else {
                menuColor1 = Color.White;
            }
            if (menu == 2) {
                menuColor2 = Color.Teal;
            } else {
                menuColor2 = Color.White;
            }
            if (menu == 3) {
                menuColor3 = Color.Teal;
            } else {
                menuColor3 = Color.White;
            }
        }
        void UpdateOptions(GameTime gameTime) {
            KeyboardState kState = Keyboard.GetState();

            for (char c = 'A'; c <= 'Z'; c++) {
                chars.Add("" + c);
            }

            char1 = chars[char1nr];
            char2 = chars[char2nr];
            char3 = chars[char3nr];

            if (kState.IsKeyDown(Keys.Up) && upPressed == false) {
                upPressed = true;
                click.Play();
                if (menu == 1) {
                    menu = 10;
                } else {
                    menu--;
                }
            }
            if (kState.IsKeyDown(Keys.Down) && downPressed == false) {
                downPressed = true;
                click.Play();
                if (menu == 10) {
                    menu = 1;
                } else {
                    menu++;
                }
            }
            if (kState.IsKeyDown(Keys.Left) && leftPressed == false) {
                leftPressed = true;
                click.Play();
                if (menu == 1) {
                    if (char1nr == 0) {
                        char1nr = 25;
                    } else {
                        char1nr--;
                    }
                }
                if (menu == 2) {
                    if (char2nr == 0) {
                        char2nr = 25;
                    } else {
                        char2nr--;
                    }
                }
                if (menu == 3) {
                    if (char3nr == 0) {
                        char3nr = 25;
                    } else {
                        char3nr--;
                    }
                }
                if (menu == 8) {
                    if (speed > 2) {
                        speed--;
                    }
                }
            }
            if (kState.IsKeyDown(Keys.Right) && rightPressed == false) {
                rightPressed = true;
                click.Play();
                if (menu == 1) {
                    if (char1nr == 25) {
                        char1nr = 0;
                    } else {
                        char1nr++;
                    }
                }
                if (menu == 2) {
                    if (char2nr == 25) {
                        char2nr = 0;
                    } else {
                        char2nr++;
                    }
                }
                if (menu == 3) {
                    if (char3nr == 25) {
                        char3nr = 0;
                    } else {
                        char3nr++;
                    }
                }
                if (menu == 8) {
                    speed++;
                }
            }
            if (kState.IsKeyDown(Keys.Enter) && enterPressed == false) {
                if (menu == 9) {
                    state = State.Play;
                }
                if (menu == 10) {
                    state = State.Menu;
                }
                menu = 1;
                enterPressed = true;
                clickclick.Play();
            }
            if (kState.IsKeyUp(Keys.Enter)) {
                enterPressed = false;
            }
            if (kState.IsKeyUp(Keys.Up)) {
                upPressed = false;
            }
            if (kState.IsKeyUp(Keys.Down)) {
                downPressed = false;
            }
            if (kState.IsKeyUp(Keys.Left)) {
                leftPressed = false;
            }
            if (kState.IsKeyUp(Keys.Right)) {
                rightPressed = false;
            }

            if (menu == 1) {
                menuColor1 = Color.Teal;

            } else {
                menuColor1 = Color.White;
            }
            if (menu == 2) {
                menuColor2 = Color.Teal;
            } else {
                menuColor2 = Color.White;
            }
            if (menu == 3) {
                menuColor3 = Color.Teal;
            } else {
                menuColor3 = Color.White;
            }
            if (menu == 4) {
                menuColor4 = Color.Teal;

            } else {
                menuColor4 = Color.White;
            }
            if (menu == 5) {
                menuColor5 = Color.Teal;
            } else {
                menuColor5 = Color.White;
            }
            if (menu == 6) {
                menuColor6 = Color.Teal;
            } else {
                menuColor6 = Color.White;
            }
            if (menu == 7) {
                menuColor7 = Color.Teal;

            } else {
                menuColor7 = Color.White;
            }
            if (menu == 8) {
                menuColor8 = Color.Teal;
            } else {
                menuColor8 = Color.White;
            }
            if (menu == 9) {
                menuColor9 = Color.Teal;
            } else {
                menuColor9 = Color.White;
            }
            if (menu == 10) {
                menuColor10 = Color.Teal;
            } else {
                menuColor10 = Color.White;
            }
        }
        void UpdateHiscores(GameTime gameTime) {
            KeyboardState kState = Keyboard.GetState();           

            if (kState.IsKeyDown(Keys.Enter) && enterPressed == false) {
                menu = 1;
                clickclick.Play();
                state = State.Menu;
                enterPressed = true;
            }
            if (kState.IsKeyUp(Keys.Enter)) {
                enterPressed = false;
            }
        }
        void UpdatePlay(GameTime gameTime) {
            judge.LoadContent(Content);

            KeyboardState kState = Keyboard.GetState();

            arrowTimer += gameTime.ElapsedGameTime.Milliseconds;
            intervalTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (arrowTimer > spawnTime) {
                arrows.Add(new Arrow());
                arrowTimer = 0;
            }
            if (intervalTimer > 250) {
                spawnTime -= 1;
                intervalTimer = 0;
            }

            foreach (Arrow block in arrows) {
                block.LoadContent(Content);
                block.Update(gameTime);

                block.speed = speed;

                if (kState.IsKeyDown(Keys.D) && dPressed == false) {
                    if (judge.hit1Rect.Intersects(block.rect) &&
                        judge.beforeRect.Intersects(block.rect) &&
                        judge.afterRect.Intersects(block.rect)) {
                        Hit10(block);
                    }
                    if ((judge.hit1Rect.Intersects(block.rect) &&
                        judge.beforeRect.Intersects(block.rect) &&
                        !judge.afterRect.Intersects(block.rect)) ||
                        (judge.hit1Rect.Intersects(block.rect) &&
                        judge.afterRect.Intersects(block.rect) &&
                        !judge.beforeRect.Intersects(block.rect))) {
                        Hit5(block);
                    }
                    dPressed = true;
                }
                if (kState.IsKeyUp(Keys.D)) {
                    dPressed = false;
                }

                if (kState.IsKeyDown(Keys.F) && fPressed == false) {
                    if (judge.hit2Rect.Intersects(block.rect) &&
                        judge.beforeRect.Intersects(block.rect) &&
                        judge.afterRect.Intersects(block.rect)) {
                        Hit10(block);
                    }
                    if ((judge.hit2Rect.Intersects(block.rect) &&
                        judge.beforeRect.Intersects(block.rect) &&
                        !judge.afterRect.Intersects(block.rect)) ||
                        (judge.hit2Rect.Intersects(block.rect) &&
                        judge.afterRect.Intersects(block.rect) &&
                        !judge.beforeRect.Intersects(block.rect))) {
                        Hit5(block);
                    }
                    fPressed = true;
                }
                if (kState.IsKeyUp(Keys.F)) {
                    fPressed = false;
                }

                if (kState.IsKeyDown(Keys.J) && jPressed == false) {
                    if (judge.hit3Rect.Intersects(block.rect) &&
                        judge.beforeRect.Intersects(block.rect) &&
                        judge.afterRect.Intersects(block.rect)) {
                        Hit10(block);
                    }
                    if ((judge.hit3Rect.Intersects(block.rect) &&
                        judge.beforeRect.Intersects(block.rect) &&
                        !judge.afterRect.Intersects(block.rect)) ||
                        (judge.hit3Rect.Intersects(block.rect) &&
                        judge.afterRect.Intersects(block.rect) &&
                        !judge.beforeRect.Intersects(block.rect))) {
                        Hit5(block);
                    }
                    jPressed = true;
                }
                if (kState.IsKeyUp(Keys.J)) {
                    jPressed = false;
                }

                if (kState.IsKeyDown(Keys.K) && kPressed == false) {
                    if (judge.hit4Rect.Intersects(block.rect) &&
                        judge.beforeRect.Intersects(block.rect) &&
                        judge.afterRect.Intersects(block.rect)) {
                        Hit10(block);
                    }
                    if ((judge.hit4Rect.Intersects(block.rect) &&
                        judge.beforeRect.Intersects(block.rect) &&
                        !judge.afterRect.Intersects(block.rect)) ||
                        (judge.hit4Rect.Intersects(block.rect) &&
                        judge.afterRect.Intersects(block.rect) &&
                        !judge.beforeRect.Intersects(block.rect))) {
                        Hit5(block);
                    }
                    kPressed = true;
                }
                if (kState.IsKeyUp(Keys.K)) {
                    kPressed = false;
                }

                if (judge.missRect.Intersects(block.rect)) {
                    remove.Add(block);
                    lives--;
                    if (combo > maxCombo) {
                        maxCombo = combo;
                    }
                    combo = 0;
                    hitScore = 0;
                    miss.Play();
                }
            }
            foreach (Arrow block in remove) {
                arrows.Remove(block);
            }

            if (lives == 0) {
                state = State.End;
            }

            if (dPressed) {
                judge.left = judge.leftD;
            }
            if (fPressed) {
                judge.up = judge.upD;
            }
            if (jPressed) {
                judge.down = judge.downD;
            }
            if (kPressed) {
                judge.right = judge.rightD;
            }
        }
        void UpdateEnd(GameTime gameTime) {

            KeyboardState kState = Keyboard.GetState();

            if (hasRead == false) {
                using (StreamReader sr = new StreamReader("Content/hiscores.txt")) {
                    hiscores.Clear();
                    for (int i = 0; i < 10; i++) {
                        string readString = sr.ReadLine();
                        string[] splitString = readString.Split(new char[] { ',' });
                        string name = splitString[0];
                        int score = Int32.Parse(splitString[1]);
                        int combo = Int32.Parse(splitString[2]);
                        hiscores.Add(new Hiscore(name, score, combo));
                        hiscores.Sort((x, y) => y.score.CompareTo(x.score));
                    }
                    sr.Close();
                }
                hasRead = true;
            }

            if(hasSubmitted == false) {
                using (StreamWriter sw = new StreamWriter("Content/hiscores.txt", false)) {
                    if (totalScore > hiscores[9].score) {
                        hiscores.Add(new Hiscore(char1 + char2 + char3, totalScore, maxCombo));
                        hiscores.Sort((x, y) => y.score.CompareTo(x.score));
                        hiscores.RemoveAt(10);
                        foreach (Hiscore hiscore in hiscores) {
                            sw.WriteLine(hiscore.user + "," + hiscore.score + "," + hiscore.combo);
                        }
                    }
                    sw.Close();
                }
                hasSubmitted = true;
            }

            if (hasPrinted == false) {
                foreach (Hiscore hiscore in hiscores) {
                    hiscore.LoadContent(Content);
                }
                hasPrinted = true;
            }

            if (kState.IsKeyDown(Keys.Up) && upPressed == false) {
                upPressed = true;
                click.Play();
                if (menu == 1) {
                    menu = 3;
                } else {
                    menu--;
                }
            }
            if (kState.IsKeyDown(Keys.Down) && downPressed == false) {
                downPressed = true;
                click.Play();
                if (menu == 3) {
                    menu = 1;
                } else {
                    menu++;
                }
            }
            if (kState.IsKeyDown(Keys.Enter) && enterPressed == false) {
                if (menu == 1) {
                    Reset();
                    state = State.Play;
                }
                if (menu == 2) {
                    Reset();
                    state = State.Menu;
                }
                if (menu == 3) {
                    this.Exit();
                }
                menu = 1;
                enterPressed = true;
                clickclick.Play();
            }
            if (kState.IsKeyUp(Keys.Enter)) {
                enterPressed = false;
            }
            if (kState.IsKeyUp(Keys.Up)) {
                upPressed = false;
            }
            if (kState.IsKeyUp(Keys.Down)) {
                downPressed = false;
            }

            if (menu == 1) {
                menuColor1 = Color.Teal;
                menuColor2 = Color.White;
                menuColor3 = Color.White;
            }
            if (menu == 2) {
                menuColor1 = Color.White;
                menuColor2 = Color.Teal;
                menuColor3 = Color.White;
            }
            if (menu == 3) {
                menuColor1 = Color.White;
                menuColor2 = Color.White;
                menuColor3 = Color.Teal;
            }
        }

        void Hit10(Arrow arrow) {
            remove.Add(arrow);
            combo++;
            hit10count++;
            hitScore = 10;
            totalScore += combo * hitScore;
            pop.Play();
        }
        void Hit5(Arrow arrow) {
            remove.Add(arrow);
            combo++;
            hit5count++;
            hitScore = 5;
            totalScore += combo * hitScore;
            pop.Play();
        }
        void Reset() {
            enterPressed = true;
            hasPrinted = false;
            hasRead = false;
            hasSubmitted = false;
            lives = 3;
            combo = 0;
            maxCombo = 0;
            hitScore = 0;
            totalScore = 0;
            hit10count = 0;
            hit5count = 0;

            arrowTimer = 0;
            intervalTimer = 0;
            spawnTime = 500;

            foreach (Arrow block in arrows) {
                remove.Add(block);
            }
        }

        protected override void Draw(GameTime gameTime) {
            base.Draw(gameTime);

            switch (state) {
                case State.Menu:
                    DrawMenu(gameTime);
                    break;
                case State.Options:
                    DrawOptions(gameTime);
                    break;
                case State.Hiscores:
                    DrawHiscores(gameTime);
                    break;
                case State.Play:
                    DrawPlay(gameTime);
                    break;
                case State.End:
                    DrawEnd(gameTime);
                    break;
            }
        }
        void DrawMenu(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.DrawString(bigfont, "no sound", new Vector2(75, 50), Color.Teal);
            spriteBatch.DrawString(bigfont, "stepmania", new Vector2(75, 75), Color.Yellow);
            spriteBatch.DrawString(bigfont, "ripoff", new Vector2(75, 100), Color.Red);
            spriteBatch.DrawString(font, "start", new Vector2(75, 450), menuColor1);
            spriteBatch.DrawString(font, "hiscores", new Vector2(75, 475), menuColor2);
            spriteBatch.DrawString(font, "quit", new Vector2(75, 500), menuColor3);
            spriteBatch.End();
        }
        void DrawOptions(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.DrawString(bigfont, "options", new Vector2(75, 50), Color.Teal);
            spriteBatch.DrawString(font, "username:", new Vector2(75, 100), Color.Yellow);

            if (char1 != null && char2 != null && char3 != null) {
                spriteBatch.DrawString(bigfont, char1, new Vector2(200, 95), menuColor1);
                spriteBatch.DrawString(bigfont, char2, new Vector2(225, 95), menuColor2);
                spriteBatch.DrawString(bigfont, char3, new Vector2(250, 95), menuColor3);
            }

            spriteBatch.DrawString(font, "controls:", new Vector2(75, 130), Color.Yellow);
            spriteBatch.DrawString(bigfont, key1, new Vector2(200, 125), menuColor4);
            spriteBatch.DrawString(bigfont, key2, new Vector2(225, 125), menuColor5);
            spriteBatch.DrawString(bigfont, key3, new Vector2(250, 125), menuColor6);
            spriteBatch.DrawString(bigfont, key4, new Vector2(275, 125), menuColor7);
            spriteBatch.DrawString(font, "speed:", new Vector2(75, 160), Color.Yellow);
            spriteBatch.DrawString(bigfont, "" + speed, new Vector2(200, 155), menuColor8);
            spriteBatch.DrawString(bigfont, "click the", new Vector2(75, 250), Color.White);
            spriteBatch.DrawString(bigfont, "arrows!", new Vector2(75, 275), Color.White);
            spriteBatch.DrawString(font, "start", new Vector2(75, 475), menuColor9);
            spriteBatch.DrawString(font, "back", new Vector2(75, 500), menuColor10);
            spriteBatch.End();
        }
        void DrawHiscores(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.DrawString(bigfont, "local hiscores", new Vector2(75, 50), Color.Teal);

            if (hiscores.Count != 0) {
                int posY = 90;

                foreach (Hiscore hiscore in hiscores) {
                    if (hiscore.font != null) {
                        bool increased = false;
                        hiscore.Draw(spriteBatch, posY);
                        if (!increased) {
                            posY += 25;
                            increased = true;
                        }
                        increased = false;
                    }
                }
            }

            spriteBatch.DrawString(font, "hiscores are only loaded if you've", new Vector2(75, 400), Color.White);
            spriteBatch.DrawString(font, "played a round because ???", new Vector2(75, 420), Color.White);

            spriteBatch.DrawString(font, "back", new Vector2(75, 500), Color.Teal);
            spriteBatch.End();
        }
        void DrawPlay(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            judge.Draw(spriteBatch);

            spriteBatch.DrawString(bigfont, "" + totalScore, new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(font, "" + combo, new Vector2(10, 40), Color.White);
            spriteBatch.DrawString(bigfont, "" + lives, new Vector2(370, 10), Color.White);

            if (hitScore == 10) {
                spriteBatch.DrawString(font, "" + hitScore, new Vector2(370, 40), Color.Cyan);
            } else if (hitScore == 5) {
                spriteBatch.DrawString(font, " " + hitScore, new Vector2(370, 40), Color.Lime);
            } else {
                spriteBatch.DrawString(font, " " + hitScore, new Vector2(370, 40), Color.Red);
            }

            foreach (Arrow block in arrows) {
                block.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
        void DrawEnd(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.DrawString(bigfont, "you scored", new Vector2(75, 50), Color.Teal);
            spriteBatch.DrawString(bigfont, "" + totalScore + " (" + maxCombo + ")*", new Vector2(75, 90), Color.Yellow);
            spriteBatch.DrawString(font, "10s:", new Vector2(75, 120), Color.White);
            spriteBatch.DrawString(font, "5s:", new Vector2(175, 120), Color.White);
            spriteBatch.DrawString(font, "*highest combo", new Vector2(75, 550), Color.Yellow);
            spriteBatch.DrawString(font, "" + hit10count, new Vector2(115, 120), Color.Cyan);
            spriteBatch.DrawString(font, "" + hit5count, new Vector2(205, 120), Color.Lime);
            spriteBatch.DrawString(bigfont, "local hiscores", new Vector2(75, 150), Color.Teal);

            if (hiscores.Count != 0) {
                int posY = 190;

                foreach (Hiscore hiscore in hiscores) {
                    if (hiscore.font != null) {
                        bool increased = false;
                        hiscore.Draw(spriteBatch, posY);
                        if (!increased) {
                            posY += 25;
                            increased = true;
                        }
                        increased = false;
                    }
                }
            }

            spriteBatch.DrawString(font, "retry", new Vector2(75, 450), menuColor1);
            spriteBatch.DrawString(font, "main menu", new Vector2(75, 475), menuColor2);
            spriteBatch.DrawString(font, "quit", new Vector2(75, 500), menuColor3);
            spriteBatch.End();
        }
    }
}
