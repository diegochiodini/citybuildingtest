using UnityEngine;

namespace Game.Abstractions
{
    public abstract class AbstractSelectionCell<TData> : MonoBehaviour where TData:IModel
    {
        protected TData ModelCache;
        public abstract TData Model { get; set; }
    } 
}