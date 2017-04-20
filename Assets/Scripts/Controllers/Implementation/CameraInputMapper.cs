using System;
using Game.Abstractions;
using UnityEngine;

namespace Game.Controllers
{
    [RequireComponent(typeof(AbstractMotionController))]
    public class CameraInputMapper : AbstractInputMapper
    {
        [SerializeField]
        private float _speed = 1f;

        private Vector3 _tempVelocity = Vector3.zero;

        private AbstractMotionController _motionController;

        private void Awake()
        {
            _motionController = GetComponent<AbstractMotionController>();
        }

        private void Update()
        {
            AquireInput();
            _motionController.Velocity = _tempVelocity;
        }

        protected override void Forward(float value)
        {
            _tempVelocity.z = value * _speed;
        }

        protected override void Right(float value)
        {
            _tempVelocity.x = value * _speed;
        }

        protected override void Up(float value)
        {
            throw new NotImplementedException();
        }

        protected override void Fire1(float value)
        {
            if (value > 0f)
            {
                Debug.Log("Fire1");

            }
        }

        protected override void Fire2(float value)
        {
            throw new NotImplementedException();
        }
    }
}