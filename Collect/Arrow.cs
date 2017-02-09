using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collect
{
    //Objekt som hanterar pilarna
    class Arrow
    {
        Random rnd = new Random();
        List<Texture2D> arrows = new List<Texture2D>();
        public Texture2D arrow;
        public Rectangle rect;
        public int posX = 0;
        public int posY = -100;
        public int speed = 10;

        public Arrow()
        {
            arrow = null;
            posX = rnd.Next(0, 4) * 100;
        }

        public void LoadContent(ContentManager Content)
        {
            arrows.Add(Content.Load<Texture2D>("textures/left"));
            arrows.Add(Content.Load<Texture2D>("textures/up"));
            arrows.Add(Content.Load<Texture2D>("textures/down"));
            arrows.Add(Content.Load<Texture2D>("textures/right"));
        }

        public void Update(GameTime gameTime)
        {
            if (posX == 0)
            {
                arrow = arrows[0];
            }
            if (posX == 100)
            {
                arrow = arrows[1];
            }
            if (posX == 200)
            {
                arrow = arrows[2];
            }
            if (posX == 300)
            {
                arrow = arrows[3];
            }
            posY += speed;
            rect = new Rectangle(posX, posY, 100, 100);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(arrow, rect, Color.White);
        }
    }
}