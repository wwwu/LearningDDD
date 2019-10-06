using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace LearningDDD.Domain.Core.Models
{
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        //protected abstract bool EqualsCore(T other);

        //protected abstract int GetHashCodeCore();

        ///// <summary>
        ///// 重写方法 相等运算
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public override bool Equals(object obj)
        //{
        //    var valueObject = obj as T;
        //    return !ReferenceEquals(valueObject, null) && EqualsCore(valueObject);
        //}

        ///// <summary>
        ///// 获取哈希
        ///// </summary>
        ///// <returns></returns>
        //public override int GetHashCode()
        //{
        //    return GetHashCodeCore();
        //}

        /// <summary>
        /// 重写方法 实体比较 ==
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        /// <summary>
        /// 重写方法 实体比较 !=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }

        /// <summary>
        /// 浅拷贝
        /// </summary>
        public virtual T ShallowCopy()
        {
            return (T)MemberwiseClone();
        }

        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <returns></returns>
        public virtual T DeepCopy()
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, this);
            memoryStream.Position = 0;
            return (T)formatter.Deserialize(memoryStream);
        }
    }
}
