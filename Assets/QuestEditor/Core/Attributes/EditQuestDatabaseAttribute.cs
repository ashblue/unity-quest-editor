using UnityEngine;
using System.Collections;

public class EditQuestDatabaseAttribute : PropertyAttribute {
	public readonly System.Type windowType;

	public EditQuestDatabaseAttribute (System.Type windowType) {
		this.windowType = windowType;
	}
}
