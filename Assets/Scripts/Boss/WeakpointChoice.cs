using System.Collections;
using TMPro;
using UnityEngine;

namespace BossFights {
	public class WeakpointChoice : MonoBehaviour
	{
		[SerializeField] float health = 100;
		[SerializeField] public TMP_Text text;
		[SerializeField] public GameObject shotgunPoint;

		private SpriteRenderer sprite_renderer;
		private MiniBoss boss_parent;

		public bool invulnerable;
		private int choice;

		private void Start() {
			sprite_renderer = GetComponent<SpriteRenderer>();
		}

        public void InitializeWeakpoint(MiniBoss boss, int c) {
			boss_parent = boss;
			choice = c;
		}

		public void BreakWeakpoint() {
			boss_parent.StartCoroutine("SubmitAnswer", choice);
		}

		private IEnumerator ShotgunFire()
		{
			shotgunPoint.SetActive(true);
			yield return new WaitForSeconds(0);
			shotgunPoint.SetActive(false);
			gameObject.SetActive(false);
		}

		private IEnumerator Death()
		{
			yield return new WaitForSeconds(0);
			gameObject.SetActive(false);
		}

		public void ChangeHealth(float h) {
			if (!invulnerable)
			{
				health += h;
			}

			if (health <= 0) {
				BreakWeakpoint();
			}
		}

		public void SetHealth(float h) {
			health = h;
		}
		
		public float GetHealth() {
			return health;
		}

		public void SetText(string s) {
			text.SetText(s);
		}
	}

}
