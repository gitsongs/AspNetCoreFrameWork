using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreFrameWork.CoreHttpFeature
{
    public interface IFeatureCollection : IDictionary<Type, object> 
    {
        public T Get<T>();
        public IFeatureCollection Set<T>(T value);
    }
    public class FeatureCollection : Dictionary<Type, object>, IFeatureCollection
    {
        public T Get<T>() => this.TryGetValue(typeof(T), out var value) ? (T)value : default(T);
        public IFeatureCollection Set<T>(T value)
        {
            this[typeof(T)] = value;
            return this;
        }
    }
}
