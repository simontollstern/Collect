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
    class Block
    {
        Random rnd = new Random();
        public Texture2D block;
        public Rectangle blockRect;
        public int blockPosX = 0;
        public int blockPosY = -10;
        public int posID = 0;
        public int speed = 5;
        Color customColor;

        public Block()
        {
            block = null;
            blockPosX = rnd.Next(0, 4) * 100;
            if(blockPosX == 0)
            {
                posID = 1;
                customColor = Color.Green;
            }
            if (blockPosX == 100)
            {
                posID = 2;
                customColor = Color.LimeGreen;
            }
            if (blockPosX == 200)
            {
                posID = 3;
                customColor = Color.LimeGreen;
            }
            if (blockPosX == 300)
            {
                posID = 4;
                customColor = Color.Green;
            }
        }

        public void LoadContent(ContentManager Content)
        {
            block = Content.Load<Texture2D>("pixel");
        }

        public void Update(GameTime gameTime)
        {
            blockPosY += speed;
            blockRect = new Rectangle(blockPosX, blockPosY, 100, 20);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(block, blockRect, customColor);
        }
    }
}