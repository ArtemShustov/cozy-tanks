using UnityEngine;

namespace Game.Tanks.States {
	public class IdleState: TankState {
		[Header("Transitions")]
		[SerializeField] private TankState _moveState;
		/*
		private void OnShootInput() {
			Tank.Shoot();
		}
		
		public override void OnUpdate() {
			if (Tank.Input.Throttle != 0) {
				Tank.Change(_moveState);
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