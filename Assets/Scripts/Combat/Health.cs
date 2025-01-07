using System;
using Mirror;
using UnityEngine;

namespace Game.Combat {
	public class Health: NetworkBehaviour, IDamageable {
		[Min(1)]
		[SerializeField] private int _maxValue = 1;
		
		[SyncVar(hook = nameof(OnHealthChanged))] 
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
			if (isServer) {
				_value = Mathf.Clamp(value, 0, _maxValue);
			} else {
				CmdSetHealth(value);
			}
		}
		public void TakeDamage(int value) {
			if (isServer) {
				_value = Mathf.Clamp(_value - value, 0, _maxValue);
			} else {
				CmdTakeDamage(value);
			}
		}
		
		[Command]
		private void CmdSetHealth(int value) {
			_value = Mathf.Clamp(value, 0, _maxValue);
		}
		[Command]
		private void CmdTakeDamage(int value) {
			_value = Mathf.Clamp(_value - value, 0, _maxValue);
		}

		public override void OnStartClient() {
			base.OnStartClient();
			ValueChanged?.Invoke(this);
		}
		private void OnHealthChanged(int oldValue, int newValue) {
			ValueChanged?.Invoke(this);
			if (_value <= 0) {
				Died?.Invoke();
			}
		}
	}
}