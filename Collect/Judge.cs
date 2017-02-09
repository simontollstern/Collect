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
    //Objekt som hanterar knapparna och linjerna som avgör om man träffar bra/dåligt eller missar
    class Judge
    {
        public Texture2D left;
        public Texture2D up;
        public Texture2D down;
        public Texture2D right;

        public Texture2D leftD;
        public Texture2D upD;
        public Texture2D downD;
        public Texture2D rightD;

        public Texture2D line;

        public Rectangle hit1Rect;
        public Rectangle hit2Rect;
        public Rectangle hit3Rect;
        public Rectangle hit4Rect;
        public Rectangle beforeRect;
        public Rectangle afterRect;
        public Rectangle missRect;

        public Judge()
        {
            left = null;
            up = null;
            down = null;
            right = null;
            hit1Rect = new Rectangle(0, 508, 100, 34);
            hit2Rect = new Rectangle(100, 508, 100, 34);
            hit3Rect = new Rectangle(200, 508, 100, 34);
            hit4Rect = new Rectangle(300, 508, 100, 34);
            beforeRect = new Rectangle(0, 475, 400, 33);
            afterRect = new Rectangle(0, 542, 400, 33);
            missRect = new Rectangle(0, 658, 400, 1);
        }

        public void LoadContent(ContentManager Content)
        {
            left = Content.Load<Texture2D>("textures/leftReceptor");
            up = Content.Load<Texture2D>("textures/upReceptor");
            down = Content.Load<Texture2D>("textures/downReceptor");
            right = Content.Load<Texture2D>("textures/rightReceptor");

            leftD = Content.Load<Texture2D>("textures/leftReceptorD");
            upD = Content.Load<Texture2D>("textures/upReceptorD");
            downD = Content.Load<Texture2D>("textures/downReceptorD");
            rightD = Content.Load<Texture2D>("textures/rightReceptorD");

            line = Content.Load<Texture2D>("textures/pixel");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (line != null)
            {
                spriteBatch.Draw(line, beforeRect, Color.Black);
                spriteBatch.Draw(line, hit1Rect, Color.Black);
                spriteBatch.Draw(line, hit2Rect, Color.Black);
                spriteBatch.Draw(line, hit3Rect, Color.Black);
                spriteBatch.Draw(line, hit4Rect, Color.Black);
                spriteBatch.Draw(line, afterRect, Color.Black);
                spriteBatch.Draw(line, missRect, Color.Black);
                spriteBatch.Draw(left, new Rectangle(0, 445, 100, 160), Color.White);
                spriteBatch.Draw(up, new Rectangle(100, 445, 100, 160), Color.White);
                spriteBatch.Draw(down, new Rectangle(200, 445, 100, 160), Color.White);
                spriteBatch.Draw(right, new Rectangle(300, 445, 100, 160), Color.White); 
            }
        }
    }
}