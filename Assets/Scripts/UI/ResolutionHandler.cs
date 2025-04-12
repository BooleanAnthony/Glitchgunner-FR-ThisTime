using UnityEngine;
using UnityEngine.UI;

public class ResolutionHandler : MonoBehaviour
{
    [SerializeField] private float targetWidth = 640f;
    [SerializeField] private float targetHeight = 480f;
    [SerializeField] private float minScale = 1f;
    [SerializeField] private float maxScale = 3f;
    
    private void Awake()
    {
        // Calculate the scale based on screen height
        float scale = Screen.height / targetHeight;
        scale = Mathf.Clamp(scale, minScale, maxScale);
        
        // Apply the scale while maintaining aspect ratio
        float currentAspectRatio = Screen.width / Screen.height;
        float targetAspectRatio = targetWidth / targetHeight;
        
        transform.localScale = new Vector3(
            scale * targetAspectRatio,
            scale,
            1f
        );
    }
}
