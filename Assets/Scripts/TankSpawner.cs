using Game.Inputs;
using Game.Tanks;
using UnityEngine;

namespace Game {
	public class TankSpawner: MonoBehaviour {
		[SerializeField] private TankInput _inputPrefab;
		[SerializeField] private Tank _tankPrefab;

		private void Start() {
			Spawn(transform, transform.position);
		}
		
		public Tank Spawn(Transform root, Vector3 position) {
			var tank = Instantiate(_tankPrefab, root);
			tank.transform.position = position;
			var input = Instantiate(_inputPrefab, tank.transform);
			tank.StateMachine.SetInput(input);
			return tank;
		}
	}
}