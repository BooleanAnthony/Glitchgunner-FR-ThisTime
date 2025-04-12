using UnityEngine;

namespace Player
{
    public class Jumping : MonoBehaviour
    {
		[SerializeField] private codaScript player_script;

        [Header("Grounded Checking")]
        
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Vector2 groundCheckSize;
        
        [Header("Timers")]

        [SerializeField] private float jumpInputBufferTime;
        [SerializeField] private float jumpCoyoteTime;
        
        [Header("Jumping")]
        
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpingGravity;
        [SerializeField] private float fallingGravity;
        
        private bool _isJumping;
        private bool _isJumpFalling;
        private bool _isJumpCutting;

        // Timers start at max to avoid accidentally triggering during start 
        private float _lastJumpPressTime = float.MaxValue;
        private float _lastGroundTime = float.MaxValue;

        private Rigidbody2D _rigidbody;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
			if (player_script.dead)
				return;

            // Timers
            _lastJumpPressTime += Time.deltaTime;
            _lastGroundTime += Time.deltaTime;

            if (Input.GetButtonDown("Jump"))
                _lastJumpPressTime = 0f;
            if (!Input.GetButton("Jump") && _isJumping && _rigidbody.linearVelocityY > 0)
                _isJumpCutting = true;

            // Grounded check
            if (!_isJumping && Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer))
                _lastGroundTime = 0f;
            
            if (_isJumping && _rigidbody.linearVelocityY < 0)
            {
                _isJumping = false;
                _isJumpFalling = true;
            }

            if (!_isJumping && _lastGroundTime <= jumpCoyoteTime && _lastJumpPressTime <= jumpInputBufferTime)
                Jump();

            _rigidbody.gravityScale = (_isJumpFalling || _isJumpCutting) ? fallingGravity : jumpingGravity;
        }

        private void Jump()
        {
            _isJumping = true;
            _isJumpFalling = false;
            _isJumpCutting = false;

            var force = jumpForce;

            // More jump force if moving down   
            if (_rigidbody.linearVelocityY < 0)
                force -= _rigidbody.linearVelocityY;
            
            _rigidbody.AddForceY(_rigidbody.mass * force, ForceMode2D.Impulse);
        }
    }
}