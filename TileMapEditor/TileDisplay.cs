using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileMapEditor
{
    class TileDisplay : GraphicsDeviceControl
    {
        Editor editor;
        Image image;
        SpriteBatch spriteBatch;
        List<Image> selector;
        bool isMouseDown;
        Vector2 mousePosition, clickPosition;

        public TileDisplay(ref Editor editor)
        {
            this.editor = editor;
            editor.OnInitialize += LoadContent;
            isMouseDown = false;
        }

        void LoadContent(object sender, EventArgs e )
        {
            image = editor.CurrentLayer.Image;
            selector = editor.Selector;
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MouseDown += TileDisplay_MouseDown;
            MouseUp += delegate { isMouseDown = false; };
            MouseMove += TileDisplay_MouseMove;
        }

        void TileDisplay_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mousePosition = new Vector2((int)(e.X / editor.CurrentLayer.TileDimensions.X),
                (int)(e.Y / editor.CurrentLayer.TileDimensions.Y));

            mousePosition *= 32;

            if (mousePosition != clickPosition && isMouseDown)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i % 2 == 0 && mousePosition.X < clickPosition.X)
                    {
                        selector[i].Position.X = mousePosition.X;
                    }
                    else if (i % 2 != 0 && mousePosition.X > clickPosition.X)
                    {
                        selector[i].Position.X = mousePosition.X;
                    }

                    if (i < 2 && mousePosition.Y < clickPosition.Y)
                    {
                        selector[i].Position.Y = mousePosition.Y;
                    }
                    else if (i >= 2 && mousePosition.Y > clickPosition.Y)
                    {
                        selector[i].Position.Y = mousePosition.Y;
                    }

                }
            }
            else
            {
                foreach (var img in selector)
                {
                    img.Position = mousePosition;
                }
            }

            Invalidate();
        }

        void TileDisplay_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!isMouseDown)
            {
                clickPosition = mousePosition;

                foreach (var img in selector)
                {
                    img.Position = mousePosition;
                }
            }

            isMouseDown = true;

        }

        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            image.Draw(spriteBatch);
            foreach (var img in selector)
            {
                img.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
