﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoFort
{
    public class GameplayScreen : GameScreen
    {
        Player player;
        Map map;

        public override void LoadContent()
        {
            base.LoadContent();

            XmlManager<Player> playerLoader = new XmlManager<Player>();
            XmlManager<Map> mapLoader = new XmlManager<Map>();
            player = playerLoader.Load("Load/Player.xml");
            map = mapLoader.Load("Load/Map/Map1.xml");
            player.LoadContent();
            map.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            player.UnloadContent();
            map.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            player.Update(gameTime);
            map.Update(gameTime, ref player);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            map.Draw(spriteBatch, "Underlay");
            player.Draw(spriteBatch);
            map.Draw(spriteBatch, "Overlay");
        }
    }
}
