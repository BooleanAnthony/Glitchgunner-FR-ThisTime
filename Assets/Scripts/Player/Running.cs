using UnityEngine;

namespace Player
{
    public class Running : MonoBehaviour
    {
        [SerializeField] private float runningSpeed;
        [SerializeField] private float runningAcceleration;
		[SerializeField] private codaScript player_script;
        
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
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
