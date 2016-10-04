using UnityEngine;

namespace Game.Helpers
{
    [System.Serializable]
    public class Serializer<T>
    {
        private string _path;
        public T Value;

        public Serializer(string path)
        {
            _path = path;
        }

        public Serializer(string path, T value)
        {
            Value = value;
            _path = path;
        }

        public void Save(bool prettyPrint = true)
        {
            string json = JsonUtility.ToJson(this, prettyPrint);
            System.IO.File.WriteAllText(_path, json);
            Debug.LogFormat("Saving {0}: \n{1}", _path, json);
        }

        public bool Load()
        {
            Value = default(T);

            bool result = true;
            try
            {
                string json = System.IO.File.ReadAllText(_path);
                JsonUtility.FromJsonOverwrite(json, this);
                Debug.Log("Loading: " + json);
            }
            catch (System.Exception ex)
            {
                result = false;
                Debug.LogWarning(ex.Message);
            }

            return result;
        }
    } 
}