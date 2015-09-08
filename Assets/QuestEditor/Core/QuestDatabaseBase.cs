using UnityEngine;
using System.Collections.Generic;

namespace Adnc.Quest {
	public abstract class QuestDatabaseBase : ScriptableObject {
		public string title;
		
		[TextArea(3, 5)]
		public string description;

		public abstract List<QuestEntryBase> Quests { get; set; }

		public abstract System.Type QuestEntry { get; }
		public abstract System.Type QuestTask { get; }
	}
}
