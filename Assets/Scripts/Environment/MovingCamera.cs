using UnityEngine;

namespace Environment 
{
    public class MovingCamera : MonoBehaviour
    {
        [SerializeField] private float scrollSpeed;

        private void LateUpdate()
        {
            transform.Translate(scrollSpeed * Time.deltaTime * Vector2.right, Space.World);
        }
    }
}
