using UnityEngine;

namespace Game.Abstractions
{
    public interface ITileView
    {
        event System.Action<ITileView, GameObject> TileSelectedEvent;
        event System.Action<ITileView, GameObject> TileEnableEvent;

        IModel ContentModel { get; set; }

        bool IsSelected { get; set; }
        bool IsEnabled { get; set; }

        void SetPosition(int order);
        void OnClick();
    } 
}