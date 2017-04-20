using UnityEngine;

namespace Game.Abstractions
{
    public interface ISelectionMenuView
    {
        ITileView CurrentElement { get; }

        void OnItemSelected(ITileView tile, GameObject tileGameObject);
    } 
}