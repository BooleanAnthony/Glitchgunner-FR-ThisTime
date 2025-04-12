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

        private Vector2 _movementInput;
        
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private SpriteRenderer sprite;
        private Shooting shootingScript;
        private Health healthScript;
        public bool dead = false;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
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
            var targetSpeed = movementSpeed * _movementInput;
            var accelerationRate = (targetSpeed.sqrMagnitude > Mathf.Epsilon) ? acceleration : deceleration;
            var speedDifference = targetSpeed - _rigidbody.linearVelocity;
            
            _rigidbody.AddForce(_rigidbody.mass * accelerationRate * speedDifference);
        }

        public void KillPlayer() {
            dead = true;
            Debug.Log("Player died!");
            SetAllSpritesVisible(false);

            if (shootingScript != null) {
                shootingScript.enabled = false;
            }
        }

        public void RevivePlayer() {
            dead = false;
            Debug.Log("Player revived!");
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