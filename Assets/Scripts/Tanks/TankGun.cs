using Game.Effects;
using Game.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Tanks {
	public class TankGun : MonoBehaviour {
		[Header("Settings")]
		[SerializeField] private float _force = 25f;
		[SerializeField] private float _reloadTime = 2f;
		[Header("Components")]
		[SerializeField] private Effect _shootEffect;
		[SerializeField] private Projectile _prefab;
		[SerializeField] private Transform _root;
		[SerializeField] private TankStateMachine _tank;

		private float _reloadTimer = 0f;
		public float ReloadProgress => Mathf.Clamp01(1f - _reloadTimer / _reloadTime);

		private void Update() {
			if (_reloadTimer > 0) {
				_reloadTimer = Mathf.Max(0f, _reloadTimer - Time.deltaTime);
			}
		}

		public void Shoot() {
			if (_reloadTimer > 0) {
				return;
			}
            
			var instance = Instantiate(_prefab);
			instance.transform.position = _root.position;
			instance.transform.forward = _root.forward;
			instance.SetForwardSpeed(_force);
			
			EventBus<EffectEvent>.Raise(new EffectEvent(_shootEffect, transform.position, transform.rotation));
            
			_reloadTimer = _reloadTime;
		}
		
		private void OnShootInput(InputActionPhase phase) {
			if (phase != InputActionPhase.Performed) {
				return;
			}
			Shoot();
		}
		private void OnEnable() {
			_tank.Input.Shoot += OnShootInput;
		}
		private void OnDisable() {
			_tank.Input.Shoot -= OnShootInput;
		}
	}
}