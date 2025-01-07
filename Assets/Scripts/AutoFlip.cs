using Game.Tanks;
using UnityEngine;

namespace Game {
	public class AutoFlip: MonoBehaviour {
		[SerializeField] private float _time = 2;
		[SerializeField] private TankStateMachine _tank;

		private float _timer;
		
		private void FixedUpdate() {
			if (_tank.IsStuck()) {
				_timer += Time.fixedDeltaTime;
			} else {
				_timer = 0;
			}
			if (_timer >= _time) {
				var random = Random.insideUnitCircle;
				_tank.transform.position = transform.position + new Vector3(random.x, 1, random.y);
				_tank.transform.rotation = Quaternion.identity;
				_tank.Rigidbody.linearVelocity = Vector3.zero;
				_timer = 0;
			}
		}
	}
}