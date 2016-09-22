using UnityEngine;

namespace Game.Abstractions
{
    public interface ICache<T> :IModel where T : Object
    {
        int Length { get; }

        T Get(int index);
    }
}