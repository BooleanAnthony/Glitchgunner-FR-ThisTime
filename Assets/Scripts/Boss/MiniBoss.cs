using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace BossFights {
	public class MiniBoss : MonoBehaviour
	{
		int correct_answer = 0;
		int current_question = -1;
		[SerializeField] public GameObject[] weakpoints;
		[SerializeField] public Question[] questions;
		[SerializeField] TMP_Text question_text;
		[SerializeField] TMP_Text question_status;

		private int questions_size;

		void Start() {
			// Time to get shit done.
			questions_size = questions.Length;

			for (int i = 0; i < weakpoints.Length; i++) {
				weakpoints[i].GetComponent<WeakpointChoice>().InitializeWeakpoint(this, i);
			}

			NextQuestion();
		}

		void NextQuestion() {
			current_question++;
			
			if (current_question < questions_size) {
				correct_answer = questions[current_question].correct_answer;

				for (int i = 0; i < weakpoints.Length; i++) {
					WeakpointChoice w = weakpoints[i].GetComponent<WeakpointChoice>();
					weakpoints[i].SetActive(true);

					w.SetText(questions[current_question].choices[i]);

					w.invulnerable = false;
					w.SetHealth(100);
				}

				question_text.SetText($"Question {current_question + 1}:\n{questions[current_question].question}");
			}
			else {
				EndBossFight();
			}
		}

		void EndBossFight() {
			print("Bossfight over!");

			gameObject.SetActive(false);
			question_text.gameObject.SetActive(false);
		}

		public void SubmitAnswer(int answer) {
			print($"Got an answer! Answer submitted was {answer}");

			foreach (GameObject weakpoint in weakpoints) {
				WeakpointChoice w = weakpoint.GetComponent<WeakpointChoice>();

				w.invulnerable = true;
			}

			StartCoroutine("ProcessAnswer", answer);
		}

		IEnumerator ProcessAnswer(int answer) {
			yield return new WaitForSeconds(1);

			question_status.gameObject.SetActive(true);
			if (answer == correct_answer) {
				question_status.SetText($"Correct!");
			}
			else {
				question_status.SetText($"Wrong!\nAnswer: Weakpoint {correct_answer + 1}");
			}

			yield return new WaitForSeconds(3);
			question_status.gameObject.SetActive(false);

			NextQuestion();
		}
	}

	[System.Serializable]
	public struct Question {
		public string question;
		public string[] choices;
		public int correct_answer;
	}
}
