using Game.Combat;
using UnityEngine;

namespace Game.Tanks {
    [SelectionBase]
	public class Tank: MonoBehaviour {
		[Header("Settings")]
		[SerializeField] private TankState _diedState;
		[Header("Components")]
		[SerializeField] private Health _health;
		[SerializeField] private TankGun _gun;
		[SerializeField] private TankStateMachine _machine;
		
		public TankStateMachine StateMachine => _machine;

		public void Kill() {
			_health.SetHealth(0);
		}
		
		private void OnEnable() {
			_health.Died += OnDied;
		}
		private void OnDisable() {
			_health.Died -= OnDied;
		}
		private void OnDied() {
			_machine.Change(_diedState);
			Destroy(gameObject, 5);
		}
	}
}