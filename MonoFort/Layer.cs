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
    public class Layer
    {
        public class TileMap
        {
            [XmlElement("Row")]
            public List<string> Row;

            public TileMap()
            {
                Row = new List<string>();
            }
        }

        [XmlElement("TileMap")]
        public TileMap Tiles;
        List<Tile> tiles;

        public Layer()
        {

        }

        public void LoadContent(Vector2 tileDimensions)
        {
            foreach (var row in Tiles.Row)
            {
                string[] split = row.Split(']');
                foreach (var s in split)
                {
                    if (s != string.Empty)
                    {
                        string str = s.Replace("[", string.Empty);
                        int value1 = int.Parse(str.Substring(0, str.IndexOf(':')));
                        int value2 = int.Parse(str.Substring(str.IndexOf(':') + 1));


                    }
                }
            }
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
