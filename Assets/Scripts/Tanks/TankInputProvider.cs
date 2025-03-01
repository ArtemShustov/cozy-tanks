using System;
using UnityEngine;

namespace Game.Tanks {
	public class TankInputProvider: ITankInput {
		private ITankInput _input;
		
		public Vector2 Direction => _input?.Direction ?? Vector2.zero;
		public float Throttle => _input?.Throttle ?? 0;
		public event Action Shoot;

		public TankInputProvider() { }
		public TankInputProvider(ITankInput input) {
			SetInput(input);
		}

		public void SetInput(ITankInput input) {
			if (_input != null) {
				UnsubscribeAll(_input);
			}
			_input = input;
			SubscribeAll(input);
		}
		private void SubscribeAll(ITankInput input) {
			input.Shoot += OnShoot;
		}
		private void UnsubscribeAll(ITankInput input) {
			input.Shoot -= OnShoot;
		}
		
		private void OnShoot() {
			Shoot?.Invoke();
		}
	}
}