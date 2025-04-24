using System;
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

        private Vector2 _movementInput;
        
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private Shooting shootingScript;
        private Health healthScript;
        public bool dead = false;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            shootingScript = GetComponent<Shooting>();
            healthScript = GetComponent<Health>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (!dead)
            {
                _movementInput.x = Input.GetAxisRaw("Horizontal");
                _movementInput.y = Input.GetAxisRaw("Vertical");

                bool isMoving = _movementInput.sqrMagnitude > 0.01f;
                _animator.SetBool("isMoving", isMoving);
            } else 
            {
                if (Input.GetKeyDown(KeyCode.Space))
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

        public void KillPlayer() {
            dead = true;
            SetAllSpritesVisible(false);

            if (shootingScript != null) {
                shootingScript.enabled = false;
            }
        }

        public void RevivePlayer() {
            dead = false;
            SetAllSpritesVisible(true);
            shootingScript.OnRevive();
            healthScript.FullHeal();

            if (shootingScript != null) {
                shootingScript.enabled = true;
            }
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