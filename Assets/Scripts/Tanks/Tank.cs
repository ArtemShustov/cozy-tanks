using UnityEngine;

namespace Game.Tanks {
	public class Tank: MonoBehaviour {
		[field: SerializeField] public TankMovement Movement { get; private set; }
	}
}