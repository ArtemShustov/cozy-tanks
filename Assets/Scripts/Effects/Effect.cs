using UnityEngine;

namespace Game.Effects {
	[CreateAssetMenu(menuName = "Effects/Effect")]
	public class Effect: ScriptableObject {
		[SerializeField] private GameObject _prefab;

		public GameObject Spawn(Transform root, Vector3 position, Quaternion rotation) {
			var instance = Instantiate(_prefab, root);
			instance.transform.position = position;
			instance.transform.rotation = rotation;
			return instance;
		}
	}
}