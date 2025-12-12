using UnityEngine;

namespace Player
{
    public class Running : MonoBehaviour
    {
        [SerializeField] private float runningSpeed;
        [SerializeField] private float runningAcceleration;
        [SerializeField] private int secondPerPoint;
		[SerializeField] private codaScript player_script;
        private float timer = 0f;
        private Rigidbody2D _rigidbody;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!player_script.dead)
            {
                timer += Time.deltaTime;
                if (timer >= secondPerPoint)
                {
                    if (ScoreManager.instance != null)
                    {
                        ScoreManager.instance.AddToScore(1);
                        timer = 0;
                    }
                }
            }
        }

        private void FixedUpdate()
        {
			if (player_script.dead) {
				_rigidbody.linearVelocityX = 0;
				return;
			}

            var speedDifference = runningSpeed - _rigidbody.linearVelocityX;

            _rigidbody.AddForceX(_rigidbody.mass * speedDifference * runningAcceleration);
        }
    }
}
