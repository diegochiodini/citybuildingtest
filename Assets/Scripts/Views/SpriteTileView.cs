using Game.Abstractions;
using Game.Models.Cache;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Views
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteTileView : AbstractTile
    {
        private static SpriteCache _cache = null;

        private void Awake()
        {
            if (_cache == null)
            {
                _cache = Locator.GetModel<SpriteCache>();
            }
        }

        public override void Init(int type)
        {
            Assert.IsTrue(type < _cache.Length, "Type must be lower than " + _cache.Length);
            _type = type;
            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = _cache.Get(Type);
        }

        public void OnClick()
        {
            Debug.Log("Click", this);
        }
    }
}