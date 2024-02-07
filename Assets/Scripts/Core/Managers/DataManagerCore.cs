using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Core
{
    public class DataManagerCore : IManager
    {
        Action LoadCompleted = null;
        public void SetCompleted(Action completed)
        {
            LoadCompleted = completed;
            if (DataCount == loadedData) completed.Invoke();
        }
        int loadedData = 0;


        // edit user-preferences
        public const int DataCount = 0;

        // add members to load


        public bool Loaded()
        {
            // add user codes ...

            return true;
        }

        public void Load()
        {
            // load data with LoadJsonAsync/LoadXmlAsync
        }

        void LoadJsonAsync<T>(string key, Action<T> callback)
        {
            ManagerCore.Resource.LoadAsyncOnce<TextAsset>(key, (textAsset) =>
            {
                T json = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(textAsset.text);
#if DEBUG
                Debug.Log($"Loaded json data: {key}");
#endif
                callback?.Invoke(json);

                loadedData++;
                if (loadedData == DataCount)
                {
#if DEBUG
                    Debug.Log($"All data is loaded completely.");
#endif
                    LoadCompleted?.Invoke();
                }
            });
        }

        void LoadXmlAsync<T>(string key, Action<T> callback)
        {
            ManagerCore.Resource.LoadAsyncOnce<TextAsset>(key, (textAsset) =>
            {
                XmlSerializer xs = new(typeof(T));
                MemoryStream stream = new(System.Text.Encoding.UTF8.GetBytes(textAsset.text));
                callback?.Invoke((T)xs.Deserialize(stream));

                loadedData++;
                if (loadedData == DataCount)
                {
#if DEBUG
                    Debug.Log($"All data is loaded completely.");
#endif
                    LoadCompleted?.Invoke();
                }
            });
        }

        void IManager.ClearManager()
        {
        }

        void IManager.InitManager()
        {

        }
    }
}
