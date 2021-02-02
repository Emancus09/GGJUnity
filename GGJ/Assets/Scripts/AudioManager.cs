using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager mInstance;
    public static AudioManager instance => mInstance;

    AudioSource mSource;
    public AudioSource source => mSource;

    void Start()
    {
        // Singleton stuff
        if (mInstance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        mInstance = this;
        DontDestroyOnLoad(this);

        mSource = gameObject.GetComponent<AudioSource>();
    }

    public static void PlayClip(AudioClip clip)
    {
        instance.source.PlayOneShot(clip);
    }
}
