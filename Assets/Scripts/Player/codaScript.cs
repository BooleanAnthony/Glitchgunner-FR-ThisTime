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
		public static codaScript Instance { get; private set; }

		private void Start()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(gameObject);
				return;
			}

			Instance = this;

			_animator = GetComponent<Animator>();

        }

		void Update()
		{
			if (revivable) {
				if (Input.GetKeyDown(KeyCode.Space)) {
					RestartToCheckpoint(current_checkpoint);
				}
			}

            if (hud == null)
            {
                hud = GameObject.Find("Canvas").GetComponent<Canvas>();
            }

            if (death_text == null)
            {
				//death_text = GameObject.Find("Canvas/Death Text");
				Transform canvasTransform = GameObject.Find("Canvas").transform;

				if (canvasTransform != null)
				{
					Transform deathTextTransform = canvasTransform.Find("DeathText");
					if (deathTextTransform != null)
					{
						death_text = deathTextTransform.gameObject;
					}
					else
					{
						Debug.Log("deathtext transform not found");
					}
				}
                else
                {
                    Debug.Log("canvastext transform not found");
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
			
			StartCoroutine(DeathSequence(3.1f));
			if (ScoreManager.instance != null)
			{
				ScoreManager.instance.AddToScore(-75);
			}
		}

		public void PlayerOutOfBounds() //separated from KillPlayer to avoid playing animations when the player cannot be seen in the first place (if someone can optimize this, please do)
		{
			dead = true; 
			revivable = true;
			death_text.SetActive(true);
			print("Player died!"); 
			sprite.enabled = false;
			if (ScoreManager.instance != null)
			{
				ScoreManager.instance.AddToScore(-75);
			}
		}

		private IEnumerator DeathSequence(float value) {
			Debug.Log("Playing Death");
			_animator.SetTrigger("Death");
			
			yield return new WaitForSeconds(value);
			death_text.SetActive(true);
			revivable = true;
			sprite.enabled = false;
		}
		
	}
}
