using Movement.Fps;
using UnityEngine;

namespace Movement.WallRunning.WallState
{
    public class AwayFromWallState : IWallState
    {
        private float _desiredAngle = 0f;
        private float _rotSpeed = 1000;

        public void Handle(MovementEntity movement, CameraEntity camera)
        {
            var cameraRotation = camera.playerCamera.transform.eulerAngles;
            var rotationDirection = -Mathf.Sign(cameraRotation.z > 180 ? -1 : 1);

            if (_desiredAngle - Mathf.Abs(cameraRotation.z) < -0.1f )
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
        }
    };
}