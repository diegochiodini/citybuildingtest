using UnityEngine;

namespace Game.Abstractions
{
    public abstract class AbstractTile<T> : MonoBehaviour where T :IModel
    {
        public abstract T Model { get; set; }
    } 
}