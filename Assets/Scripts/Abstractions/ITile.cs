using UnityEngine;

namespace Game.Abstractions
{
    public interface ITile
    {
        event System.Action<ITile, GameObject> TileSelectedEvent;
        event System.Action<ITile, GameObject> TileEnableEvent;

        IModel ContentModel { get; set; }

        bool IsSelected { get; set; }
        bool IsEnabled { get; set; }

        void SetPosition(int order);
        void OnClick();
    } 
}