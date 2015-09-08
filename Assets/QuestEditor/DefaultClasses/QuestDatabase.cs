using UnityEngine;
using System.Collections.Generic;

namespace Adnc.Quest {
	[System.Serializable]
	public class QuestDatabase : QuestDatabaseBase<QuestEntry> {
		[HideInInspector]
		public List<QuestEntry> _quests = new List<QuestEntry>();
		public override List<QuestEntry> Quests {
			get {
				return _quests;
			}
			set {
				_quests = value;
			}
		}
	}
}
