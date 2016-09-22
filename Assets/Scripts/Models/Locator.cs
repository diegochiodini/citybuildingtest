using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Locator : MonoBehaviour
{
    private static Locator _instance;

    [SerializeField]
    private ScriptableObject[] _assets = null;

    private void Awake()
    {
        _instance = this;
    }

    public static T GetModel<T>() where T : IModel
    {
        return GetModels<T>().FirstOrDefault();
    }

    public static IEnumerable<T> GetModels<T>() where T : IModel
    {
        if (_instance._assets == null || _instance._assets.Length == 0)
        {
            throw new System.Exception("Asset not initialised in Locator");
        }

        IEnumerable<T> results = _instance._assets.OfType<T>();
        if (results == null || !results.Any())
        {
            string message = string.Format("Locator can't find type of: {0}. Please check if the asset has a valid Script field", typeof(T).Name);
            throw new System.Exception(message);
        }

        return results;
    }

    public static void PrintAssets()
    {
        if (_instance._assets == null || _instance._assets.Length == 0)
        {
            Debug.LogWarning("Locator assets are null or empty");
        }
        else
        {
            foreach (var item in _instance._assets)
            {
                Debug.Log("Locator: " + item);
            }
        }
    }
}