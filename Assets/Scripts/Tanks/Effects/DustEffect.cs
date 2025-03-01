using UnityEngine;

namespace Game.Tanks.Effects {
	public class DustEffect: MonoBehaviour {
		[SerializeField] private ParticleSystem _particle;
		[SerializeField] private Tank _tank;

		private bool _isActive;

		private void Awake() {
			_particle.Stop();
		}
		private void Update() {
			var isOnGround = _tank.Movement.IsOnGround();
			if (_isActive == isOnGround) {
				return;
			}
			
			_isActive = isOnGround;
			if (_isActive) {
				_particle.Play();
			} else {
				_particle.Stop();
			}
		}
	}
}