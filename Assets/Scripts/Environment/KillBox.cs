using UnityEngine;

public class KillBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] BoxCollider2D collision;
    [SerializeField] BoxCollider2D player_collision;
	[SerializeField] bool InFrame;

    // Update is called once per frame
	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision == player_collision) {
			Player.codaScript player_script = player_collision.GetComponent<Player.codaScript>();

			if (!player_script.dead && InFrame) {
				player_script.KillPlayer();
			} else {
				player_script.PlayerOutOfBounds();
			}
		}
	}
}
