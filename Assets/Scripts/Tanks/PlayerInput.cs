using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Tanks {
	public class PlayerInput: MonoBehaviour, ITankInput {
		[SerializeField] private InputAction _movement = new InputAction("Movement", InputActionType.Value);
		[SerializeField] private InputAction _shoot = new InputAction("Shoot", InputActionType.Button);
		[SerializeField] private Camera _camera;
		
		private Vector2 _input;
		private Vector2 _lastInput;
		
		public Vector2 Direction => _input;
		public float Throttle => Mathf.Clamp01(_input.magnitude);
		public event Action Shoot;

		private void Awake() {
			if (_camera == null) {
				_camera = Camera.main;
			}
		}
		private void Update() {
			var input = GetRelatedMove();
			if (_lastInput != input) {
				_lastInput = input;
				_input = input;
			}
		}

		private Vector2 GetRelatedMove() {
			var input = _movement.ReadValue<Vector2>();

			var directionAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
			if (directionAngle < 0) {
				directionAngle += 360;
			}
			directionAngle += _camera.transform.eulerAngles.y;
			if (directionAngle > 360) {
				directionAngle -= 360;
			}
			var forward = Quaternion.Euler(0, directionAngle, 0) * Vector3.forward;
			var result = forward * input.magnitude;
			return new Vector2(result.x, result.z);
		}
		
		private void OnShootAction(InputAction.CallbackContext context) {
			Shoot?.Invoke();
		}
		private void OnEnable() {
			_movement.Enable();
			_shoot.Enable();
			_shoot.performed += OnShootAction;
		}
		private void OnDisable() {
			_movement.Disable();
			_shoot.Disable();
			_shoot.performed -= OnShootAction;
		}
	}
}