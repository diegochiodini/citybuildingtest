using System;
using Game.Abstractions;
using UnityEngine;

namespace Game.Controllers
{
    public class LinearMotionController : AbstractMotionController
    {
        [SerializeField]
        private Vector3 _velocity;
        public override Vector3 Velocity
        {
            get
            {
                return _velocity;
            }

            set
            {
                _velocity = value;
            }
        }

        [SerializeField]
        private bool _x;

        [SerializeField]
        private bool _y;

        [SerializeField]
        private bool _z;

        private Vector3 _constraint;
        public override Vector3 VelocityConstraint
        {
            get
            {
                return _constraint;
            }
        }

        public override Vector3 AngularVelocity
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override Vector3 AngularVelocityConstraint
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private Vector3 _cacheVector;
        protected override void EvaluateLinearPosition()
        {
            _cacheVector.Set(Velocity.x * VelocityConstraint.x, Velocity.y * VelocityConstraint.y, Velocity.z * VelocityConstraint.z);
            transform.position += _cacheVector * Time.fixedDeltaTime;
        }

        protected override void EvaluateAngularPosition()
        {
            //do nothing
        }

        private void Awake()
        {
            _constraint = new Vector3(_x ? 1f : 0f, _y ? 1f : 0f, _z ? 1f : 0f);
        }
    }
}