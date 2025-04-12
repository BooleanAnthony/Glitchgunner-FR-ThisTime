using System;
using UnityEditor.UI;
using UnityEngine;
using Player;

namespace Environment
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] protected float speed;
        [SerializeField] protected float lifetime;
		[SerializeField] protected float damage;
		
        protected float _age;
        
        protected Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _age = 0f;
            _rigidbody.linearVelocity = transform.right * speed;
        }

		private void OnTriggerEnter2D(Collider2D collision) {
			BossFights.WeakpointChoice weakpoint = collision.GetComponent<BossFights.WeakpointChoice>();

			if (weakpoint != null) {
				if (!weakpoint.invulnerable)
					weakpoint.ChangeHealth(-damage);
			}

			Destroy(gameObject);
		}

		private void Update()
        {
            _age += Time.deltaTime;
            
            if (_age >= lifetime)
                Destroy(gameObject);
        }

        public float GetDamage()
        {
            return damage;
        }
    }
}