using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace Adnc.Quest {
	public class QuestWindow : QuestWindowBase<QuestDatabase, QuestEntry> {
		string filter;
		int deleteIndex = -1;

		// Loop vars
		QuestEntry quest;
		bool errorId;

		[MenuItem("Window/Quest Editor")]
		public static void ShowEditor () {
			EditorWindow.GetWindow<QuestWindow>("Quest Editor");
		}

		public override void Header () {
			EditorGUILayout.BeginHorizontal();
			
			EditorGUI.BeginChangeCheck();
			
			GUILayout.Label(string.Format("{0}: {1}", DisplayName, database.title), titleStyle);
			
			GUI.SetNextControlName("Filter");
			if (GUI.GetNameOfFocusedControl() == "Filter") {
				filter = EditorGUILayout.TextField(filter);
			} else {
				EditorGUILayout.TextField("Filter");
			}
			
			if (EditorGUI.EndChangeCheck()) {
				FilterDecisions(filter);
			}
			
			EditorGUILayout.EndHorizontal();
			
			if (GUILayout.Button("Add Quest")) AddQuest();
		}


		public override void DisplayDatabase () {
			EditorGUI.BeginChangeCheck();

			for (int i = 0, l = questTmp.Count; i < l; i++) {
				quest = questTmp[i];
				
				errorId = string.IsNullOrEmpty(quest.id);
//				
				if (!errorId) {
					quest.expanded = EditorGUILayout.Foldout(quest.expanded, quest.displayName);
				} else {
					quest.expanded = EditorGUILayout.Foldout(quest.expanded, 
                                     	string.Format("{0}: ID cannot be left blank and must be unique", quest.displayName), 
                                    	errorFoldoutStyle);
				}
				
				if (quest.expanded) {
					BeginIndent(20f);
					
					quest.displayName = EditorGUILayout.TextField("Display Name", quest.displayName);
					quest.id = EditorGUILayout.TextField("ID", quest.id);
//					quest.defaultValue = EditorGUILayout.Toggle("Default Value", quest.defaultValue);
//					
//					EditorGUILayout.LabelField("Notes");
//					quest.notes = GUILayout.TextArea(quest.notes, GUILayout.MaxHeight(60f), GUILayout.Width(300f));
//					
//					if (GUILayout.Button(string.Format("Remove '{0}'", quest.displayName))) {
//						if (ConfirmDelete(quest.displayName)) {
//							deleteIndex = i;
//						}
//					}
//					
					EndIndent();
				}
			}

			if (EditorGUI.EndChangeCheck()) {
				EditorUtility.SetDirty(database);
			}
			
			if (deleteIndex != -1) {
				RemoveQuest(deleteIndex);
				deleteIndex = -1;
			}
		}

		void AddQuest () {
			database.Quests.Insert(0, new QuestEntry());
			
			FilterDecisions(filter);
			EditorUtility.SetDirty(database);
		}

		void RemoveQuest (int index) {
			database.Quests.RemoveAt(index);
			FilterDecisions(filter);
			EditorUtility.SetDirty(database);
		}
		
		void FilterDecisions (string search) {
			if (string.IsNullOrEmpty(search)) {
				questTmp = database.Quests;
				return;
			}
			
			string[] searchBits = search.ToLower().Split(' ');
			List<QuestEntry> matches = database.Quests.Where(d => searchBits.All(n => d.displayName.ToLower().Contains(n))).ToList();
			
			questTmp = matches;
		}

		public override void OnSelectionChange () {
			Object current = Selection.activeObject;
			if (current != null && current.GetType() == typeof(QuestDatabase)) {
				SetDatabase(Selection.activeObject as QuestDatabase);
			}
		}

		public void SetDatabase (QuestDatabase db) {
			database = db;
			questTmp = db.Quests;
		}
	}
}
