using UnityEngine;

namespace Game.Abstractions
{
    public abstract class AbstractSelectionMenu<TElement,TData> : MonoBehaviour 
        where TElement :MonoBehaviour
        where TData :Object
    {
        [SerializeField]
        protected TElement ElementTemplate;

        protected abstract void OnItemSelected(TData data);
    } 
}