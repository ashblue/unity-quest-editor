using UnityEngine;
using System.Collections;

namespace Adnc.Quest {
	public abstract class QuestBase {
		public string id = "";
		public string displayName = "";
		public QuestStatus status = QuestStatus.Undefined;
		public string description = "";
	}
}
