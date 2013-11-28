using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoFort
{
    public class TitleScreen : GameScreen
    {
        MenuManager menuManager;

        public TitleScreen()
        {
            menuManager = new MenuManager();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            menuManager.LoadContent("Load/TitleMenu.xml");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            menuManager.UnloadContent();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            menuManager.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            menuManager.Draw(spriteBatch);
        }
    }
}
