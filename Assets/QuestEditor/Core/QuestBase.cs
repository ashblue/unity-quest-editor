using UnityEngine;
using System.Collections;

namespace Adnc.Quest {
	[System.Serializable]
	public abstract class QuestBase {
		public string id = "";
		public string displayName = "Untitled";
		public QuestStatus status = QuestStatus.Undefined;
		public string description = "";
		public bool _expanded = false; // Whether or not this item should be expanded in the editor
	}
}
