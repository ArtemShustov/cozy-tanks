using Game.Combat;
using Game.Effects;
using Game.Events;
using Mirror;
using UnityEngine;

namespace Game.Tanks {
	[RequireComponent(typeof(Rigidbody))]
	public class Projectile: NetworkBehaviour {
		[SerializeField] private float _destroyTime = 10;
		[SerializeField] private int _damage = 1;
		[SerializeField] private Effect _hitEffect;
		private bool _exploded = false;
		
		public void SetForwardSpeed(float speed) {
			GetComponent<Rigidbody>().linearVelocity = speed * transform.forward;
		}
		public void SetDamage(int damage) {
			_damage = Mathf.Max(0, damage);
		}
		
		private void DestroySelf() {
			NetworkServer.Destroy(gameObject);
		}

		[ClientRpc]
		private void RpcHit() {
			EventBus<EffectEvent>.Raise(new EffectEvent(_hitEffect, transform.position, Quaternion.identity));
		}
		
		[ServerCallback]
		private void OnCollisionEnter(Collision collision) {
			if (_exploded) {
				return;
			}
			if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable)) {
				damageable.TakeDamage(_damage);
				RpcHit();
			}
			_exploded = true;
		}
		public override void OnStartServer() {
			base.OnStartServer();
			Invoke(nameof(DestroySelf), _destroyTime);
		}
	}
}