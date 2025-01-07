using System;
using Game.Tanks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Inputs {
	public class AIInput: TankInput {
		[SerializeField] private float _maxDist = 20;
		private Vector3 _target;
		private Vector3 _direction;

		public override Vector2 Direction => _direction;
		public override float Throttle => 1;
		public override event Action<InputActionPhase> Shoot;

		private void Awake() {
			_target = transform.position;
		}
		private void Update() {
			if (Vector3.Distance(transform.position, _target) < 1) {
				var random = UnityEngine.Random.insideUnitCircle;
				_target = new Vector3(random.x, 0, random.y) * _maxDist;
			} 
			var direction = _target -transform.position;
			_direction = new Vector2(direction.x, direction.z);
		}
		private void OnDrawGizmosSelected() {
			Gizmos.DrawWireSphere(_target, 1);
			var direction = new Vector3(Direction.x, 0, Direction.y).normalized * 2;
			Gizmos.DrawLine(transform.position, transform.position + direction);
			Gizmos.DrawLine(transform.position, _target);
		}
	}
}