using UnityEngine;

namespace Game {
	public class DestroyAfter: MonoBehaviour {
		[SerializeField] private float _time = 10;

		private void Awake() {
			Destroy(gameObject, _time);
		}
	}
}