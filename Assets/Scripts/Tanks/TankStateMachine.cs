using Mirror;
using UnityEngine;

namespace Game.Tanks {
	public class TankStateMachine: NetworkBehaviour {
		[Header("Ground")]
		[SerializeField] private float _groundCheckDistance = 0.1f;
		[SerializeField] private Vector3 _groundCheckArea = new Vector3(1, 0.1f, 1);
		[SerializeField] private LayerMask _groundMask;
		[Header("Components")]
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private TankState _defaultState;
		
		private readonly TankInputProvider _input = new TankInputProvider();
		private ITankState _current;

		public Rigidbody Rigidbody => _rigidbody;
		public ITankInput Input => _input;
		public ITankState CurrentState => _current;

		private void Awake() {
			if (TryGetComponent<ITankInput>(out var defaultInput)) {
				_input.SetInput(defaultInput);
			}
			Change(_defaultState);
		}
		private void Update() {
			_current?.OnUpdate();
		}
		private void FixedUpdate() {
			_current?.OnFixedUpdate();
		}

		public void Change(ITankState state) {
			state.Initialize(this);
			_current?.OnExit();
			_current = state;
			_current?.OnEnter();
		}
		public void SetInput(ITankInput input) {
			_input.SetInput(input);
		}
		
		public void RotateTo(Vector2 direction, float speed, float deltaTime) {
			if (!isLocalPlayer) return;
			if (direction.sqrMagnitude < Mathf.Epsilon) return;
			
			Vector3 localDirection = _rigidbody.transform.InverseTransformDirection(new Vector3(direction.x, 0, direction.y));
			float targetAngle = Mathf.Atan2(localDirection.x, localDirection.z) * Mathf.Rad2Deg;
			
			float newAngle = Mathf.LerpAngle(0, targetAngle, speed * deltaTime);
			_rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(0, newAngle, 0));
		}
		public void MoveTo(Vector2 direction, float speed, float deltaTime) {
			if (!isLocalPlayer) return;
			
			var movement = transform.forward * (speed * _input.Throttle * deltaTime);
			_rigidbody.MovePosition(transform.position + movement);
		}
		public bool IsOnGround() {
			var colliders = Physics.BoxCastAll(
				transform.position, 
				_groundCheckArea / 2, 
				-transform.up,
				transform.rotation,
				_groundCheckDistance,
				_groundMask.value
			);
			foreach (var hit in colliders) {
				if (hit.rigidbody == _rigidbody) {
					continue;
				}
				if (Vector3.Dot(hit.normal, transform.up) > 0.9f) {
					return true;
				}
			}
			return false;
		}
		public bool IsStuck() {
			return !IsOnGround() && _rigidbody.linearVelocity.sqrMagnitude <= 1;
		}

		/*
		private void OnDrawGizmosSelected() {
			Gizmos.color = IsOnGround() ? Color.red : Color.green;
			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.DrawWireCube(Vector3.zero, _groundCheckArea);
			Gizmos.DrawWireCube(Vector3.down * _groundCheckDistance, _groundCheckArea);
			if (IsStuck()) {
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(Vector3.zero, 1);
			}
		}
		*/
	}
}