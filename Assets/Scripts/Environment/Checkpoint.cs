using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public bool is_current_checkpoint = false;
	[SerializeField] Collider2D collide;
	[SerializeField] Collider2D player_collision;

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision == player_collision && !is_current_checkpoint) {
			Player.codaScript player_script = player_collision.GetComponent<Player.codaScript>();

			player_script.SetCheckpoint(this);

			GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
		}
	}
}
