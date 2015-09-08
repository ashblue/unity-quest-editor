using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Adnc.Quest {
	public abstract class QuestWindowBase : EditorWindow {
		public abstract QuestDatabaseBase Database { get; set; }
		public virtual string WindowName { get { return "Quest Database"; } }
		public abstract QuestEntryBase QuestsTmp {  get; set; }

		GUIStyle titleStyle; // Style used for title in upper left
		int paddingSize = 15; // Total padding wrapping the window
		GUIStyle containerPadding;

		// @NOTE Must be implemented in the child class
//		[MenuItem("Window/Quest Editor")]
//		public static void ShowEditor () {
//			// Show existing window instance. If one doesn't exist, make one.
//			EditorWindow.GetWindow<typeof(this)>("Quest Editor");
//		}

		public virtual void OnEnable () {
			containerPadding = new GUIStyle();
			containerPadding.padding = new RectOffset(paddingSize, paddingSize, paddingSize, paddingSize);
			
			titleStyle = new GUIStyle();
			titleStyle.fontSize = 20;
		}

		public virtual void OnGui () {
			EditorGUILayout.BeginVertical(containerPadding); // BEGIN Padding

			if (Database == null) {
				GUILayout.Label(WindowName, titleStyle);
				GUILayout.Label("Please select a decision database from the assets and click the edit " +
				                "button in the inspector pannel (or create one if you haven't).");
				return;
			}

			EditorGUILayout.EndVertical(); // END Padding
		}

		public virtual void DisplayQuest (QuestEntryBase quest) {
			// Overridable quest dispaly logic here
		}
	}
}
