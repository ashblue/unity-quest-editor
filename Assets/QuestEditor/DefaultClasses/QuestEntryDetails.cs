using UnityEngine;
using System.Collections;

namespace Adnc.Quest {
	public class QuestEntryDetails : QuestDetailsBase<QuestEntry> {
		public int currentTaskIndex = 0; // Index of the current task

		public string Description {
			get {
				return "Figure out what the current task and status is, return details based upon that.";

				// Get default description and tack on additional details

				// if success / fail / abandoned
				// return success or fail message

				// if ongoing, get the currently ongoing task description
			}
		}
	}
}
