using UnityEngine;

namespace Game.Tanks.States {
	public class IdleState: TankState {
		[Header("Transitions")]
		[SerializeField] private TankState _moveState;
		
		public override void OnUpdate() {
			if (Tank.Input.Throttle != 0) {
				Tank.Change(_moveState);
			}
		}
	}
}