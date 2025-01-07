using UnityEngine;
using UnityEngine.InputSystem;

namespace Game {
	public class FrameLimit: MonoBehaviour {
		[SerializeField] private InputAction _button = new InputAction("Button", InputActionType.Button, "<Keyboard>/F6");
		[SerializeField] private int[] _modes = new[] { 15, 30, 60, 120, -1 };

		private int _current = 0;

		private void Awake() {
			Application.targetFrameRate = -1;
		}
		
		private void OnButton(InputAction.CallbackContext obj) {
			_current = _current + 1 >= _modes.Length ? 0 : _current + 1;
			Application.targetFrameRate = _modes[_current];
		}
		private void OnEnable() {
			_button.Enable();
			_button.performed += OnButton;
		}
		private void OnDisable() {
			_button.Disable();
			_button.performed -= OnButton;
		}
		private void OnGUI() {
			var style = new GUIStyle();
			style.fontSize = 48;
			GUILayout.Label($"Target FPS: {Application.targetFrameRate}", style);
			GUILayout.Label($"Current FPS: {Mathf.RoundToInt(1f / Time.deltaTime)}", style);
		}
	}
}