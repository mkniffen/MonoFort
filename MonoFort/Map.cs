using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoFort
{
    public class Map
    {
        [XmlElement("Layer")]
        public List<Layer> Layers;
        public Vector2 TileDimensions;

        public Map()
        {
            Layers = new List<Layer>();
            TileDimensions = Vector2.Zero;
        }

        public void LoadContent()
        {
            foreach (var layer in Layers)
            {
                layer.LoadContent(TileDimensions);
            }
        }

        public void UnloadContent()
        {
            foreach (var layer in Layers)
            {
                layer.UnloadContent();
            }
        }

        public void Update(GameTime gameTime, ref Player player)
        {
            foreach (var layer in Layers)
            {
                layer.Update(gameTime, ref player);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var layer in Layers)
            {
                layer.Draw(spriteBatch);
            }
        }

    }
}
