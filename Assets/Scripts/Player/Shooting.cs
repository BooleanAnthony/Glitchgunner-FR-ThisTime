using System.Collections;
using UnityEngine;

namespace Player
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform shootPoint;
        
        [SerializeField] private float fireRate;
        [SerializeField] private DroneMovement movement;
        
        private bool _isFiring;
        private Animator _anim;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (TouchInputManager.TapHeld && !_isFiring)
            {
                StartCoroutine(FireBullet());
            }
        }

        private IEnumerator FireBullet()
        {
            _isFiring = true;

            while (Input.GetButton("Fire1"))
            {
                _anim.SetTrigger("isFiring");
                if (movement.dead) 
                {
                    _isFiring = false;
                    yield break; 
                }

                Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
                yield return new WaitForSeconds(fireRate);
            }

            _isFiring = false;
        }

        public void OnRevive()
        {
            _isFiring = false; 
        }

    }
}