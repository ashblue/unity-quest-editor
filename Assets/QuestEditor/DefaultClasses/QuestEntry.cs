using UnityEngine;
using System.Collections.Generic;

namespace Adnc.Quest {
	[System.Serializable]
	public class QuestEntry : QuestEntryBase<QuestTask> {
		public bool sideQuest;

		public List<QuestTask> _tasks = new List<QuestTask>();
		public override List<QuestTask> Tasks {
			get {
				return _tasks;
			}
			set {
				_tasks = value;
			}
		}
	}
}
