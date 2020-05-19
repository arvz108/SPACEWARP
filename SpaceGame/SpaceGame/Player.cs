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
    //Main
    public class Player
    {

        public Texture2D texture, bulletTexture, healthTexture;
        public Vector2 position, healthBarPosition;
        public int speed, health;
        public float bulletDelay; //Shoots bullets at a certain set time and removed from the list
        public List<Bullet> bulletList;
        SoundManager sm = new SoundManager();


        //Collision Variables bellow
        public bool isColliding;
        public Rectangle boundingBox, healthRectangle;

        //Constructor sets variables to null
        public Player()
        {
            
            texture = null;
            position = new Vector2(800, 600); // Players Starting Position
            speed = 15; //Speed of player
            isColliding = false; // colliding settings
            bulletDelay = 1;
            bulletList = new List<Bullet>();
            health = 300; // Width for health rectangle by setting 200 instead of 100.
            healthBarPosition = new Vector2(50, 50); // health bar position

        }

        // Load Content
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Spaceship1");
            bulletTexture = Content.Load<Texture2D>("playerbullet");
            healthTexture = Content.Load<Texture2D>("healthbar");
            sm.LoadContent(Content);
        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(healthTexture, healthRectangle, Color.White);
            foreach (Bullet b in bulletList) // for every bullet in the list draw the bullet
                b.Draw(spriteBatch);
        }


        //Update
        public void Update(GameTime gameTime)
        {
            KeyboardState keystate = Keyboard.GetState(); // Getting Keyboard State Enabled
            //Fire Bullets
           
            boundingBox = new Rectangle((int)position.X,(int)position.Y,texture.Width,texture.Height); //Spaceship bounding box

            healthRectangle = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y,health,25); //Spaceship health bounding box

            
            if (keystate.IsKeyDown(Keys.Space))
            {
                Shoot();
            }

            UpdateBullets();

            //Ship Controls
            if (keystate.IsKeyDown(Keys.Up))
                position.Y = position.Y - speed; //Control Ship Up
            if (keystate.IsKeyDown(Keys.Down))
                position.Y = position.Y + speed;    //Control Ship Down
            if (keystate.IsKeyDown(Keys.Left))
                position.X = position.X - speed; ////Control Ship Left
            if (keystate.IsKeyDown(Keys.Right))
                position.X = position.X + speed; ////Control Ship Right


            // Optional Direction Controls Bellow

            if (keystate.IsKeyDown(Keys.W))
                position.Y = position.Y - speed; //Control Ship Up
            if (keystate.IsKeyDown(Keys.S))
                position.Y = position.Y + speed; //Control Ship Down
            if (keystate.IsKeyDown(Keys.A))
                position.X = position.X - speed; //Control Ship Left
            if (keystate.IsKeyDown(Keys.D))
                position.X = position.X + speed; //Control Ship Right

            //Player within boundaries(Rules)

            if (position.X <= 0) position.X = 0; // X boundry
            if (position.X >= 1800 - texture.Width) position.X = 1800 - texture.Width;

            if (position.Y <= 0) position.Y = 0; // Y boundry
            if (position.Y >= 950 - texture.Width) position.Y = 950 - texture.Width;
        }

        //Shoot Method
        public void Shoot()
        {
            //Shoot only once the bullet delay has reset
            if (bulletDelay >= 0)
                bulletDelay--;
            if (bulletDelay <= 0)
            {
                sm.playerShoot.Play();
                Bullet newBullet = new Bullet(bulletTexture);
                newBullet.position = new Vector2(position.X + 53 + newBullet.texture.Width / 2, position.Y + 30);

                newBullet.isVisible = true;


                if (bulletList.Count() < 800)
                    bulletList.Add(newBullet); // Allows 800 bullets on the screen at a time until the bullet delay is reset.
            }
            if (bulletDelay == 0)
                bulletDelay = 10;

        }

        public void UpdateBullets()
        {
            foreach (Bullet b in bulletList) // each bullet update the movement once the bullet has hit the end of the screen destroy the bullet.
            {
                b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);// Bounding box for bullets

                b.position.Y = b.position.Y - b.speed; // bullet position update and adds to the speed using the - to go along the positive Y axis

                // if bullet hits the end of the screen then make the bullet visability false removing it from the bullet list
                if (b.position.Y <= 0)
                    b.isVisible = false;

            }
            // Add 1 through the bullList by itterator i++ check if any are not visible, if they are not remove the bullet from the bullet list
            for (int i = 0; i < bulletList.Count; i++)
            {
                if (!bulletList[i].isVisible)
                {
                    bulletList.RemoveAt(i);
                    i++;
                }

            }

        }


    }

}


