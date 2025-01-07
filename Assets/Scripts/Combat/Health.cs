using System;
using UnityEngine;

namespace Game.Combat {
	public class Health: MonoBehaviour, IDamageable {
		[Min(1)]
		[SerializeField] private int _maxValue = 1;
		[SerializeField] private bool _invulnerable = false;
		private int _value;
		
		public int Value => _value;
		public int Max => _maxValue;
		public bool IsMax => _value >= _maxValue;
		
		public event Action<Health> ValueChanged;
		public event Action Died;

		private void Awake() {
			_value = _maxValue;
		}
		
		public void SetHealth(int value) {
			_value = Mathf.Clamp(value, 0, _maxValue);
			ValueChanged?.Invoke(this);
			if (_value <= 0) {
				Died?.Invoke();
			}
		}
		public void TakeHeal(int value) {
			var damage = Mathf.Abs(value);
			SetHealth(_value + damage);
		}
		public void TakeDamage(int value) {
			if (_invulnerable) {
				return;
			}
			var damage = Mathf.Abs(value);
			SetHealth(_value - damage);
		}
		public void SetInvulnerable(bool invulnerable) {
			_invulnerable = invulnerable;
		}
	}
}