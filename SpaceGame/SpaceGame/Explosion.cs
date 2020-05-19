using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace SpaceGame
{
    public class Explosion
    
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public float timer;
        public float interval;
        public int currentFrame, spriteWidth, spriteHeight;
        public Rectangle sourceRect;
        public bool isVisible;

        //Constructor

        public Explosion(Texture2D newTexture , Vector2 newPosition)
    {
        position = newPosition;
        texture = newTexture;
        timer = 0f;
        interval = 10f;
        currentFrame = 1;
        spriteWidth = 128;
        spriteHeight = 128;
        isVisible = true;

    }

        //LoadContent

        public void LoadContent(ContentManager Content)
        { 
        
        
        }


        //Update

        public void Update(GameTime gameTime)
        {

            timer += (float)gameTime.ElapsedGameTime.Milliseconds;

            if (timer > interval)
            {
                currentFrame++;
                timer = 0f;
            }

            if (currentFrame == 17) // Makes frame 17 invisible, making end of explosion finish.
            
            {
                isVisible = false;
                currentFrame = 0; // reset frame to 0
            }

            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            origin = new Vector2(sourceRect.Width /2 , sourceRect.Height /2);

        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible == true)
                spriteBatch.Draw(texture, position, sourceRect, Color.White, 0f, origin,1.0f, SpriteEffects.None, 0);
        }

    }
}
