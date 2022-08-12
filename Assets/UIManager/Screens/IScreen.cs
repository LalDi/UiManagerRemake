//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public interface IScreen
//{
//    public class Id
//    {
//        readonly string name;

//        public Id(string name)
//        {
//            this.name = name;
//        }

//        public override int GetHashCode()
//        {
//            return name.GetHashCode();
//        }

//        public static bool operator ==(IScreen.Id x, IScreen.Id y)
//        {
//            if (ReferenceEquals(x, null))
//                return ReferenceEquals(y, null);

//            return x.Equals(y);
//        }

//        public static bool operator !=(IScreen.Id x, IScreen.Id y)
//        {
//            return !(x == y);
//        }

//        public override bool Equals(object obj)
//        {
//            Id other = obj as Id;
//            if (ReferenceEquals(other, null))
//                return false;

//            return name == other.name;
//        }

//        public override string ToString()
//        {
//            return name;
//        }
//    }

//    /// <summary>
//    /// Data container that is passed along to Screens that are being pushed. Screens can use these to setup
//    /// themselves up with custom data provided at run-time.
//    /// </summary>
//    public class Data
//    {
//        private Dictionary<string, object> _data;

//        public Data()
//        {
//            _data = new Dictionary<string, object>();
//        }

//        public Data(int capacity)
//        {
//            _data = new Dictionary<string, object>(capacity);
//        }

//        public void Add(string key, object data)
//        {
//            _data.Add(key, data);
//        }

//        public T Get<T>(string key)
//        {
//            object datum = Get(key);

//            try
//            {
//                return (T)datum;
//            }
//            catch
//            {
//                throw new System.Exception(string.Format("[BlitzyUI.Screen.Data] Could not cast data object '{0}' to type '{1}'", key, typeof(T).Name));
//            }
//        }

//        public object Get(string key)
//        {
//            object datum;

//            if (!_data.TryGetValue(key, out datum))
//                throw new System.Exception(string.Format("[BlitzyUI.Screen.Data] No object found for key '{0}'", key));

//            return datum;
//        }

//        public bool TryGet(string key, out object datum)
//        {
//            return _data.TryGetValue(key, out datum);
//        }

//        public bool TryGet<T>(string key, out T datum)
//        {
//            object datumObj;

//            if (_data.TryGetValue(key, out datumObj))
//            {
//                try
//                {
//                    datum = (T)datumObj;
//                    return true;
//                }
//                catch
//                {
//                    throw new System.Exception(string.Format("[BlitzyUI.Screen.Data] Could not cast data object '{0}' to type '{1}'", key, typeof(T).Name));
//                }
//            }

//            datum = default(T);
//            return false;
//        }
//    }

//    public delegate void ScreenDelegate(IScreen screen);

//    public event ScreenDelegate onPushFinished;
//    public event ScreenDelegate onPopFinished;

//    public abstract void Setup(IScreen.Id id, EScreenKey key);

//    public abstract void OnSetData(IScreen.Data data);

//    public abstract void OnHierFixed();

//    public abstract void OnShowing();

//    public abstract void InAnimEnd();

//    public abstract void OnHiding();

//    public abstract void OutAnimEnd();

//    public abstract void PushFinished();

//    public abstract void PopFinished();
//}
