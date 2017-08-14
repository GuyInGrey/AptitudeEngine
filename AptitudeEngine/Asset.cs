using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptitudeEngine
{
    public abstract class Asset
    {
        public int ID { get; private set; }
        public string Path { get; private set; }

        public abstract void Load(FileStream file);
        public static T Load<T>(string path)
            where T : Asset, new()
        {
            // we dont care if the file doesnt exist since we will want File.Open to cause an exception for stack tracing.
            using (var file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var t = new T();
                t.Load(file);
                t.Path = path;

                return t;
            }
        }
    }
}