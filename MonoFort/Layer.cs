﻿using System;
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
        public Image Image;
        List<Tile> underlayTiles, overlayTiles;
        public string SolidTiles, OverlayTiles;
        string state;


        public Layer()
        {
            Image = new Image();
            underlayTiles = new List<Tile>();
            overlayTiles = new List<Tile>();
            SolidTiles = OverlayTiles = string.Empty;
        }

        public void LoadContent(Vector2 tileDimensions)
        {
            Image.LoadContent();

            Vector2 position = -tileDimensions;

            foreach (var row in Tiles.Row)
            {
                string[] split = row.Split(']');
                position.X = -tileDimensions.X;
                position.Y += tileDimensions.Y;

                foreach (var s in split)
                {
                    if (s != string.Empty)
                    {
                        position.X += tileDimensions.X;

                        if (!s.Contains("x"))
                        {
                            state = "Passive";
                            Tile tile = new Tile();

                            string str = s.Replace("[", string.Empty);
                            int value1 = int.Parse(str.Substring(0, str.IndexOf(':')));
                            int value2 = int.Parse(str.Substring(str.IndexOf(':') + 1));

                            if (SolidTiles.Contains("[" + value1.ToString() + ":" + value2.ToString() + "]"))
                            {
                                state = "Solid";
                            }

                            tile.LoadContent(position, new Rectangle(
                                value1 * (int)tileDimensions.X, value2 * (int)tileDimensions.Y,
                                (int)tileDimensions.X, (int)tileDimensions.Y), state);

                            if (OverlayTiles.Contains("[" + value1.ToString() + ":" + value2.ToString() + "]"))
                            {
                                overlayTiles.Add(tile);
                            }
                            else
                            {
                                underlayTiles.Add(tile);
                            }
                        }
                    }
                }
            }
        }

        public void UnloadContent()
        {
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime, ref Player player)
        {
            foreach (var tile in underlayTiles)
            {
                tile.Update(gameTime, ref player);
            }

            foreach (var tile in overlayTiles)
            {
                tile.Update(gameTime, ref player);
            }
        }

        public void Draw(SpriteBatch spriteBatch, string drawType)
        {
            List<Tile> tiles;

            if (drawType == "Underlay")
            {
                tiles = underlayTiles;
            }
            else
            {
                tiles = overlayTiles;
            }
            
            foreach (var tile in tiles)
            {
                Image.Position = tile.Position;
                Image.SourceRect = tile.SourceRect;
                Image.Draw(spriteBatch);
            }
        }
    }
}
