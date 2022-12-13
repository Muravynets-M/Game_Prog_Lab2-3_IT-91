using System;
using Movement.Fps;
using UnityEngine;

namespace Movement.WallRunning.WallState
{
    public class OnWallState : IWallState
    {
        private float _desiredAngle = 15f;
        private float _rotSpeed = 1500;
        
        public Collider CollidedWith { get; set; }
        private Collider _previousCollider = null;

        public void Handle(MovementEntity movement, CameraEntity camera)
        {
            var rotationDirection = Mathf.Sign(Input.GetAxis("Horizontal"));
            if (movement.velocity.y < 0) 
                movement.velocity.y *= Time.deltaTime * 10f;

            var cameraRotation = camera.playerCamera.transform.eulerAngles;

            if (ShouldRotate(cameraRotation, rotationDirection))
            {
                var desiredQ = Quaternion.Euler(
                    cameraRotation.x, 
                    0,
                    cameraRotation.z + rotationDirection * _rotSpeed * Time.deltaTime
                );
                camera.playerCamera.transform.localRotation = Quaternion.Lerp(
                    camera.playerCamera.transform.rotation,
                    desiredQ,
                    Time.deltaTime * 10
                );
            }

            if (_previousCollider == null || CollidedWith.gameObject.GetInstanceID() != _previousCollider.gameObject.GetInstanceID())
            {
                Debug.LogError("Returned Jump!");
                _previousCollider = CollidedWith;
                movement.currentJumpCount++;
            }
        }

        private bool ShouldRotate(Vector3 cameraRotation, float rotationDirection)
        {
            if (rotationDirection > 0)
            {
                return _desiredAngle > cameraRotation.z || 360 - _desiredAngle < cameraRotation.z;
            }

            return 360f - _desiredAngle < cameraRotation.z || cameraRotation.z < _desiredAngle;
        }
    }
}