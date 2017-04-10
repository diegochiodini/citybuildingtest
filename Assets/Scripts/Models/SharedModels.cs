using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class SharedModels : Singleton<SharedModels>
{
    [SerializeField]
    private ScriptableObject[] _assets = null;

    //TODO: The type T is supposed to be a serializable class
    public static T GetWriteableModel<T>() where T :MonoBehaviour
    {
        return Instance.GetComponent<T>();
    }

    public static T Get<T>() where T : IModel
    {
        return GetModels<T>().FirstOrDefault();
    }

    public static T[] GetModels<T>() where T : IModel
    {
        if (Instance._assets == null || Instance._assets.Length == 0)
        {
            throw new System.Exception("Asset not initialised in Locator");
        }

        IEnumerable<T> results = Instance._assets.OfType<T>();
        if (results == null || !results.Any())
        {
            string message = string.Format("Locator can't find type of: {0}. Please check if the asset has a valid Script field", typeof(T).Name);
            throw new System.Exception(message);
        }

        var array = results.ToArray();
        Assert.IsNotNull(array, "Failed conversion");
        return array;
    }

    public static void PrintAssets()
    {
        if (Instance._assets == null || Instance._assets.Length == 0)
        {
            Debug.LogWarning("Locator assets are null or empty");
        }
        else
        {
            foreach (var item in Instance._assets)
            {
                Debug.Log("Locator: " + item);
            }
        }
    }
}