using UnityEngine;

public class ParallaxMovement : MonoBehaviour
{
    Material mat;
    float distance;

    [Range(0f, 0.5f)]
    [SerializeField] private float speed = 0.2f;
    [SerializeField] private ParallaxController parallaxController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (parallaxController != null)
        {
            if (parallaxController.continueParallax)
            {
                distance += Time.deltaTime*speed;
                mat.SetTextureOffset("_MainTex", Vector2.right * distance);
            }
        } 
        else 
        {
            distance += Time.deltaTime*speed;
            mat.SetTextureOffset("_MainTex", Vector2.right * distance);
        }
        
    }
}
