using System.Collections.Generic;
using Movement.Fps;
using Movement.WallRunning.WallState;
using UnityEngine;

namespace Movement.WallRunning
{
    public class WallRunnerComponent : MonoBehaviour
    {
        [SerializeField] private float rayCastDistance;
        private const int LayerMask = 1 << 8;
        private CharacterController _characterController;
        private MovementEntity _movementEntity;
        private CameraEntity _cameraEntity;
        
        private IWallState _state;
        private AwayFromWallState _away;
        private OnWallState _on;

        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _cameraEntity = GetComponent<CameraEntity>();
            _movementEntity = GetComponent<MovementEntity>();
            
            
            _away = new AwayFromWallState();
            _on = new OnWallState();
            _state = _away;
        }
        
        void Update()
        {
            _state.Handle(_movementEntity, _cameraEntity);
            _state = _away;
            
            if (_characterController.isGrounded) 
                return;

            var hit = CheckForWall();
            if (hit.collider != null)
            {
                _on.CollidedWith = hit.collider;
                _state = _on;
            }
        }

        private RaycastHit CheckForWall()
        {
            var direction = (transform.right * Input.GetAxis("Horizontal")).normalized;
            if (Physics.Raycast(transform.position, direction, out var hit, rayCastDistance, LayerMask))
            {
                Debug.DrawRay(transform.position, direction * hit.distance, Color.yellow, 0.05f, false);
            }
            else
            {
                Debug.DrawRay(transform.position, direction * rayCastDistance, Color.white, 0.05f, false);
            }

            return hit;
        }
    }
}
