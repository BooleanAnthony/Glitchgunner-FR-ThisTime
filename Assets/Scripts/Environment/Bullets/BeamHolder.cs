using UnityEngine;

public class BeamHolder : MonoBehaviour
{
    [SerializeField] protected float lifetime;
    [SerializeField] protected GameObject beam;
    public float _age;
    private BoxCollider2D beamCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _age = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _age += Time.deltaTime;
            
        if (_age >= lifetime)
            Destroy(gameObject);
    }

    private void ActivateBeam()
    {
        beamCollider = beam.GetComponent<BoxCollider2D>();
        beamCollider.enabled = true;
    }
}
