using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Editor
{
    public static class ScriptableObjectUtility
    {
        /// <summary>
        //	This makes it easy to create, name and place unique new ScriptableObject asset files.
        /// </summary>
        public static void CreateAsset<T>() where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();
            Assert.IsNotNull(asset, "Can't create an instance of " + typeof(T).Name);
            InternalCreateAsset(asset);
        }

        public static void CreateAsset(System.Type type)
        {
            ScriptableObject asset = ScriptableObject.CreateInstance(type);
            Assert.IsNotNull(asset, "Can't create an instance of " + type.ToString());
            InternalCreateAsset(asset);
        }

        private static void InternalCreateAsset(ScriptableObject asset)
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets/Scripts/Models";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + asset.GetType().Name + ".asset");

            AssetDatabase.CreateAsset(asset, assetPathAndName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;

            Debug.Log(typeof(ScriptableObjectUtility).ToString() + "Create instance of " + assetPathAndName);
        }
    } 
}