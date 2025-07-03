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

        private Vector2 _movementInput;
        
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private Shooting shootingScript;
        private Health healthScript;
        public bool dead = false;
        public bool movable = true;
        public AudioSource audioPlayer;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            shootingScript = GetComponent<Shooting>();
            healthScript = GetComponent<Health>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (!dead && movable)
            {
                _movementInput = Vector2.zero;

                if (Input.GetKey(KeyCode.Space))
                {
                    _movementInput.y = 1f;
                }

                bool isMoving = _movementInput.y > 0.01f;
                _animator.SetBool("isMoving", isMoving);
            }
            else
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
                Vector2 targetSpeed;

                if (_movementInput.y > 0.01f)
                {
                    // Ascending movement
                    targetSpeed = new Vector2(0f, movementSpeed.y);
                }
                else
                {
                    // Passive descent
                    targetSpeed = new Vector2(0f, -movementSpeed.y * 0.5f); // adjust descent rate as needed
                }

                Vector2 speedDifference = targetSpeed - _rigidbody.linearVelocity;

                _rigidbody.AddForce(_rigidbody.mass * acceleration * speedDifference);
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
            SetAllSpritesVisible(false);
            audioPlayer.Play();

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

            if (shootingScript != null) {
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