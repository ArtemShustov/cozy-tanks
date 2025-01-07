using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Tanks {
	public interface ITankInput {
		Vector2 Direction { get; }
		float Throttle { get; }

		event Action<InputActionPhase> Shoot;
	}
}