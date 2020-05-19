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
    public class Enemy
    {
        public Rectangle boundingBox;
        public Texture2D texture, bulletTexture;
        public Vector2 position;
        public int health, speed, bulletDelay, currentDifficultyLevel;
        public bool isVisible;
        public List<Bullet> bulletList;

    //Constructor
        public Enemy(Texture2D newTexture, Vector2 newPosition, Texture2D newBulletTexture)
        {
            bulletList = new List<Bullet>();
            texture = newTexture;
            bulletTexture = newBulletTexture;
            health = 5;
            position = newPosition;
            currentDifficultyLevel = 1;
            bulletDelay = 100;
            speed = 5;
            isVisible = true;

        }

            //Update
        public void Update(GameTime gameTime)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            //Update player movement
            position.Y += speed;

            //Move enemy back to original position if he leaves boundaries
            if (position.Y >= 950)
                position.Y = 0; 
            
            EnemyShoot();
            UpdateBullets();
        }

                    //Draw
                public void Draw(SpriteBatch spriteBatch)
                {
                    //Draw enemy ship
                    spriteBatch.Draw(texture, position, Color.White);

                    //Draw enemy bullets
                    foreach(Bullet b in bulletList)
                    {
                        b.Draw(spriteBatch);
                    }
                }

                        //Update bullets

                public void UpdateBullets()
                {
                    foreach (Bullet b in bulletList) // each bullet update the movement once the bullet has hit the end of the screen destroy the bullet.
                    {
                        b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);// Bounding box for bullets

                        b.position.Y = b.position.Y + b.speed; // bullet position update and adds to the speed using the - to go along the positive Y axis

                        // if bullet hits the end of the screen then make the bullet visability false removing it from the bullet list
                        if (b.position.Y >= 950)
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
                public void EnemyShoot() // shoot function for enemy
                { 
                    if(bulletDelay >= 0)
                        bulletDelay--;

                    if (bulletDelay <= 0)
                    {
                        Bullet newBullet = new Bullet(bulletTexture);
                        newBullet.position = new Vector2(position.X + texture.Width / 2 - newBullet.texture.Width /2, position.Y + 30);

                        newBullet.isVisible = true;

                        //Reset bullet delay
                        if (bulletList.Count() < 10)
                            bulletList.Add(newBullet);

                        if (bulletDelay == 0)
                            bulletDelay = 100;

                    }

                }
            
    }

}
