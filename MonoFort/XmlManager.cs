using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;


namespace MonoFort
{
    public class XmlManager<T>
    {
        public Type Type;

        public XmlManager()
        {
            Type = typeof(T);
        }

        public T Load(string path)
        {
            T instance;

            using (TextReader reader = new StreamReader(TitleContainer.OpenStream(path)))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                instance = (T)xml.Deserialize(reader);
            }

            return instance;
        }

        public void Save(string path, object obj)
        {
            using (TextWriter writer = new StreamWriter(TitleContainer.OpenStream(path)))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                xml.Serialize(writer, obj);

            }
        }
    }
}
