using Game.Models.Cache;
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    public static class CreateCacheAssets
    {
        private const string MenuPrefix = "Window/ScriptableObjects/Cache Objects/Create ";

        [MenuItem(MenuPrefix + "SpriteCache")]
        public static void CreateSpriteCache()
        {
            ScriptableObjectUtility.CreateAsset<SpriteCache>();
        }

        [MenuItem(MenuPrefix + "TextureCache")]
        public static void CreateTextureCache()
        {
            ScriptableObjectUtility.CreateAsset<TextureCache>();
        }

        [MenuItem(MenuPrefix + "Texture2DCache")]
        public static void CreateTexture2DCache()
        {
            ScriptableObjectUtility.CreateAsset<Texture2DCache>();
        }

        [MenuItem(MenuPrefix + "Texture3DCache")]
        public static void CreateTexture3DCache()
        {
            ScriptableObjectUtility.CreateAsset<Texture3DCache>();
        }

        [MenuItem(MenuPrefix + "GameObjectCache")]
        public static void CreateGameObjectCache()
        {
            ScriptableObjectUtility.CreateAsset<CacheModel<GameObject>>();
        }
    }
}