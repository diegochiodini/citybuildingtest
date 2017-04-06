using UnityEngine;

namespace Game.Abstractions
{
    public abstract class AbstractTile<T> : MonoBehaviour where T :IModel
    {
        public event System.Action<T> TileSelected;

        public abstract T Model { get; set; }
        public abstract void OnSelect();

        protected void FireSelectionEvent()
        {
            if (TileSelected != null)
            {
                TileSelected(Model);
            }
        }
    } 
}