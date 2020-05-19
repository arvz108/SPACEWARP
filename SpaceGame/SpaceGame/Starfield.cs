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
    public class Starfield
    {
        public Texture2D texture; 
        public Vector2 bgPos1, bgPos2; // declare bgpos1&2
        public int speed;

        // Constructor 
        public Starfield() // Background bg5 Starfield
        {
            texture = null;
           
            bgPos1 = new Vector2(0, 0);         // sets the first background position
            bgPos2 = new Vector2(0, -950);      // sets the second background position
            speed = 5; // sets speed of background animation 
        }

        //Load Content 
        public void LoadContent(ContentManager Content) // Import png
        {
            texture = Content.Load<Texture2D>("bg5"); // Call from content import method loading bg5.png
       
            
        }


        //Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bgPos1, Color.White);  // Draw bg1
            spriteBatch.Draw(texture, bgPos2, Color.White);  // Draw bg2
        }

        //Update
        public void Update(GameTime gameTime)
        {
            // Background Speed Control
            bgPos1.Y = bgPos1.Y + speed; // Makes the first background scroll west (-X)
            bgPos2.Y = bgPos2.Y + speed; // Second background scroll west (-X)
             
            //  Repeats bg5.png image using scrolling
            if (bgPos1.Y >= 950)  // if bg5(1) = less than or equal to do statement bellow
            {
                bgPos1.Y = 0;
                bgPos2.Y = -950;
               
            }
           

        }
    }







}
