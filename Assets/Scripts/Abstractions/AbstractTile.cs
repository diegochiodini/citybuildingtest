using UnityEngine;

namespace Game.Abstractions
{
    public abstract class AbstractTile : MonoBehaviour
    {
        [SerializeField]
        protected int _type = -1;

        public int Type
        {
            get
            {
                return _type;
            }
        }

        public abstract void Init(int type);
    }
}