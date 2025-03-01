using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
	public class TestMenu : MonoBehaviour {
		[SerializeField] private TMP_InputField _nameField;
		[SerializeField] private Button _createButton;
		[SerializeField] private Button _joinButton;

		private void Join() {
			NetworkManager.singleton.StartClient();
		}
		private void Create() {
			NetworkManager.singleton.StartHost();
		}

		private void OnEnable() {
			_createButton.onClick.AddListener(Create);
			_joinButton.onClick.AddListener(Join);
		}
		private void OnDisable() {
			_createButton.onClick.RemoveListener(Create);
			_joinButton.onClick.RemoveListener(Join);
		}
	}
}