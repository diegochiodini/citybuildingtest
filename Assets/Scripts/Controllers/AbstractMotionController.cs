using UnityEngine;

namespace Game.Abstractions
{
    public abstract class AbstractMotionController : MonoBehaviour
    {
        public abstract Vector3 Velocity { get; set; }
        public abstract Vector3 VelocityConstraint { get; }

        public abstract Vector3 AngularVelocity { get; set; }
        public abstract Vector3 AngularVelocityConstraint { get; }

        protected abstract void EvaluateLinearPosition();
        protected abstract void EvaluateAngularPosition();

        private void FixedUpdate()
        {
            EvaluateLinearPosition();
            EvaluateAngularPosition();
        }
    }
}