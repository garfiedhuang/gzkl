using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GZKL.Client.UI.Common
{
    public class CollectionHelper
    {
        /// <summary>
        /// 按指定数量分组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="size"></param>
        public static Dictionary<int, ICollection<T>> GroupBySize<T>(ICollection<T> t, int size = 1000)
        {
            ICollection<T> collection = (ICollection<T>)t;
            Dictionary<int, ICollection<T>> dic = new Dictionary<int, ICollection<T>>();
            if (collection.Count <= size)
            {
                dic.Add(0, collection);
            }
            else
            {
                for (int i = 0; i <= collection.Count / size; i++)
                {
                    ICollection<T> current = collection.Skip(i * size).Take(size).ToList();
                    dic.Add(i, current);
                }
            }
            return dic;
        }

        /// <summary>
        /// 克隆对象（通过序列化到内存、从内存反序列化回来的方式克隆）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Clone<T>(T source)
        {
            //进行序列化的对象必须注明[Serializable]注解
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            StreamingContext streamingContext = new StreamingContext(StreamingContextStates.Clone);
            IFormatter formatter = new BinaryFormatter(null, streamingContext);

            T t;
            using (Stream stream = new MemoryStream())
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                t = (T)formatter.Deserialize(stream);
                stream.Flush();
                stream.Close();
            }
            return t;
        }

        /// <summary>
        /// 克隆列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origin"></param>
        /// <returns></returns>
        public static List<T> Clone<T>(List<T> origin)
        {
            List<T> target = new List<T>();
            foreach (T t in origin)
            {
                target.Add(Clone(t));
            }
            return target;
        }
    }
}
