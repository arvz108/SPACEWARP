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
    public class Bullet //Bullet Class
    {
        public Rectangle boundingBox; //BOUNDRY BOX
        public Texture2D texture; 
        public Vector2 origin; //ORIGIN OF BULLET OBJECT
        public Vector2 position;  
        public bool invisible; 
        public float speed;
        public bool isVisible;
   

    //Constructor

        public Bullet(Texture2D newTexture)
        {
            //BULLET ATTRIBUTES
            speed = 25;
            texture = newTexture; 
            isVisible = false;
            

        }
    
        //Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);

        }

       
    }


}
