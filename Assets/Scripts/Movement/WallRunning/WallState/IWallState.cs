using Movement.Fps;
using UnityEngine;

namespace Movement.WallRunning.WallState
{
    public interface IWallState
    {
        public void Handle(MovementEntity movement, CameraEntity camera);
    }
}