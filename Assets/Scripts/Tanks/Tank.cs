using Game.Combat;
using Mirror;
using UnityEngine;

namespace Game.Tanks {
    [SelectionBase]
	public class Tank: NetworkBehaviour {
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
		
		private void OnZeroHealth() {
			_machine.Change(_diedState);
			Destroy(gameObject, 5);
		}
		private void OnEnable() {
			_health.Died += OnZeroHealth;
		}
		private void OnDisable() {
			_health.Died -= OnZeroHealth;
		}
	}
}