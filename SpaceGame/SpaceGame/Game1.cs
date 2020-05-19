//Arvin Teymouri
//Space Shooter Called SpaceWarp
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceGame
{
    //Main Method
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public enum State
        { 
            Menu,
            Playing,
            Gameover
        }
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random = new Random(); //Random Instance
        Player p = new Player(); // Player 1
        Starfield bg5 = new Starfield(); // Background Bg5
        HUD hud = new HUD();
        SoundManager sm = new SoundManager(); 
        public int enemyBulletDamage;
        public Texture2D menuImage;
        public Texture2D gameoverImage;

        //  Lists of Classes holding list variables
        List<Asteroid> asteroidList = new List<Asteroid>();
        List<Enemy> enemyList = new List<Enemy>();
        List<Explosion> explosionList = new List<Explosion>();

        //Set first state
        State gamestate = State.Menu;

        // Constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false; // can be set to true to enable fullscreen
            graphics.PreferredBackBufferWidth = 1800; // Game window width
            graphics.PreferredBackBufferHeight = 950; //Game window height
            this.Window.Title = "Space Warp"; // Title name for the window
            Content.RootDirectory = "Content";
            enemyBulletDamage = 10;
            menuImage = null;
            gameoverImage = null;
        }
        //Init
        protected override void Initialize()
        {

            base.Initialize();
        }
        //Load Content
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            p.LoadContent(Content);
            bg5.LoadContent(Content);
            hud.LoadContent(Content);
            sm.LoadContent(Content);
            menuImage = Content.Load<Texture2D>("menustart");
            gameoverImage = Content.Load<Texture2D>("gameover");

        }
        //Unload content
        protected override void UnloadContent()
        {

        }
        //Update
        protected override void Update(GameTime gameTime)
        {
            //Exit Game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            //UPDATING PLAYING STATES
            switch (gamestate)
            { 
                case State.Playing:
                    {

                        bg5.speed = 5;
            foreach (Enemy e in enemyList) //Collision enemy to player
            {

                if (e.boundingBox.Intersects(p.boundingBox))
                {
                    p.health -= 40;
                    e.isVisible = false;
                    hud.playerScore -= 100;
                }
                //check enemy bullet collision against player
                for (int i = 0; i < e.bulletList.Count; i++)
                {
                    if (p.boundingBox.Intersects(e.bulletList[i].boundingBox))
                    {
                        p.health -= enemyBulletDamage;
                        e.bulletList[i].isVisible = false;
                        hud.playerScore -= 100;

                    }
                }
                //check bullet collision to the enemy ship
                for (int i = 0; i < p.bulletList.Count; i++)
                {
                    if (p.bulletList[i].boundingBox.Intersects(e.boundingBox))
                    {
                        explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(e.position.X, e.position.Y)));
                        sm.explodeSound.Play();
                        p.bulletList[i].isVisible = false;
                        e.isVisible = false;
                        hud.playerScore += 100;
                    }
                }
                e.Update(gameTime);
            }

            foreach (Explosion ex in explosionList)
            {
                ex.Update(gameTime);
            }


            foreach (Asteroid a in asteroidList)
            {

                if (a.boundingBox.Intersects(p.boundingBox)) //Check for collisions
                {
                    p.health -= 20; // if collision occurs, -20 from player healthbar
                    a.isVisible = false; // removes the asteroid from the list              
                    hud.playerScore -= 50;
                }

                a.Update(gameTime); // For each asteroid in the asteroid list , update.
                // if any asteroid collide with the bullets, set both bullet and asteroid to false, destroying them both.

                for (int i = 0; i < p.bulletList.Count; i++)
                {
                    if (a.boundingBox.Intersects(p.bulletList[i].boundingBox))
                    {
                        explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(a.position.X, a.position.Y)));
                        sm.explodeSound.Play();
                        a.isVisible = false;
                        p.bulletList.ElementAt(i).isVisible = false;
                        hud.playerScore += 50;
                    }
                }
            }
                        //If player health is 0 go to gameover
            if (p.health <= 0)
                gamestate = State.Gameover;
                        p.Update(gameTime);
                        bg5.Update(gameTime);
                        ManageExplosions();
                        LoadAsteroids();
                        LoadEnemies();
                        //hud.Update();
                        break;
 }
                    //UPDATING MENU STATE
                case State.Menu:
                    {
                        // Get keystate
                        KeyboardState keyState = Keyboard.GetState();
                        if (keyState.IsKeyDown(Keys.Space))
                        {
                            gamestate = State.Playing;
                            MediaPlayer.Play(sm.bgMusic);

                        }
                        bg5.Update(gameTime);
                        bg5.speed = 1;

                        break;
                    }

                    //UPDATE GAMESTATE
                case State.Gameover:
                    {
                        KeyboardState keyState = Keyboard.GetState();
                        if (keyState.IsKeyDown(Keys.Escape))
                        {
                            enemyList.Clear();
                            asteroidList.Clear();
                            gamestate = State.Menu;
                            MediaPlayer.Stop();
                            p.health = 200;
                            hud.playerScore = 0;
                            
                        }
                        break;
                    }
            }
           
                        base.Update(gameTime);
        }
        //Draw
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(); // begins drawing sprites
           
            //Draw Game State
            switch (gamestate)
            {
                case State.Playing:
                    { 
            bg5.Draw(spriteBatch); // Background is loaded before the Player, order is sequential
            p.Draw(spriteBatch);// Draw Player Layer after Starfield Class 
            hud.Draw(spriteBatch);//Draw HUD

            foreach (Enemy ex in enemyList)
            {
                ex.Draw(spriteBatch);
            }

            foreach (Asteroid a in asteroidList)
            {
                a.Draw(spriteBatch);
            }

            foreach (Enemy e in enemyList)
            {
                e.Draw(spriteBatch);
            }

            foreach (Explosion ex in explosionList)
            {
                ex.Draw(spriteBatch);
            }            
                        break;
         }
                
                
                //Draw Menu State
                case State.Menu:
                    {
                        bg5.Draw(spriteBatch);
                        spriteBatch.Draw(menuImage, new Vector2(0, 0), Color.White);
                        break;
                    }
                //Draw GameOver State
                case State.Gameover:
                    {
                        spriteBatch.Draw(gameoverImage, new Vector2(0, 0), Color.White);
                        spriteBatch.DrawString(hud.playerScoreFont, "HIGH SCORE:" + hud.playerScore.ToString(),new Vector2(1200, 800),Color.LimeGreen); 

                        break;
                    }
            
            }
           
           



            spriteBatch.End(); //ends sprite
            base.Draw(gameTime);
        }
        //Asteroid Loader
        public void LoadAsteroids()
        {
            //Random Variables for X & Y axis for asteroid object
            int randX = random.Next(0, 1600);
            int randY = random.Next(-950, -50);

            //if less than 5 asteroids then create more till there is 3 again
            if (asteroidList.Count() < 3)
            {
                asteroidList.Add(new Asteroid(Content.Load<Texture2D>("asteroid-big-0000"), new Vector2(randX, randY)));
            }

            // If any asteroid were destroyed then remove from the asteroidList
            //Respawns another set of asteroids 
            for (int i = 0; i < asteroidList.Count; i++) //if i is less than the count add one.
            {
                if (!asteroidList[i].isVisible)
                {
                    asteroidList.RemoveAt(i);
                    i--;
                }

            }

        }
        //Load enemies
        public void LoadEnemies()
        {
            //Random Variables for X & Y axis for enemies object
            int randX = random.Next(0, 1600);
            int randY = random.Next(-950, -50);

            //if less than 2 enemies then create more till there is 2 again
            if (enemyList.Count() < 2)
            {
                enemyList.Add(new Enemy(Content.Load<Texture2D>("enemyship"), new Vector2(randX, randY), Content.Load<Texture2D>("EnemyBullet")));
            }

            // If any enemies were destroyed then remove from the enemyList
            //Respawns another set of enemies 
            for (int i = 0; i < enemyList.Count; i++) //if i is less than the count add one.
            {
                if (!enemyList[i].isVisible)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }

            }

        }
        // Manage Explosions
        public void ManageExplosions()
        {
            for (int i = 0; i < explosionList.Count; i++) //if i is less than the count add one.
            {
                if (!explosionList[i].isVisible)
                {
                    explosionList.RemoveAt(i);
                    i--;
                }
            }

        }
    }
}

