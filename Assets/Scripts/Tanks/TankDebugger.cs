using System.Text;
using UnityEngine;

namespace Game.Tanks {
	[RequireComponent(typeof(TankStateMachine))]
	public class TankDebugger: MonoBehaviour {
		[Range(0, 128)]
		[SerializeField] private int _fontSize = 48;
		[SerializeField] private TankState _defaultState;
		private TankStateMachine _tank;
		private GUIStyle _style;
		
		private void Awake() {
			_tank = GetComponent<TankStateMachine>();
			_style = new GUIStyle();
			_style.fontSize = _fontSize;
			_style.alignment = TextAnchor.UpperCenter;
		}
		private void Start() {
			if (_defaultState != null && _tank.CurrentState == null) {
				_tank.Change(_defaultState);
			}
		}

		private void OnGUI() {
			var position = Camera.main.WorldToScreenPoint(transform.position);
			var rect = new Rect(position.x, Screen.height - position.y, 0, 0);
			var text = new StringBuilder();

			text.AppendLine("-[Tank]-");
			text.AppendLine($"State: {_tank.CurrentState}");
			text.AppendLine($"Input: {_tank.Input?.Direction}({_tank.Input?.Throttle})");
			
			GUI.Label(rect, text.ToString(), _style);
		}
	}
}