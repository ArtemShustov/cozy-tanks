using System;
using System.Linq;
using Game.Inputs;
using UnityEngine;

namespace Game {
	[Obsolete("Testing")]
	public class WheelTank: MonoBehaviour {
		[SerializeField] private SphereCollider[] _wheels;
		[SerializeField] private float _force;
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private PlayerInput _input;

		private void FixedUpdate() {
			RotateTo(_input.Direction, 5, Time.fixedDeltaTime);
			
			foreach (var wheel in _wheels) {
				var hits = Physics.SphereCastAll(
					wheel.transform.position, 
					wheel.radius, 
					Vector3.down, 
					0.05f
				);
				if (hits.Count(hit => hit.rigidbody != _rigidbody && hit.collider != wheel) > 0) {
					_rigidbody.AddForceAtPosition(
						transform.forward * (_input.Throttle * _force), 
						wheel.transform.position, 
						ForceMode.Impulse
					);
				}
			}
		}
		public void RotateTo(Vector2 direction, float speed, float deltaTime) {
			if (direction.sqrMagnitude < Mathf.Epsilon) return;
			
			Vector3 localDirection = _rigidbody.transform.InverseTransformDirection(new Vector3(direction.x, 0, direction.y));
			float targetAngle = Mathf.Atan2(localDirection.x, localDirection.z) * Mathf.Rad2Deg;
			
			float newAngle = Mathf.LerpAngle(0, targetAngle, speed * deltaTime);
			_rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(0, newAngle, 0));
		}
	}
}