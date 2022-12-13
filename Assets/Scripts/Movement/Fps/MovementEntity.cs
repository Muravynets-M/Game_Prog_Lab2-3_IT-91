using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Movement.Fps
{
    [Serializable]
    public class MovementEntity: MonoBehaviour
    {
        [SerializeField] private float walkingSpeed = 7.5f;
        [SerializeField] private float runningSpeed = 11.5f;
        [SerializeField] private float jumpSpeed = 8.0f;
        
        public Vector3 velocity = Vector3.zero;
        public int maxJumpCount = 2;
        public int currentJumpCount = 2;

        public float WalkingSpeed
        {
            get => walkingSpeed;
            set => walkingSpeed = value;
        }

        public float RunningSpeed
        {
            get => runningSpeed;
            set => runningSpeed = value;
        }

        public float JumpSpeed
        {
            get => jumpSpeed;
            set => jumpSpeed = value;
        }
    }
}