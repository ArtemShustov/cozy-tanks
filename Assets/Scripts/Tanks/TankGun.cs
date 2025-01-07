using Game.Effects;
using Game.Events;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Tanks {
	public class TankGun : NetworkBehaviour {
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

		[Client]
		private void Shoot() {
			if (_reloadTimer > 0) {
				return;
			}
			CmdShoot();
			_reloadTimer = _reloadTime;
		}
		[Command]
		private void CmdShoot() {
			var instance = Instantiate(_prefab);
			instance.transform.position = _root.position;
			instance.transform.forward = _root.forward;
			instance.SetForwardSpeed(_force);
			NetworkServer.Spawn(instance.gameObject);
			RpcShoot();
		}
		[ClientRpc]
		private void RpcShoot() {
			EventBus<EffectEvent>.Raise(new EffectEvent(_shootEffect, transform.position, transform.rotation));
		}

		private void OnShootInput(InputActionPhase phase) {
			if (!isLocalPlayer) return;
			if (phase != InputActionPhase.Performed) return;
			
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