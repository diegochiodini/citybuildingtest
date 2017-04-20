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

        [SerializeField]
        protected string _fire1;

        [SerializeField]
        protected string _fire2;

        protected abstract void Up(float value);
        protected abstract void Forward(float value);
        protected abstract void Right(float value);
        protected abstract void Fire1(float value);
        protected abstract void Fire2(float value);

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

            if (!string.IsNullOrEmpty(_fire1))
            {
                Fire1(Input.GetAxis(_fire1));
            }

            if (!string.IsNullOrEmpty(_fire2))
            {
                Fire2(Input.GetAxis(_fire2));
            }
        }
    }
}