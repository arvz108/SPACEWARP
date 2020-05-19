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
    public class SoundManager
    {
        public SoundEffect playerShoot;
        public SoundEffect explodeSound;
        public Song bgMusic;


        public SoundManager()
        {
            playerShoot = null;
            explodeSound = null;
            bgMusic = null;
        }
        public void LoadContent(ContentManager Content)
        {
            playerShoot = Content.Load<SoundEffect>("playershoot");
            explodeSound = Content.Load<SoundEffect>("explode");
            bgMusic = Content.Load<Song>("bg");
        }
    }
}
