using Game.Events;
using UnityEngine;

namespace Game.Effects {
	public class EffectEvent: IGameEvent {
		public Effect Effect { get; private set; }
		public Vector3 Position { get; private set; }
		public Quaternion Rotation { get; private set; }

		public EffectEvent(Effect effect, Vector3 position, Quaternion rotation) {
			Effect = effect;
			Position = position;
			Rotation = rotation;
		}
	}
}