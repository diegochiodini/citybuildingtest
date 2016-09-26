using UnityEngine;

namespace Game.Abstractions
{
    public abstract class AbstractToggleMenu : MonoBehaviour
    {
        public abstract bool IsOpen { get; }
        public abstract void ToggleMenu();
    } 
}