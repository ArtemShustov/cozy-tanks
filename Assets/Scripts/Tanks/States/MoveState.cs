using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Tanks.States {
	public class MoveState: TankState {
		[Header("Settings")]
		[SerializeField] private float _linearSpeed = 2;
		[SerializeField] private float _rotationSpeed = 5;
		[Header("Transitions")]
		[SerializeField] private TankState _idleState;
		/*
		private void OnShootInput() {
			Tank.Shoot();
		}
		
		public override void OnUpdate() {
			if (Tank.Input.Throttle == 0) {
				Tank.Change(_idleState);
			}
		}
		public override void OnFixedUpdate() {
			var direction = Tank.Input.Direction;
			if (Tank.IsOnGround()) {
				Tank.MoveTo(direction, _linearSpeed, Time.deltaTime);
				Tank.RotateTo(direction, _rotationSpeed, Time.deltaTime);
			}
		}
		public override void OnEnter() {
			Tank.Input.Shoot += OnShootInput;
		}
		public override void OnExit() {
			Tank.Input.Shoot -= OnShootInput;
		}
		*/
	}
}