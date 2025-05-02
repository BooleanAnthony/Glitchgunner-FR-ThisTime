using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BossFights {
	public class MiniBoss : MonoBehaviour
	{
		int correct_answer = 0;
		int current_question = -1;
		[SerializeField] public GameObject[] weakpoints;
		[SerializeField] TMP_Text question_text;
		[SerializeField] TMP_Text question_status;
		[SerializeField] string nextScene;
		[SerializeField] ScriptActivator activator;
		[SerializeField] Health playerHealth;
		[SerializeField] JSONReader jsonReader;
		
		private InvincibilityFlicker flicker;

		private int questions_size;

		IEnumerator Start() {
			yield return new WaitForSeconds(1f);
			// Time to get shit done.
			questions_size = jsonReader.questions.Length;

			for (int i = 0; i < weakpoints.Length; i++) {
				weakpoints[i].GetComponent<WeakpointChoice>().InitializeWeakpoint(this, i);
			}

			flicker = GetComponent<InvincibilityFlicker>();

			NextQuestion();
		}

		void NextQuestion() {
			current_question++;
			
			if (current_question < questions_size) {
				correct_answer = jsonReader.questions[current_question].correct_answer;

				for (int i = 0; i < weakpoints.Length; i++) {
					WeakpointChoice w = weakpoints[i].GetComponent<WeakpointChoice>();
					weakpoints[i].SetActive(true);
				
					w.SetText(jsonReader.questions[current_question].choices[i]);
					w.SetHealth(100);
				}

				question_text.SetText($"Question {current_question + 1}:\n{jsonReader.questions[current_question].questionInput}");
			}
			else {
				EndBossFight();
			}

			if (current_question == questions_size-1)
			{
				activator.ActivateAllScripts();
			}
		}

		void EndBossFight() {
			print("Bossfight over!");

			gameObject.SetActive(false);
			question_text.gameObject.SetActive(false);
			SceneManager.LoadScene(nextScene); //loads the game scene upon death
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
			WeakpointChoice weakpoint = weakpoints[answer].GetComponent<WeakpointChoice>();
			yield return new WaitForSeconds(1);

			question_status.gameObject.SetActive(true);
			if (answer == correct_answer) {
				question_status.SetText($"Correct!");
				playerHealth.HealDamage(1);
				weakpoint.StartCoroutine("Death");
			}
			else {
				question_status.SetText($"Wrong!\nAnswer: Weakpoint {correct_answer + 1}");
				weakpoint.StartCoroutine("ShotgunFire");
			}

			yield return new WaitForSeconds(3);
			question_status.gameObject.SetActive(false);

			NextQuestion();

			if (current_question < questions_size) {
				flicker.StartCoroutine("Flicker");
				yield return new WaitForSeconds(5);
				for (int i = 0; i < weakpoints.Length; i++) 
				{
					WeakpointChoice w = weakpoints[i].GetComponent<WeakpointChoice>();
					w.invulnerable = false;
				}
			}
		}
    }
}
