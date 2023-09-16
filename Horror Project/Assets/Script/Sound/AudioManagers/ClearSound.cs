using UnityEngine;

public class ClearSound : MonoBehaviour
{
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(!source.isPlaying)
        {
            source.Stop();
            DestroyImmediate(gameObject);
        }
    }
}
