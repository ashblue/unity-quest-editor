using UnityEngine;
using System.Collections.Generic;

namespace Adnc.Quest {
	public abstract class QuestEntryBase : QuestBase {
		public string notes = "";
		public string successMessage = "";
		public string failMessage = "";

		public abstract List<QuestTaskBase> Tasks { get; set; }
	}
}
