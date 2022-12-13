using System;
using UnityEngine;

namespace Movement.Fps
{
    [Serializable]
    public class CameraEntity: MonoBehaviour
    {
        public Camera playerCamera;
        public float lookSpeed = 2.0f;
        public float lookXLimit = 45.0f;
        
        public float rotationX = 0;
    }
}