using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace TileMapEditor
{
    class Editor : GraphicsDeviceControl
    {
        ContentManager content;
        SpriteBatch spriteBatch;
        Map map;
        int layerNumber;
        public List<Image> Selector;
        string[] selectorPath = { "Graphics/SelectorT1", "Graphics/SelectorT2", "Graphics/SelectorB1", "Graphics/SelectorB2" };

        public event EventHandler OnInitialize;

        public Editor()
        {
            map = new Map();
            layerNumber = 0;
            Selector = new List<Image>();

            for (int i = 0; i < 4; i++)
            {
                Selector.Add(new Image());
            }
        }

        public Layer CurrentLayer
        {
            get { return map.Layer[layerNumber]; }
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            content = new ContentManager(Services, "Content");

            for (int i = 0; i < 4; i++)
            {
                Selector[i].Path = selectorPath[i];
                Selector[i].Initialize(content);
            }

            XmlManager<Map> mapLoader = new XmlManager<Map>();
            map = mapLoader.Load("Load/Map1.xml");
            map.Initialize(content);

            if (OnInitialize != null)
            {
                OnInitialize(this, null);
            }
        }

        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            map.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
