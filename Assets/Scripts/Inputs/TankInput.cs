using System;
using Game.Tanks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Inputs {
	public abstract class TankInput: MonoBehaviour, ITankInput {
		public abstract Vector2 Direction { get; }
		public abstract float Throttle { get; }
		public abstract event Action<InputActionPhase> Shoot;
	}
}