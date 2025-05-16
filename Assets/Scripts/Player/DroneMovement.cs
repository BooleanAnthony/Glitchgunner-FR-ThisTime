using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class DroneMovement : MonoBehaviour
    {
        [SerializeField] private Vector2 movementSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private float deceleration;
        [SerializeField] private SpriteRenderer[] allSprites;
        [SerializeField] private Vector2 minBounds; // bottom-left world position
        [SerializeField] private Vector2 maxBounds; // top-right world position
        [SerializeField] private float swipeThreshold = 50f;  // Min swipe magnitude to register movement
        [SerializeField] private float movementDuration = 0.3f; // How long to move after swipe (seconds)
        [SerializeField] private float movementMultiplier = 0.75f; // How hard to move after swipe (seconds)

        private Vector2 _movementInput = Vector2.zero;
        private float _movementTimer = 0f;
        
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private Shooting shootingScript;
        private Health healthScript;
        private BoxCollider2D _collider;
        public bool dead = false;
        public bool movable = true;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            shootingScript = GetComponent<Shooting>();
            healthScript = GetComponent<Health>();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if (!dead && movable)
            {
                // Default to zero each frame
                _movementInput = Vector2.zero;
                Vector2 swipe = TouchInputManager.LastSwipeDelta;

                if (swipe.magnitude > swipeThreshold)
                {
                    Vector2 direction;

                    // Clamp movement to 4 directions based on larger delta axis
                    if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                        direction = new Vector2(Mathf.Sign(swipe.x), 0);
                    else
                        direction = new Vector2(0, Mathf.Sign(swipe.y));

                    // Apply multiplier to get actual movement input
                    _movementInput = direction * movementMultiplier;
                }
                swipe = Vector2.zero;
            //#endif
                bool isMoving = _movementInput.sqrMagnitude > 0.01f;
                _animator.SetBool("isMoving", isMoving);
            } else 
            {
                if (TouchInputManager.TapDetected)
                {
                    RevivePlayer();
                }
            }
        }

        private void FixedUpdate()
        {
            if (!dead)
            {
                var targetSpeed = movementSpeed * _movementInput;
                var accelerationRate = (targetSpeed.sqrMagnitude > Mathf.Epsilon) ? acceleration : deceleration;
                var speedDifference = targetSpeed - _rigidbody.linearVelocity;
                
                _rigidbody.AddForce(_rigidbody.mass * accelerationRate * speedDifference);
            }
            ClampPosition();
        }

        private void ClampPosition()
        {
            Vector3 pos = transform.position;

            pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
            pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);

            transform.position = pos;
        }

        public IEnumerator KillPlayer() {
            movable = false;
            _animator.SetBool("isDead", true);
            yield return new WaitForSeconds(1f);
            dead = true;
            _collider.enabled = false;
            SetAllSpritesVisible(false);

            if (shootingScript != null) {
                shootingScript.enabled = false;
            }

            if (ScoreManager.instance != null)
			{
				ScoreManager.instance.AddToScore(-40);
			}
        }

        public void RevivePlayer() {
            movable = true;
            dead = false;
            SetAllSpritesVisible(true);
            shootingScript.OnRevive();
            healthScript.FullHeal();
            _collider.enabled = true;

            if (shootingScript != null)
            {
                shootingScript.enabled = true;
            }
            _animator.SetBool("isDead", false);
            _animator.Play("Idle", 0, 0f);
        }

        private void SetAllSpritesVisible(bool visible)
        {
            foreach (var sr in allSprites)
            {
                if (sr != null)
                {
                    sr.enabled = visible;
                }
            }
        }
    }
}