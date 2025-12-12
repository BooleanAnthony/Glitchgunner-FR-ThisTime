using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    private Transform cameraTransform;
    private Vector3 lastCamPos;
    private float textureUnitSizeX;
    private float textureUnitSizeY;
    [SerializeField] private Vector2 parallaxMultiplier;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCamPos = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCamPos;
        transform.position += new Vector3(deltaMovement.x * parallaxMultiplier.x, deltaMovement.y * parallaxMultiplier.y);
        lastCamPos = cameraTransform.position;

        if (Mathf.Abs(cameraTransform.position.x - transform.position.x - 0.01f) >= textureUnitSizeX)
        {
            float offsetPositionX = Mathf.Sign(cameraTransform.position.x - transform.position.x) * (Mathf.Abs(cameraTransform.position.x - transform.position.x) % textureUnitSizeX);
            transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
        }
        if (Mathf.Abs(cameraTransform.position.y - transform.position.y - 0.01f) >= textureUnitSizeY)
        {
            float offsetPositionY = Mathf.Sign(cameraTransform.position.y - transform.position.y) * (Mathf.Abs(cameraTransform.position.y - transform.position.y) % textureUnitSizeY);
            transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offsetPositionY);
        }
    }
}