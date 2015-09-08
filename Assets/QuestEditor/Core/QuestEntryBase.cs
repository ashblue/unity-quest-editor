using UnityEngine;
using System.Collections.Generic;

namespace Adnc.Quest {
	[System.Serializable]
	public abstract class QuestEntryBase<T> : QuestBase {
		public string notes = "";
		public string successMessage = "";
		public string failMessage = "";
		public bool expanded = false;

		public abstract List<T> Tasks { get; set; }
	}
}
