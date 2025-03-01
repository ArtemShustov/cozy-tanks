using UnityEngine;

namespace Game.Tanks {
    public class TankMovement : MonoBehaviour { 
        [Header("Settings")]
        [SerializeField] private float _linearSpeed = 3;
        [SerializeField] private float _linearAcceleration = 30;
        [SerializeField] private float _rotationSpeed = 5;
        [SerializeField] private float _brakeForce = 5;
        [Header("Ground")]
        [SerializeField] private float _groundCheckDistance = 0.1f;
        [SerializeField] private Vector3 _groundCheckArea = new Vector3(1, 0.1f, 1);
        [SerializeField] private LayerMask _groundMask;
        [Header("Components")]
        [SerializeField] private Rigidbody _rigidbody;

        private readonly RaycastHit[] _groundHits = new RaycastHit[8];
        
        private void FixedUpdate() {
            var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            var clampedInput = Vector2.ClampMagnitude(input, 1);
            if (!IsOnGround()) {
                return;
            }
            RotateTo(clampedInput);
            MoveTo(clampedInput);
            ApplyBraking(clampedInput);
        }

        private void RotateTo(Vector2 input) {
            if (input.sqrMagnitude < Mathf.Epsilon) return;

            Vector3 localDirection = _rigidbody.transform.InverseTransformDirection(new Vector3(input.x, 0, input.y));
            float targetAngle = Mathf.Atan2(localDirection.x, localDirection.z) * Mathf.Rad2Deg;
            float newAngle = Mathf.LerpAngle(0, targetAngle, _rotationSpeed * Time.deltaTime);
            _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(0, newAngle, 0));
        }
        private void MoveTo(Vector2 input) {
            if (input.sqrMagnitude < Mathf.Epsilon) return;
            
            Vector3 targetVelocity = transform.forward * (_linearSpeed * input.magnitude);
            Vector3 velocityChange = targetVelocity - _rigidbody.linearVelocity;
            Vector3 accelerationForce = Vector3.ClampMagnitude(velocityChange * _linearAcceleration, _linearAcceleration);
            _rigidbody.AddForce(accelerationForce, ForceMode.Acceleration);
        }
        private void ApplyBraking(Vector2 input) {
            if (input.sqrMagnitude > Mathf.Epsilon) return;
            
            Vector3 brakeForce = -_rigidbody.linearVelocity.normalized * _brakeForce;
            if (_rigidbody.linearVelocity.magnitude < 0.1f) {
                _rigidbody.linearVelocity = Vector3.zero;
            } else {
                _rigidbody.AddForce(brakeForce, ForceMode.Acceleration);
            }
        }

        public bool IsOnGround() {
            var hitCount = Physics.BoxCastNonAlloc(
                transform.position, 
                _groundCheckArea / 2, 
                -transform.up,
                _groundHits,
                transform.rotation,
                _groundCheckDistance,
                _groundMask.value
            );
            for (int i = 0; i < hitCount; i++) {
                var hit = _groundHits[i];
                if (hit.rigidbody == _rigidbody) continue;
                if (Vector3.Dot(hit.normal, transform.up) > 0.9f) return true;
            }
            return false;
        }

        #if DEBUG
        private void OnDrawGizmosSelected() {
            Gizmos.color = IsOnGround() ? Color.red : Color.green;
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            Vector3 center = Vector3.down * (_groundCheckDistance / 2);
            Vector3 size = _groundCheckArea + new Vector3(0, _groundCheckDistance, 0);
            Gizmos.DrawWireCube(center, size);
        }
        private void OnGUI() {
            var style = new GUIStyle();
            style.fontSize = 48;
            GUILayout.Label($"Speed: {_rigidbody.linearVelocity.magnitude}", style);
        }
        #endif
    }
}
