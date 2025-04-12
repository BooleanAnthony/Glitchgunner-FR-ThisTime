using UnityEngine;
using System.Collections;

namespace Player {
	public class codaScript : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer sprite;
		[SerializeField] private Canvas hud;
		[SerializeField] private GameObject death_text;
		
		public bool dead, revivable = false;
		private Animator _animator;

		public Checkpoint current_checkpoint = null;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		void Update()
		{
			if (revivable) {
				if (Input.GetKeyDown(KeyCode.Space)) {
					RestartToCheckpoint(current_checkpoint);
				}
			}
		}

		public void RestartToCheckpoint(Checkpoint checkpoint) {
			if (checkpoint != null) {
				transform.SetPositionAndRotation(checkpoint.gameObject.transform.position, transform.rotation);
			}
			else {
				transform.SetPositionAndRotation(new Vector2(-13.0f, -1.0f), transform.rotation);
			}

			_animator.SetTrigger("Reset");
			dead = false;
			revivable = false;
			sprite.enabled = true;
			death_text.SetActive(false);
			print("Player Respawned!");
		}

		public void SetCheckpoint(Checkpoint checkpoint) {
			if (current_checkpoint != null)
				current_checkpoint.is_current_checkpoint = false;

			current_checkpoint = checkpoint;
			checkpoint.is_current_checkpoint = true;

			print("Player new checkpoint!");
		}

		public void KillPlayer()  //happens when the player is dead offscreen
		{
			dead = true;
			print("Player died!");
			
			StartCoroutine(DeathSequence(1f));
		}

		public void PlayerOutOfBounds() //separated from KillPlayer to avoid playing animations when the player cannot be seen in the first place (if someone can optimize this, please do)
		{
			dead = true; 
			revivable = true;
			print("Player died!"); 
			death_text.SetActive(true); 
			sprite.enabled = false;
		}

		private IEnumerator DeathSequence(float value) {
			death_text.SetActive(true);
			_animator.SetTrigger("Death");
			
			yield return new WaitForSeconds(value);
			revivable = true;
			sprite.enabled = false;
		}
		
	}
}
