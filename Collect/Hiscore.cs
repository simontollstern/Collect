using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collect {

    //Objekt som lagrar namn, poäng och combo
    //Lagras i en lista av 10 för att utgöra hiscoreslistan
    class Hiscore {
        public SpriteFont font;


        public string user;
        public int score;
        public int combo;
        
        public Hiscore(string user, int score, int combo) {
            this.user = user;
            this.score = score;
            this.combo = combo;
        }

        public void LoadContent(ContentManager Content) {
            font = Content.Load<SpriteFont>("fonts/font");
        }

        public void Draw(SpriteBatch spriteBatch, int posY) {
            spriteBatch.DrawString(font, user, new Vector2(75, posY), Color.White);
            spriteBatch.DrawString(font, "" + score, new Vector2(150, posY), Color.Yellow);
            spriteBatch.DrawString(font, "(" + combo + ")", new Vector2(250, posY), Color.Yellow);
        }
    }
}
