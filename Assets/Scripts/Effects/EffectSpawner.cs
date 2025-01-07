using Game.Events;
using UnityEngine;

namespace Game.Effects {
	public class EffectSpawner: MonoBehaviour {
		private void OnEvent(EffectEvent gameEvent) {
			gameEvent.Effect.Spawn(transform, gameEvent.Position, gameEvent.Rotation);
		}
		private void OnEnable() {
			EventBus<EffectEvent>.Event += OnEvent;
		}
		private void OnDisable() {
			EventBus<EffectEvent>.Event -= OnEvent;
		}
	}
}