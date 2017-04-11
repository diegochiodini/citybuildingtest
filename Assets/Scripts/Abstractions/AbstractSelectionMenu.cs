using UnityEngine;

namespace Game.Abstractions
{
    public abstract class AbstractSelectionMenu<TElement,TData> : MonoBehaviour 
        where TElement :MonoBehaviour
        where TData :Object
    {
        [SerializeField]
        protected GameObject ElementTemplate;

        protected TElement ElementBehaviour;

        //protected abstract void OnItemSelected(ITile tile);
    } 
}