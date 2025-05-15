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
        private bool isFalling;

        // Timers start at max to avoid accidentally triggering during start 
        private float _lastJumpPressTime = float.MaxValue;
        private float _lastGroundTime = float.MaxValue;

        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
			if (player_script.dead)
				return;

            // Timers
            _lastJumpPressTime += Time.deltaTime;
            _lastGroundTime += Time.deltaTime;

            #if UNITY_EDITOR
                if (Input.GetButtonDown("Jump"))
                    _lastJumpPressTime = 0f;
                if (!Input.GetButton("Jump") && _isJumping && _rigidbody.linearVelocityY > 0)
                    _isJumpCutting = true;
            #else
            #endif
                Debug.Log("Updating");
                if (TouchInputManager.TapStarted)
                {
                    _lastJumpPressTime = 0f;
                    Debug.Log("[Jump] Tap START detected: initiating jump.");
                }

                if (TouchInputManager.TapHeld && _isJumping && _rigidbody.linearVelocityY > 0)
                    _isJumpCutting = false;

                if (!TouchInputManager.TapHeld && _isJumping && _rigidbody.linearVelocityY > 0)
                {
                    _isJumpCutting = true;
                    Debug.Log("[Jump] Jump cut triggered (tap released).");
                }

            // Grounded check
            if (!_isJumping && Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer))
                _lastGroundTime = 0f;
            
            if (_isJumping && _rigidbody.linearVelocityY < 0)
            {
                _isJumping = false;
                _isJumpFalling = true;
            }

            if (!_isJumping && Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer))
            {
                _lastGroundTime = 0f;
                _isJumpFalling = false;
                _isJumping = false;
            }

            if (_isJumping && _rigidbody.linearVelocityY < 0)
            {
                _isJumping = false;
                _isJumpFalling = true;
            }

            if (!_isJumping && _lastGroundTime <= jumpCoyoteTime && _lastJumpPressTime <= jumpInputBufferTime)
                Jump();

            _rigidbody.gravityScale = (_isJumpFalling || _isJumpCutting) ? fallingGravity : jumpingGravity;

            if (_isJumpFalling || _isJumping)
            {
                isFalling = true;
            }

            if (!_isJumpFalling && !_isJumping)
            {
                if (isFalling)
                {
                    _animator.Play("running", 0, 0f);
                }
                isFalling = false;
            }
            _animator.SetBool("isFalling", isFalling);
        }

        private void Jump()
        {
            Debug.Log("JUMP");
            _animator.SetTrigger("Jump");
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