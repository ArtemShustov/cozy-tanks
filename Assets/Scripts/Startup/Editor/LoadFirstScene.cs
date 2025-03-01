using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Game.Startup.Editor {
	[InitializeOnLoad]
	public static class LoadFirstScene {
		private const string TogglePrefKey = "Game.LoadFirstScene.Enabled";
		private const string MenuPath = "Tools/Load First Scene On Play";

		static LoadFirstScene() {
			EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
			Menu.SetChecked(MenuPath, IsEnabled);
		}

		[MenuItem(MenuPath)]
		private static void ToggleLoadFirstScene() {
			IsEnabled = !IsEnabled;
			Menu.SetChecked(MenuPath, IsEnabled);
		}

		private static bool IsEnabled {
			get => EditorPrefs.GetBool(TogglePrefKey, true);
			set => EditorPrefs.SetBool(TogglePrefKey, value);
		}

		private static void OnPlayModeStateChanged(PlayModeStateChange state) {
			if (state == PlayModeStateChange.ExitingEditMode) {
				if (!IsEnabled) {
					EditorSceneManager.playModeStartScene = null;
					return;
				}
				var scenePath = EditorBuildSettings.scenes[0].path;
				var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
				EditorSceneManager.playModeStartScene = sceneAsset;
			}
		}
	}
}