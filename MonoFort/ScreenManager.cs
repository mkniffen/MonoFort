using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Windows.Storage;

namespace MonoFort
{
    public class ScreenManager
    {
        [XmlIgnore]
        public Vector2 Dimensions { private set; get; }
        [XmlIgnore]
        public ContentManager Content { private set; get;}
        XmlManager<GameScreen> xmlGameScreenManager;

        GameScreen currentScreen, newScreen;
        [XmlIgnore]
        public GraphicsDevice GraphicsDevice;
        [XmlIgnore]
        public SpriteBatch SpriteBatch;

        public Image Image;
        [XmlIgnore]
        public bool IsTransitioning { get; private set; }

        private static ScreenManager instance;
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    XmlManager<ScreenManager> xml = new XmlManager<ScreenManager>();
                    instance = xml.Load("Load/ScreenManager.xml");
                }

                return instance;
            }
        }

        public void ChangeScreens(string screenName)
        {
            newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("MonoFort." + screenName));
            Image.IsActive = true;
            Image.FadeEffect.Increase = true;
            Image.Alpha = 0.0f;
            IsTransitioning = true;
        }

        void Transition(GameTime gameTime)
        {
            if (IsTransitioning == true)
            {
                Image.Update(gameTime);
                if (Image.Alpha == 1.0f)
                {
                    currentScreen.UnloadContent();
                    currentScreen = newScreen;
                    xmlGameScreenManager.Type = currentScreen.Type;

                    try
                    {
                        //no exception means file exists -- Hack because File.IO does not work in Win 8 apps
                        TitleContainer.OpenStream(currentScreen.XmlPath);
                        currentScreen = xmlGameScreenManager.Load(currentScreen.XmlPath);

                    }
                    catch (Exception ex)
                    {
                        //find out through exception 
                    }

                    currentScreen.LoadContent();
                }
                else if (Image.Alpha == 0.0f)
                {
                    Image.IsActive = false;
                    IsTransitioning = false;
                }
            }
        }

        public ScreenManager()
        {
            Dimensions = new Vector2(640, 480);
            //currentScreen = new SplashScreen();
            currentScreen = new GameplayScreen(); ;
            xmlGameScreenManager = new XmlManager<GameScreen>();
            xmlGameScreenManager.Type = currentScreen.Type;
            //currentScreen = xmlGameScreenManager.Load("Load/SplashScreen.xml");
        }

        public void LoadContent(ContentManager content)
        {
            this.Content = new ContentManager(content.ServiceProvider, "Content");
            currentScreen.LoadContent();
            Image.LoadContent();
        }

        public void UnloadContent()
        {
            currentScreen.UnloadContent();
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
            Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
            if (IsTransitioning)
            {
                Image.Draw(spriteBatch);
            }

        }
    }
}
