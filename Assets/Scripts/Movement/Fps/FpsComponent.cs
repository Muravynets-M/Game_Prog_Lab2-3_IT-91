using UnityEngine;
using UnityEngine.Serialization;

namespace Movement.Fps
{
    public enum MovementState
    {
        Standing,
        Walking,
        Running
    }

    public class FpsComponent : MonoBehaviour
    {
        private MovementEntity _movementEntity;
        private CameraEntity _cameraEntity;
        private CharacterController _characterController;

        [HideInInspector] public MovementState state = MovementState.Standing;
        private static float Vertical => Input.GetAxis("Vertical");
        private static float Horizontal => Input.GetAxis("Horizontal");

        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _movementEntity = GetComponent<MovementEntity>();
            _cameraEntity = GetComponent<CameraEntity>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            SetState();
            HandleState();

            ApplyGravity();

            PlayerMovement();
            CameraMovement();
        }

        private void ApplyGravity()
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (_movementEntity.currentJumpCount > 0)
                {
                    
                    _movementEntity.velocity.y = _movementEntity.JumpSpeed;
                    _movementEntity.currentJumpCount--;
                    
                }
            }
            if (!_characterController.isGrounded)
            {
                _movementEntity.velocity.y += 2 * Physics.gravity.y * Time.deltaTime;
            }
            else
            {
                _movementEntity.currentJumpCount = _movementEntity.maxJumpCount;
            }
        }

        private void SetState()
        {
            if (Vertical == 0f && Horizontal == 0f)
            {
                state = MovementState.Standing;
                return;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                state = MovementState.Running;
                return;
            }

            state = MovementState.Walking;
        }

        private void HandleState()
        {
            var forward = transform.TransformDirection(Vector3.forward);
            var right = transform.TransformDirection(Vector3.right);

            var acceleration = 0f;
            switch (state)
            {
                case MovementState.Standing:
                    acceleration = 0f;
                    break;
                case MovementState.Walking:
                    acceleration = _movementEntity.WalkingSpeed;
                    break;
                case MovementState.Running:
                    acceleration = _movementEntity.RunningSpeed;
                    break;
            }

            var previousY = _movementEntity.velocity.y;
            _movementEntity.velocity = (forward * (Vertical * acceleration)) + (right * (Horizontal * acceleration));
            _movementEntity.velocity.y = previousY;
        }

        private void PlayerMovement()
        {
            _characterController.Move(_movementEntity.velocity * Time.deltaTime);
        }

        private void CameraMovement()
        {
            _cameraEntity.rotationX += -Input.GetAxis("Mouse Y") * _cameraEntity.lookSpeed;
            _cameraEntity.rotationX =
                Mathf.Clamp(_cameraEntity.rotationX, -_cameraEntity.lookXLimit, _cameraEntity.lookXLimit);
            _cameraEntity.playerCamera.transform.localRotation = Quaternion.Euler(
                _cameraEntity.rotationX,
                0,
                _cameraEntity.playerCamera.transform.eulerAngles.z
            );
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _cameraEntity.lookSpeed, 0);
        }
    }
}