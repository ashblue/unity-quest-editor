using UnityEngine;
using System.Collections.Generic;

namespace Adnc.Quest {
	[System.Serializable]
	public abstract class QuestDatabaseBase<T> : ScriptableObject {
		public string title = "Untitled";
		
		[TextArea(3, 5)]
		public string description;

		public abstract List<T> Quests { get; set; }
//		public abstract System.Type QuestEntry { get; }
//		public abstract System.Type QuestTask { get; }
	}
}
