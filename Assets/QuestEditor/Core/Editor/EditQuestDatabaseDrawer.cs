using UnityEngine;
using UnityEditor;

namespace Adnc.Quest {
	[CustomPropertyDrawer(typeof(EditQuestDatabaseAttribute))]
	public class EditQuestDatabaseDrawer : PropertyDrawer {
		EditQuestDatabaseAttribute dbDetails { get { return ((EditQuestDatabaseAttribute)attribute); } }

		public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label) {
			if (GUI.Button(position, prop.stringValue)) {
				object[] paramDetails = new object[] { prop.serializedObject.targetObject };
				dbDetails.windowType.GetMethod("SetDatabase").Invoke(null, paramDetails);
				dbDetails.windowType.GetMethod("ShowEditor").Invoke(null, null);
			}
		}
		
		
	}
}
