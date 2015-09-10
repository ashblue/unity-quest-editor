using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Adnc.Quest {
	public class QuestController : MonoBehaviour {
		[SerializeField] QuestDatabase defenitions;

		Dictionary<string, QuestEntryDetails> questLib = new Dictionary<string, QuestEntryDetails>();
		Dictionary<string, QuestTaskDetails> questTaskLib = new Dictionary<string, QuestTaskDetails>();

		public QuestConditions status;

		void Awake () {
			// Populate defenition dictionary with error checking
			foreach (QuestEntry quest in defenitions.Quests) {
				Debug.Assert(string.IsNullOrEmpty(quest.id), string.Format("QuestEntry {0} is missing an ID", quest.displayName));
				Debug.Assert(questLib.ContainsKey(quest.id), string.Format("Duplicate QuestEntry ID on {0}", quest.displayName));
				questLib[quest.id] = new QuestEntryDetails {
					defenition = quest
				};

				foreach (QuestTask task in quest.Tasks) {
					Debug.Assert(string.IsNullOrEmpty(task.id), string.Format("QuestTask {0} is missing an ID", task.displayName));
					Debug.Assert(questTaskLib.ContainsKey(task.id), string.Format("Duplicate QuestTask ID on {0}", task.displayName));
					questTaskLib[task.id] = new QuestTaskDetails {
						defenition = task
						// @TODO Set task index
					};
				}
			}

			status = new QuestConditions {
				ctrl = this
			};
		}

		// @TODO Test and make sure sorting actually works how it should
		// Returns a list of all currently active quests
		public List<QuestEntryDetails> GetActive () {
			List<QuestEntryDetails> active = new List<QuestEntryDetails>();
			foreach (QuestEntry q in defenitions.Quests) {
				QuestEntryDetails quest = _GetQuest(q.id);
				if (quest.status != QuestStatus.Undefined) active.Add(quest);
			}

			// Sort by start date, priorty, ongoing items
			active.OrderByDescending(q => q.startTime)
				.OrderBy(q => q.defenition.sideQuest)
				.OrderByDescending(q => q.status == QuestStatus.Ongoing);

			return active;
		}

		public QuestEntryDetails _GetQuest (string id) {
			QuestEntryDetails quest;
			if (questLib.TryGetValue(id, out quest)) {
				return quest;
			} else {
				Debug.LogErrorFormat("Quest ID {0} does not exist. Please fix.", id);
				return null;
			}
		}

		public QuestTaskDetails _GetQuest (string id, string taskId) {
			QuestEntryDetails quest = _GetQuest(id);
			QuestTaskDetails task = _GetTask(id);

			if (quest == null || task == null) return null;

			if (quest.defenition.Tasks.Contains(task.defenition)) {
				return task;
			} else {
				Debug.LogErrorFormat("Quest ID {0} does not contain task ID {1}. Please fix.", id, taskId);
				return null;
			}
		}

		/// <summary>
		/// Gets the task. Kept private to prevent trying to access tasks individually and accidentally altering the
		/// wrong quest.
		/// </summary>
		/// <returns>The task.</returns>
		/// <param name="id">Identifier.</param>
		public QuestTaskDetails _GetTask (string id) {
			QuestTaskDetails task;
			if (questTaskLib.TryGetValue(id, out task)) {
				return task;
			} else {
				Debug.LogErrorFormat("Quest task ID {0} does not exist. Please fix.", id);
				return null;
			}
		}

		void SetQuest (string id, QuestStatus status) {
			QuestEntryDetails quest = _GetQuest(id);
			if (quest != null) {
				quest.status = status;
			}
		}

		void SetQuest (string id, string idTask, QuestStatus status) {
			QuestTaskDetails task = _GetQuest(id, idTask);
			if (task != null) {
				task.status = status;
			}
		}

		// User boots up and starts a quest
		public void BeginQuest (string id) {
			// Get the quest and first task, mark them as ongoing, set start timestamp
			QuestEntryDetails quest = _GetQuest(id);
			if (quest != null) {
				quest.status = QuestStatus.Ongoing;
				quest.startTime = System.DateTime.Now;
				if (quest.defenition.Tasks != null && quest.defenition.Tasks.Count > 0) {
					QuestTaskDetails task = _GetQuest(id, quest.defenition.Tasks[0].id);
					if (task != null) {
						// @TODO Poll the requirements of the task to check if they've already been completed
						task.status = QuestStatus.Ongoing;
						task.startTime = System.DateTime.Now;
					}
				}
			}
		}

		// @TODO Make abstract to force the implementation
		public virtual void Reward (QuestEntryDetails quest) {
			// Distribute rewards from the quest, show notifications, ect.
		}

		public void EndQuest (string id, bool success = true) {
			QuestEntryDetails quest = _GetQuest(id);
			if (quest == null) return;

			if (success) {
				quest.status = QuestStatus.Success;
				Reward(quest);
			} else {
				quest.status = QuestStatus.Failure;
			}
		}

		// @TODO Incomplete
		public void NextTask (string id) {
			// Update current quest task and mark as success

			// No more quests? Mark as success and end quest
			// Mark next quest task as ongoing
		}

		// @TODO Incomplete
		// Checks if the designated quest task requirements are complete. Meant to be run when talking with specific characters,
		// interacting with items, or at specific locations.
		public bool CheckQuestTask (string id, string idKey) {
			// Skips the item if it isn't ongoing

			// Checks if the target quest task requirements are complete
			// If so it will run NextTask

			// Only returns true if the quest check occured and was successful for the first time
			return true;
		}

		// @NOTE Update quest doesn't necessarily make sense, we will never need to individually call a quest task ID
		// The quests will always be updated in sequential order (1, 2, 3, 4, 5)
//		public void UpdateQuest (string id, string idKey, QuestStatus status) {
			// Set the current task index to this key

			// status success / fail, run EndQuest
			// status ongoing 

			// Quest must be ongoing in order to update it
			// Mark all previous quest entry statuses as success

			// If we are updating the final task, also mark the parent as success or fail
//		}
	}
}

