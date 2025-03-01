using UnityEngine;

namespace Game.Tanks {
	public class TankChassis : MonoBehaviour {
		[Header("Wheels settings")]
		[SerializeField] private float _sideFriction = 10;
		[Header("Components")]
		[SerializeField] private Rigidbody _rigidbody;

		private void FixedUpdate() {
			ApplySideFriction();
		}

		private void ApplySideFriction() {
			Vector3 localVelocity = transform.InverseTransformDirection(_rigidbody.linearVelocity);
			Vector3 frictionForce = new Vector3(-localVelocity.x * _sideFriction, 0, 0);
			Vector3 worldForce = transform.TransformDirection(frictionForce);
			_rigidbody.AddForceAtPosition(worldForce, transform.position, ForceMode.Acceleration);
		}
	}
}