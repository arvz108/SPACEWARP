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
    public class Asteroid
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public float rotation;
        public int speed;
        public Rectangle boundingBox; // declares collision 

        public bool isVisible; // declares asteroid isVisibile attribute
        Random random = new Random(); // generates random numbers
        public float randX, randY; // declares random co-ordinates 



        // Constructor 
        public Asteroid(Texture2D newTexture, Vector2 newPosition)
        {
            position = newPosition;
            texture = newTexture;
            speed = 4; // speed of random asteroids towards the west 
            isVisible = true;
            randX = random.Next(0, 1500);
            randY = random.Next(-950,-50);


           
        }

        //Load Content 
        public void LoadContent(ContentManager Content) // Import png
        {
           
        }

        //Update
        public void Update(GameTime gameTime)
        {
            //Set box for collision 
            boundingBox = new Rectangle((int)position.X, (int)position.Y, 90, 89);

             
            origin.X = texture.Width / 2; // Width and Heigth for origin variable 
            origin.Y = texture.Height / 2; // Divides both sprites/ images in half and gives the origin
            
            
            // Movement
            position.Y = position.Y + speed;
            if (position.Y >= 950) // if the asteroids position is less than or  equal to 0 on the X axis then reset to 1800
                position.Y = -50;

            //Rotate Aesteroid
            //float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //rotation += elapsed;
            //float circle = MathHelper.Pi * 2;
            //rotation = rotation % circle;
        }
            //Draw
            public void Draw(SpriteBatch spriteBatch)
            {
                   
                if(isVisible) 
                    spriteBatch.Draw(texture,position,null,Color.White);
          
            }

        }
    }

