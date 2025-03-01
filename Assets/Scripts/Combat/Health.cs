using System;
using UnityEngine;

namespace Game.Combat {
	public class Health: MonoBehaviour, IDamageable {
		[Min(1)]
		[SerializeField] private int _maxValue = 1;
		
		private int _value;
		
		public int Value => _value;
		public int Max => _maxValue;
		public bool IsMax => _value >= _maxValue;
		
		public event Action<Health> ValueChanged;

		private void Awake() {
			_value = _maxValue;
		}

		public void SetHealth(int value) {
			_value = Mathf.Clamp(value, 0, _maxValue);
			ValueChanged?.Invoke(this);
		}
		public void TakeDamage(int value) {
			_value = Mathf.Clamp(_value - value, 0, _maxValue);
			ValueChanged?.Invoke(this);
		}
	}
}