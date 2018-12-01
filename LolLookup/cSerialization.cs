using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.IO;

namespace LolLookup
{
    public static class ImageSerializationExtensions
    {
        // extension class Image
        public static byte[] ToBytes(this Image image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, image.RawFormat);
                return stream.ToArray();
            }
        }
        public static string ToBase64String(this Image image)
        {
            return Convert.ToBase64String(ToBytes(image));
        }

        // extension class byte[]
        public static Image ToImage(this byte[] byteArray)
        {
            using (var stream = new MemoryStream(byteArray) { Position = 0})
            {
                return Image.FromStream(stream);
            }
        }

        // extension class string
        public static Image ToImage(this string base64String)
        {
            using (var stream = new MemoryStream(Convert.FromBase64String(base64String)) { Position = 0 })
            {
                return Image.FromStream(stream);
            }
        }
    }

    public static class ObjectsSerializationExtensions
    {
        // extension class object
        public static byte[] ToBytes(this object obj)
        {
            using (var stream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, obj);
                return stream.ToBytes();
            }
        }

        // extension class byte[]
        public static T ToObject<T>(this byte[] byteArray)
        {
            using (var stream = new MemoryStream(byteArray) { Position = 0 })
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

    }

    
}
