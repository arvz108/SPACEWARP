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
    public class HUD
    
    {
        public int playerScore,screenHeight, screenWidth;
        public SpriteFont playerScoreFont;
        public Vector2 playerScorePos;
        public bool showHud;

        //Constructor
        public HUD()
        {
            playerScore = 0;
            showHud = true;
            screenHeight = 950;
            screenWidth = 1800;
            playerScoreFont = null;
            playerScorePos = new Vector2(screenWidth/2, 20);

        }

        //Load Content
        public void LoadContent(ContentManager Content)
        {
            playerScoreFont = Content.Load<SpriteFont>("Corbel");
        
        }

        //Update
        public void Update(GameTime gameTime)
        { 
            //Key State
            KeyboardState keyState = Keyboard.GetState();
        }

        //Draw
        public void Draw(SpriteBatch spriteBatch)
        { 
            //If HUD is toggled on then show HUD
            if (showHud)
                spriteBatch.DrawString(playerScoreFont, "Score : " + playerScore, playerScorePos, Color.LimeGreen);


        }

    }
}
