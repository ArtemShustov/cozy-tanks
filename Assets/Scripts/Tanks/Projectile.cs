using Game.Combat;
using Game.Effects;
using Game.Events;
using UnityEngine;

namespace Game.Tanks {
	[RequireComponent(typeof(Rigidbody))]
	public class Projectile: MonoBehaviour {
		[SerializeField] private float _destroyTime = 10;
		[SerializeField] private int _damage = 1;
		[SerializeField] private Effect _hitEffect;
		private bool _exploded = false;

		private void Awake() {
			Invoke(nameof(DestroySelf), _destroyTime);
		}

		public void SetForwardSpeed(float speed) {
			GetComponent<Rigidbody>().linearVelocity = speed * transform.forward;
		}
		public void SetDamage(int damage) {
			_damage = Mathf.Max(0, damage);
		}
		
		private void DestroySelf() {
			GameObject.Destroy(gameObject);
		}

		private void OnCollisionEnter(Collision collision) {
			if (_exploded) {
				return;
			}
			if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable)) {
				damageable.TakeDamage(_damage);
				EventBus<EffectEvent>.Raise(new EffectEvent(_hitEffect, transform.position, Quaternion.identity));
			}
			_exploded = true;
		}
	}
}