using UnityEngine;

public class ScaleFromMicrophone : MonoBehaviour
{
    public enum Mode
    {
        Normal,
        Scaling
    }
    public Mode result;
    public AudioSource source;
    public Vector2 minScale;
    public Vector2 maxScale;
    public AudioLoudnessDetection detector;

    public float loudnessSensibility = 100;
    public float threshold = 0.1f;
    public float loudness;

    // Update is called once per frame
    void Update()
    {
        loudness = detector.GetMicrophoneLoudness() * loudnessSensibility;

        if (loudness < threshold)
        {
            loudness = 0;
        }

        if (result == Mode.Scaling)
        {
            transform.localScale = Vector2.Lerp(minScale, maxScale, loudness);
        }
    }
}
