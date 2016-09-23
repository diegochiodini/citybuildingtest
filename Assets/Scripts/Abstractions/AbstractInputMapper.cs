using UnityEngine;

namespace Game.Abstractions
{
    public abstract class AbstractInputMapper : MonoBehaviour
    {
        [SerializeField]
        protected string _upAxis;

        [SerializeField]
        protected string _forwardAxis;

        [SerializeField]
        protected string _rightAxis;

        protected abstract void Up(float value);
        protected abstract void Forward(float value);
        protected abstract void Right(float value);

        protected void AquireInput()
        {
            if (!string.IsNullOrEmpty(_upAxis))
            {
                Up(Input.GetAxis(_upAxis));
            }

            if (!string.IsNullOrEmpty(_forwardAxis))
            {
                Forward(Input.GetAxis(_forwardAxis));
            }

            if (!string.IsNullOrEmpty(_rightAxis))
            {
                Right(Input.GetAxis(_rightAxis));
            }
        }
    }
}