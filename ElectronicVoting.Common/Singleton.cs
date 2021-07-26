using System;

namespace ElectronicVoting.Common
{
    public class Singleton<T>
    {
        private static readonly Lazy<T> _instance = new Lazy<T>();

        protected Singleton()
        {
            
        }

        public static T Instance => (T) _instance.Value; 
    }
}