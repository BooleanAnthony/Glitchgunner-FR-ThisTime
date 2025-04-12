using UnityEngine;

namespace Environment
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] private Transform target;
        
        [SerializeField] private float horizontalOffset;
        
        [SerializeField] private float verticalBasePosition;
        [SerializeField] private float verticalBoundsMax;
        [SerializeField] private float verticalBoundsMin;
        [SerializeField] private float verticalSmoothTime;

        private float _verticalVelocity;

        private void LateUpdate()
        {
            var targetY = transform.position.y;
            
            if (target.position.y > transform.position.y + verticalBoundsMax)
                targetY = target.position.y - verticalBoundsMax;
            else if (target.position.y < transform.position.y + verticalBoundsMin)
                targetY = target.position.y - verticalBoundsMin;
            
            if (target.position.y < verticalBasePosition)
                targetY = verticalBasePosition;
            
            var newY = Mathf.SmoothDamp(transform.position.y, targetY, ref _verticalVelocity, verticalSmoothTime);
            
            transform.position = new Vector3(target.position.x - horizontalOffset, newY, transform.position.z);
        }
    }
}